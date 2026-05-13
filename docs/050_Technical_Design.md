# TaskHost Local – Technical Design

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck des Dokuments

Dieses Dokument beschreibt den technischen Aufbau von **TaskHost Local**. Es dient als Orientierung für Implementierung, Debugging und spätere Weiterentwicklung.

## 2. Technisches Ziel

Das technische Ziel der ersten Version ist eine einfache, lokal lauffähige Windows-Desktop-Anwendung mit zuverlässiger SQLite-Speicherung.

Wichtige Ziele:

- schnelle lokale Nutzbarkeit
- verständlicher Code
- einfache Architektur
- klare Trennung von UI und Datenbankzugriff
- spätere Erweiterbarkeit
- kein unnötiger Framework-Ballast

## 3. Technologie-Stack

| Bereich | Entscheidung |
|---|---|
| Sprache | C# |
| Runtime/Framework | .NET 8 Windows |
| UI | Windows Forms |
| Datenbank | SQLite |
| Datenbankbibliothek | Microsoft.Data.Sqlite |
| Build | .NET SDK / Visual Studio |
| Zielplattform | Windows |
| Repository | GitHub |

## 4. Architekturprinzipien

### 4.1 Einfache Schichtung

Die Anwendung wird in einfache logische Schichten getrennt:

```text
UI / Forms
   ↓
Services
   ↓
Repositories
   ↓
Database
   ↓
SQLite
```

### 4.2 Keine SQL-Logik in Formularen

Ein wichtiges Architekturprinzip lautet:

> **Formulare dürfen keine SQL-Statements enthalten.**

Formulare sollen Dienste aufrufen. SQL gehört ausschließlich in Repositories.

### 4.3 Keine Überarchitektur im MVP

Das MVP soll nicht durch zu viele Projekte, Interfaces oder Frameworks verlangsamt werden.

Für V1 genügt ein einzelnes WinForms-Projekt mit klaren Ordnern. Eine spätere Aufteilung in mehrere Projekte bleibt möglich.

Mögliche spätere Struktur:

```text
TaskHostLocal.Core
TaskHostLocal.Data.Sqlite
TaskHostLocal.WinForms
TaskHostLocal.Tests
TaskHostLocal.Sync.TaskHostApi
```

## 5. Hauptkomponenten

### 5.1 Program.cs

Startpunkt der Anwendung.

Aufgaben:

- WinForms-Anwendung initialisieren
- `MainForm` starten

### 5.2 MainForm.cs

Hauptfenster der Anwendung.

Aufgaben:

- Menü und Toolbar aufbauen
- linke Navigation anzeigen
- Aufgaben anzeigen
- Benutzeraktionen entgegennehmen
- Services aufrufen
- UI nach Änderungen aktualisieren

Nicht-Aufgaben:

- SQL ausführen
- Datenbankverbindungen öffnen
- tiefe Geschäftslogik enthalten

### 5.3 Forms/TaskEditForm.cs

Dialog oder Bearbeitungsformular für Aufgaben.

Aufgaben:

- Eingabefelder für Titel, Notiz, Fälligkeit, Priorität bereitstellen
- Eingaben validieren oder an Service übergeben
- Ergebnis an MainForm zurückgeben

### 5.4 Forms/ListEditForm.cs

Dialog für Listen.

Aufgaben:

- Listennamen erfassen
- leere Namen verhindern

### 5.5 Models

Models repräsentieren fachliche Datenobjekte.

Wichtige Models:

- `TaskItem`
- `TaskList`

Diese Klassen sollten möglichst wenig Logik enthalten und als verständliche Datenstrukturen dienen.

### 5.6 Services

Services enthalten fachliche Operationen.

Beispiele:

- `TaskService`
- `ListService`
- `BackupService`

Services koordinieren Repositories und enthalten einfache Regeln.

### 5.7 Repositories

Repositories kapseln Datenbankzugriff.

Beispiele:

- `TaskRepository`
- `ListRepository`

Repositories verwenden parametrisierte SQL-Abfragen und mappen Datenbankzeilen auf Models.

### 5.8 Database

Database-Klassen sind für Datenbankpfad, Verbindung und Initialisierung zuständig.

Beispiele:

- `DbConnectionFactory`
- `DatabaseInitializer`

## 6. Datenbankort

Die Datenbank soll nicht im Programmverzeichnis liegen.

Empfohlener Pfad:

```text
%AppData%\SASD\TaskHostLocal\taskhost.db
```

Beispiel:

```text
C:\Users\<User>\AppData\Roaming\SASD\TaskHostLocal\taskhost.db
```

Vorteile:

- keine Adminrechte erforderlich
- Daten bleiben bei Programmupdates erhalten
- klarer Speicherort
- einfache Backupmöglichkeit

## 7. Datenbankinitialisierung

Beim Start der Anwendung soll `DatabaseInitializer` sicherstellen:

- AppData-Verzeichnis existiert
- SQLite-Datei existiert
- Tabellen existieren
- Standardliste existiert

Initialisierung muss idempotent sein. Mehrfaches Starten darf keine Duplikate erzeugen.

## 8. SQL-Strategie

Für das MVP wird direktes SQL verwendet.

Regeln:

- SQL nur in Repository-Klassen
- Parameterbindung statt Stringverkettung
- `CREATE TABLE IF NOT EXISTS` für Initialisierung
- ISO-ähnliche Datumswerte als Text speichern
- Booleans als Integer speichern (`0`/`1`)

## 9. Fehlerbehandlung

Fehler werden in der UI verständlich angezeigt.

Beispielstruktur:

```text
Die Aufgaben konnten nicht geladen werden.

Details:
SQLite Error 1: near "=": syntax error.
```

Für die spätere Entwicklung wäre optional eine einfache Logging-Komponente sinnvoll, aber nicht zwingend im MVP.

## 10. Build und Start

Typische Befehle:

```powershell
dotnet restore
dotnet build
dotnet run --project .\TaskHostLocal.WinForms\TaskHostLocal.WinForms.csproj
```

Für Windows Forms sollte der Start bevorzugt unter Windows erfolgen, nicht aus WSL heraus.

## 11. Bekannter technischer Fehler

Aktueller Laufzeitfehler:

```text
SQLite Error 1: near "=": syntax error.
```

Wahrscheinliche Ursache:

- fehlerhaft zusammengesetztes SQL in `TaskRepository`
- möglicherweise ungültige WHERE-Bedingung bei optionalen Filtern
- möglicherweise Syntax wie `WHERE = ...` oder `AND = ...`

Vorgehen zur Behebung:

1. SQL-Statements in `TaskRepository` prüfen.
2. Query bei leerem Suchbegriff prüfen.
3. Query bei ausgewählter Liste prüfen.
4. Parameter und zusammengesetzte WHERE-Bedingungen prüfen.
5. Minimalen Test mit Standardliste und leerer Aufgabenliste durchführen.

## 12. Öffentliche Repository-Nutzung

Da das Repository öffentlich ist, gelten technische Schutzregeln:

- `.db`-Dateien ignorieren
- Backup-Dateien ignorieren
- `bin/` und `obj/` ignorieren
- keine echten Aufgaben oder persönlichen Daten einchecken
- keine Secrets einchecken

## 13. Spätere technische Erweiterungen

Mögliche spätere Erweiterungen:

- Tests für Services und Repositories
- separate Core-/Data-Projekte
- Export/Import im JSON-Format
- TaskHost-kompatibles Datenformat
- API-Adapter für TaskHost
- Sync-Schicht
- besseres Logging
- Migrationen mit Schema-Versionierung
- Avalonia-Client als spätere plattformübergreifende UI

## 14. Technische Nicht-Ziele für V1

Nicht Bestandteil von V1:

- Entity Framework
- komplexes Dependency Injection Setup
- Cloud-API
- Authentifizierung
- Hintergrunddienst
- automatische Synchronisierung
- Installer
- Auto-Update
- Plugin-System
- Verschlüsselung der Datenbank

Diese Punkte können später neu bewertet werden.

