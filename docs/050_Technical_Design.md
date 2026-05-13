# TaskHost Local – Technical Design

**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13  
**Bezug:** Lastenheft/Pflichtenheft MVP v0.2

## 1. Zweck des Dokuments

Dieses Dokument beschreibt den technischen Aufbau von **TaskHost Local**.

Es dient als Orientierung für:

- Implementierung,
- Debugging,
- Code-Reviews,
- spätere Weiterentwicklung,
- Abgleich mit Lastenheft und Pflichtenheft.

Temporäre Laufzeitfehler werden nicht dauerhaft in diesem Dokument gepflegt. Sie gehören nach `080_Known_Issues.md`.

## 2. Technisches Ziel

Das technische Ziel der ersten Version ist eine einfache, lokal lauffähige Windows-Desktop-Anwendung mit zuverlässiger SQLite-Speicherung.

Wichtige Ziele:

- schnelle lokale Nutzbarkeit,
- verständlicher Code,
- einfache Architektur,
- klare Trennung von UI und Datenbankzugriff,
- spätere Erweiterbarkeit,
- kein unnötiger Framework-Ballast,
- keine Netzwerkkommunikation im MVP.

## 3. Technologie-Stack

| Bereich | Entscheidung |
|---|---|
| Sprache | C# |
| Runtime/Framework | .NET 8 Windows |
| UI | Windows Forms |
| Datenbank | SQLite |
| Datenbankbibliothek | Microsoft.Data.Sqlite |
| ORM | keines |
| Build | .NET SDK / Visual Studio |
| Zielplattform MVP | Windows |
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

Für V1 genügt ein einzelnes WinForms-Projekt mit klaren Ordnern.

Eine spätere Aufteilung in mehrere Projekte bleibt möglich:

```text
TaskHostLocal.Core
TaskHostLocal.Data.Sqlite
TaskHostLocal.WinForms
TaskHostLocal.Tests
TaskHostLocal.Sync.TaskHostApi
```

Diese Aufteilung soll aber erst erfolgen, wenn der Nutzen größer ist als die zusätzliche Komplexität.

### 4.4 Offline-first im MVP

Das MVP ist vollständig lokal.

Es darf im MVP nicht enthalten:

- HTTP-Client für TaskHost,
- API-Anbindung,
- Telemetrie,
- automatische Update-Prüfung,
- Hintergrunddienst für Synchronisierung,
- Netzwerkkommunikation zur Laufzeit.

## 5. Hauptkomponenten

### 5.1 Program.cs

Startpunkt der Anwendung.

Aufgaben:

- WinForms-Anwendung initialisieren,
- Anwendungskonfiguration vorbereiten,
- `MainForm` starten.

### 5.2 MainForm.cs

Hauptfenster der Anwendung.

Aufgaben:

- Menü und Toolbar aufbauen,
- linke Navigation anzeigen,
- Aufgaben anzeigen,
- Benutzeraktionen entgegennehmen,
- Services aufrufen,
- UI nach Änderungen aktualisieren,
- Fehlermeldungen verständlich anzeigen.

Nicht-Aufgaben:

- SQL ausführen,
- Datenbankverbindungen öffnen,
- tiefe Geschäftslogik enthalten,
- direkte Dateisystemlogik für Backups enthalten.

### 5.3 Forms/TaskEditForm.cs

Dialog oder Bearbeitungsformular für Aufgaben.

Aufgaben:

- Eingabefelder für Titel, Notiz, Fälligkeit und Priorität bereitstellen,
- Eingaben plausibilisieren,
- Ergebnis an MainForm bzw. Service zurückgeben,
- keine SQL-Operationen ausführen.

### 5.4 Forms/ListEditForm.cs

Dialog für Listen.

Aufgaben:

- Listennamen erfassen,
- leere Namen verhindern,
- keine SQL-Operationen ausführen.

### 5.5 Models

Models repräsentieren fachliche Datenobjekte.

Wichtige Models:

- `TaskItem`,
- `TaskList`.

Diese Klassen sollten möglichst wenig Logik enthalten und als verständliche Datenstrukturen dienen.

### 5.6 Services

Services enthalten fachliche Operationen.

Beispiele:

- `TaskService`,
- `ListService`,
- `BackupService`.

Services koordinieren Repositories und enthalten einfache Regeln, zum Beispiel:

- Titel darf nicht leer sein,
- Liste mit Aufgaben darf im MVP nicht gelöscht werden,
- Priorität muss in einem gültigen Bereich liegen,
- beim Erledigen wird `completed_at` gesetzt,
- beim Wiederöffnen wird `completed_at` zurückgesetzt.

### 5.7 Repositories

Repositories kapseln Datenbankzugriff.

Beispiele:

- `TaskRepository`,
- `ListRepository`.

Repositories verwenden:

- parametrisierte SQL-Abfragen,
- nachvollziehbare Query-Methoden,
- eindeutiges Mapping von Datenbankzeilen auf Models.

Repositories sollen keine UI-Logik enthalten.

### 5.8 Database

Database-Klassen sind für Datenbankpfad, Verbindung und Initialisierung zuständig.

Beispiele:

- `DbConnectionFactory`,
- `DatabaseInitializer`.

Aufgaben:

- AppData-Pfad bestimmen,
- Verzeichnis anlegen,
- SQLite-Verbindung erzeugen,
- Tabellen initialisieren,
- Standardliste sicherstellen.

## 6. Datenbankort

Die Datenbank soll nicht im Programmverzeichnis liegen.

Empfohlener Pfad:

```text
%AppData%\SASD\TaskHostLocal\taskhost.db
```

Beispiel:

```text
C:\Users\<Benutzername>\AppData\Roaming\SASD\TaskHostLocal\taskhost.db
```

Vorteile:

- keine Adminrechte erforderlich,
- Daten bleiben bei Programmupdates erhalten,
- klarer Speicherort,
- einfache Backupmöglichkeit,
- benutzerspezifische Datenhaltung.

## 7. Datenbankinitialisierung

Beim Start der Anwendung soll `DatabaseInitializer` sicherstellen:

- AppData-Verzeichnis existiert,
- SQLite-Datei existiert,
- Tabellen existieren,
- Standardliste „Eingang“ existiert.

Initialisierung muss idempotent sein.

Mehrfaches Starten darf keine Duplikate erzeugen.

## 8. SQL-Strategie

Für das MVP wird direktes SQL verwendet.

Regeln:

- SQL nur in Repository-Klassen,
- Parameterbindung statt Stringverkettung für Werte,
- keine dynamischen WHERE-Klauseln ohne saubere Prüfung,
- `CREATE TABLE IF NOT EXISTS` für Initialisierung,
- ISO-nahe Datumswerte als Text speichern,
- Booleans als Integer speichern (`0`/`1`),
- Smart Views als definierte Abfragen umsetzen, nicht als Datenbanklisten.

### 8.1 Dynamische Filter

Dynamische Filter sind eine typische Fehlerquelle.

Für Suche und Smart Views gilt:

- WHERE-Bedingungen dürfen nicht mit leerem Spaltennamen erzeugt werden.
- Optionale Bedingungen müssen kontrolliert zusammengesetzt werden.
- Leerer Suchtext muss eine gültige Abfrage erzeugen.
- Kein Code darf SQL-Fragmente wie `WHERE = ...` oder `AND = ...` erzeugen.

## 9. Datenmodell-Kurzfassung

MVP-Tabellen:

```text
task_lists
- id
- name
- sort_order
- created_at
- updated_at

tasks
- id
- list_id
- title
- description
- due_date
- priority
- is_completed
- created_at
- updated_at
- completed_at
```

Wichtige Regeln:

- Jede Aufgabe gehört genau zu einer echten Liste.
- „Eingang“ ist eine echte Liste.
- Smart Views sind gefilterte Ansichten.
- Favoriten benötigen später `is_starred`.
- Unteraufgaben, Tags, Anhänge und Erinnerungen gehören nicht ins MVP.

## 10. Fehlerbehandlung

Fehler werden in der UI verständlich angezeigt.

Beispiel:

```text
Die Aufgaben konnten nicht geladen werden.

Details:
SQLite Error 1: near "=": syntax error.
```

Für Entwickler soll die technische Detailmeldung sichtbar bleiben, damit frühe Fehler schnell analysiert werden können.

Später kann eine einfache Logging-Komponente ergänzt werden. Für das MVP ist Logging hilfreich, aber nicht zwingend.

## 11. Backup

Die Backup-Funktion soll die aktuelle SQLite-Datenbankdatei kopieren.

Anforderungen:

- Backup-Dateiname soll Datum/Uhrzeit enthalten.
- Zielpfad soll für den Benutzer nachvollziehbar sein.
- Backup darf nicht automatisch ins Repository gelangen.
- Backup ist kein fachlicher Export.

Ein JSON-Export/Import ist eine spätere Erweiterung.

## 12. Build und Start

Typische Befehle:

```powershell
dotnet restore
dotnet build
dotnet run --project .\TaskHostLocal.WinForms\TaskHostLocal.WinForms.csproj
```

Für Windows Forms sollte der Start bevorzugt unter Windows erfolgen, nicht primär aus WSL heraus.

## 13. Öffentliche Repository-Nutzung

Da das Repository öffentlich ist, gelten technische Schutzregeln:

- `.db`-Dateien ignorieren,
- Backup-Dateien ignorieren,
- `bin/` und `obj/` ignorieren,
- keine echten Aufgaben oder persönlichen Daten einchecken,
- keine Secrets einchecken,
- Screenshots nur mit fiktiven Daten erstellen.

## 14. TaskHost-Kompatibilität

TaskHost Local bleibt im MVP eigenständig.

Trotzdem sollen Benennung und Datenmodell spätere Integration nicht erschweren.

Zu berücksichtigen sind:

- TaskList,
- TaskItem,
- DueDate,
- Priority,
- Completed,
- Description,
- Starred,
- SubTask,
- Attachment,
- Reminder,
- SmartView.

Nicht Teil des MVP:

- API-Adapter,
- Authentifizierung,
- Token-Speicherung,
- Sync-Status,
- Konfliktlösung,
- Remote-IDs.

## 15. Spätere technische Erweiterungen

Mögliche spätere Erweiterungen:

- Tests für Services und Repositories,
- separate Core-/Data-Projekte,
- Export/Import im JSON-Format,
- TaskHost-kompatibles Datenformat,
- API-Adapter für TaskHost,
- Sync-Schicht,
- besseres Logging,
- Migrationen mit Schema-Versionierung,
- Avalonia-Client als spätere plattformübergreifende UI,
- Installer,
- optionale Datenbankverschlüsselung.

## 16. Technische Nicht-Ziele für V1

Nicht Bestandteil von V1:

- Entity Framework,
- komplexes Dependency Injection Setup,
- Cloud-API,
- Authentifizierung,
- Hintergrunddienst,
- automatische Synchronisierung,
- Installer,
- Auto-Update,
- Plugin-System,
- Verschlüsselung der Datenbank,
- TaskHost-API-Anbindung,
- Telemetrie.

Diese Punkte können später neu bewertet werden.
