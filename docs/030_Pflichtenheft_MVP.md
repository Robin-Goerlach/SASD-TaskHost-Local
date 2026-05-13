# TaskHost Local – MVP-Pflichtenheft

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  
**Perspektive:** Umsetzung / Wie soll das MVP realisiert werden?  

## 1. Zweck des Dokuments

Dieses Pflichtenheft beschreibt, wie die im Lastenheft genannten Anforderungen für die erste arbeitsfähige Version von **TaskHost Local** umgesetzt werden sollen.

Es dient als Brücke zwischen fachlichem Wunsch und konkreter technischer Umsetzung.

## 2. Umsetzungsschwerpunkt

Der Schwerpunkt der MVP-Umsetzung liegt auf:

1. stabiler lokaler Datenhaltung
2. einfacher Aufgaben- und Listenverwaltung
3. verständlicher Oberfläche
4. sauberer Code-Struktur
5. schneller Nutzbarkeit

Nicht im Fokus stehen:

- perfekte visuelle Gestaltung
- komplexe Architektur
- Cloud-/API-Integration
- Mehrbenutzerbetrieb
- vollständige Automatisierung

## 3. Technologieentscheidungen

Für das MVP werden folgende Technologien eingesetzt:

| Bereich | Entscheidung |
|---|---|
| Programmiersprache | C# |
| Framework | .NET 8 Windows |
| UI | Windows Forms |
| Datenbank | SQLite |
| SQLite-Zugriff | `Microsoft.Data.Sqlite` |
| ORM | kein ORM im MVP |
| Datenbankort | `%AppData%\SASD\TaskHostLocal\taskhost.db` |
| Projektstruktur | ein WinForms-Projekt mit sauberer Ordnerstruktur |

Begründung:

- C# ist für den Entwickler gut bekannt.
- Windows Forms ermöglicht eine schnelle lokale Desktop-App.
- SQLite ist für eine Einzelplatz-Offlinedatenbank ausreichend.
- Direkte SQL-Repositories sind für das kleine Datenmodell leichter nachvollziehbar als ein ORM.

## 4. Projektstruktur

Die erste Version wird bewusst einfach gehalten, aber logisch gegliedert.

Empfohlene Struktur:

```text
TaskHostLocal.WinForms/
├── Program.cs
├── MainForm.cs
├── Forms/
│   ├── TaskEditForm.cs
│   └── ListEditForm.cs
├── Models/
│   ├── TaskItem.cs
│   └── TaskList.cs
├── Services/
│   ├── BackupService.cs
│   ├── TaskService.cs
│   └── ListService.cs
├── Repositories/
│   ├── TaskRepository.cs
│   └── ListRepository.cs
└── Database/
    ├── DbConnectionFactory.cs
    └── DatabaseInitializer.cs
```

## 5. Schichtenmodell

Die Anwendung folgt einem einfachen Schichtenmodell:

```text
Forms / UI
   ↓
Services
   ↓
Repositories
   ↓
Database / SQLite
```

### 5.1 Forms / UI

Die Formulare sind für die Darstellung und Benutzerinteraktion zuständig.

Erlaubt:

- Buttons behandeln
- ausgewählte Aufgabe ermitteln
- Dialoge öffnen
- Services aufrufen
- Anzeige aktualisieren

Nicht erlaubt:

- SQL direkt ausführen
- Datenbankverbindungen selbst öffnen
- Geschäftsregeln tief im UI-Code verstecken

### 5.2 Services

Services enthalten einfache Fachlogik und koordinieren Repositories.

Beispiele:

- prüfen, ob ein Listentitel leer ist
- entscheiden, ob eine Liste gelöscht werden darf
- Aufgabe als erledigt/offen setzen
- Backup auslösen

### 5.3 Repositories

Repositories kapseln den konkreten Datenbankzugriff.

Erlaubt:

- SQL-Statements
- Parameterbindung
- Mapping von Datenbankzeilen auf Models

Nicht erlaubt:

- UI-Logik
- MessageBoxen
- Formulare öffnen

### 5.4 Database

Die Database-Schicht stellt Verbindungen bereit und initialisiert die Datenbank.

Aufgaben:

- Datenbankpfad bestimmen
- Ordner anlegen
- SQLite-Verbindung erzeugen
- Tabellen anlegen
- Standarddaten anlegen

## 6. Datenbankinitialisierung

Beim Programmstart soll die Anwendung:

1. den AppData-Ordner bestimmen
2. den Ordner `%AppData%\SASD\TaskHostLocal` anlegen, falls er fehlt
3. die SQLite-Datenbankdatei `taskhost.db` anlegen, falls sie fehlt
4. die benötigten Tabellen anlegen
5. eine Standardliste „Eingang“ anlegen, falls noch keine Liste existiert

## 7. Listenverwaltung – Umsetzung

### 7.1 Liste anzeigen

Die linke Navigation lädt alle Listen aus `task_lists`.

Zusätzlich können später Smart Views wie Heute, Woche, Favoriten und Erledigt angezeigt werden.

### 7.2 Liste anlegen

Ablauf:

1. Benutzer klickt „Neue Liste“.
2. `ListEditForm` öffnet sich.
3. Benutzer gibt Namen ein.
4. `ListService` validiert den Namen.
5. `ListRepository` speichert den Datensatz.
6. UI lädt Listen neu.

### 7.3 Liste umbenennen

Ablauf:

1. Benutzer wählt Liste aus.
2. Benutzer klickt „Umbenennen“.
3. Dialog zeigt aktuellen Namen.
4. Änderung wird gespeichert.
5. UI wird aktualisiert.

### 7.4 Liste löschen

Für das MVP wird empfohlen:

- Liste darf nur gelöscht werden, wenn sie keine Aufgaben enthält.
- Alternativ muss eine explizite Sicherheitsabfrage erfolgen.

Die sicherere Variante für V1:

> Listen mit Aufgaben werden zunächst nicht gelöscht.

## 8. Aufgabenverwaltung – Umsetzung

### 8.1 Aufgabe anlegen

Ablauf:

1. Benutzer wählt Liste aus.
2. Benutzer klickt „Neue Aufgabe“.
3. `TaskEditForm` öffnet sich.
4. Benutzer erfasst Titel, Notiz, Fälligkeit und Priorität.
5. `TaskService` validiert die Eingaben.
6. `TaskRepository` speichert die Aufgabe.
7. UI lädt Aufgaben neu.

### 8.2 Aufgabe bearbeiten

Ablauf:

1. Benutzer wählt Aufgabe aus.
2. Benutzer klickt „Bearbeiten“ oder öffnet Details.
3. Bestehende Daten werden angezeigt.
4. Änderungen werden gespeichert.
5. UI wird aktualisiert.

### 8.3 Aufgabe löschen

Ablauf:

1. Benutzer wählt Aufgabe aus.
2. Benutzer klickt „Löschen“.
3. Sicherheitsabfrage erscheint.
4. Bei Bestätigung wird die Aufgabe gelöscht.
5. UI wird aktualisiert.

### 8.4 Aufgabe erledigt/offen setzen

Ablauf:

1. Benutzer wählt Aufgabe aus.
2. Benutzer klickt „Erledigt“ oder „Offen“.
3. `is_completed` wird gesetzt.
4. `completed_at` wird gepflegt.
5. UI wird aktualisiert.

## 9. Suche – Umsetzung

Die Suche wird zunächst einfach umgesetzt.

Suchlogik:

- Suche im Titel
- Suche in der Beschreibung/Notiz
- Groß-/Kleinschreibung soll für den Nutzer keine Rolle spielen

Technisch kann dies mit `LIKE` und Parametern erfolgen.

Wichtig:

- Suchbegriffe dürfen nicht per Stringverkettung ungeschützt in SQL eingefügt werden.
- Es müssen SQLite-Parameter verwendet werden.

## 10. Backup – Umsetzung

Die Backupfunktion kopiert die SQLite-Datei an einen vom Nutzer gewählten oder automatisch bestimmten Zielort.

Empfohlenes Namensschema:

```text
taskhost-backup-YYYY-MM-DDTHHmmss.db
```

Beispiel:

```text
taskhost-backup-2026-05-13T084331.db
```

## 11. UI-Umsetzung im MVP

Die erste UI darf technisch einfacher sein. Trotzdem ist folgende Richtung festgelegt:

- Menüleiste bleibt erhalten.
- Toolbar bleibt erhalten.
- linke Navigation bleibt erhalten.
- Aufgabenliste wird mittig angezeigt.
- Detailbereich rechts wird als Zielbild vorgesehen.
- Aufgaben sollen langfristig eher als listenartige Zeilen/Karten erscheinen, nicht als überladene technische Tabelle.

Für die erste lauffähige Version darf eine `DataGridView` genutzt werden. Für eine bessere Nutzererfahrung soll später eine eigene Darstellung mit `Panel`, `UserControl` oder Owner Draw geprüft werden.

## 12. Fehlerbehandlung

Fehler sollen dem Benutzer verständlich angezeigt werden.

Beispiel:

- Benutzertext: „Die Aufgaben konnten nicht geladen werden.“
- Detailtext: technische Fehlermeldung, z. B. SQLite-Fehler

Technische Fehler sollen möglichst so dokumentiert werden, dass sie später reproduzierbar sind.

## 13. Bekannter Startfehler

Im aktuellen Stand tritt beim Laden der Aufgaben folgender Fehler auf:

```text
SQLite Error 1: near "=": syntax error.
```

Bewertung:

- Build ist erfolgreich.
- App startet.
- Fehler entsteht vermutlich in einer SQL-Abfrage in `TaskRepository`.
- Der Fehler blockiert das Laden der Aufgaben.

Dieser Fehler wird in `080_Known_Issues.md` dokumentiert und später vor der Weiterentwicklung behoben.

## 14. Sicherheits- und Datenschutzanforderungen

Für das MVP gelten einfache, aber wichtige Regeln:

- keine echten privaten Aufgaben in das Repository einchecken
- keine SQLite-Datenbanken einchecken
- keine Backup-Dateien einchecken
- keine Zugangsdaten einchecken
- keine Telemetrie
- keine Netzwerkkommunikation

Da das Projekt öffentlich auf GitHub liegt, ist besondere Vorsicht geboten.

## 15. Abnahmekriterien für das MVP

Das MVP kann als erste arbeitsfähige Version akzeptiert werden, wenn:

- [ ] die App ohne Laufzeitfehler startet
- [ ] die Datenbank automatisch angelegt wird
- [ ] eine Standardliste vorhanden ist
- [ ] Listen angelegt, geändert und gelöscht werden können
- [ ] Aufgaben angelegt, geändert und gelöscht werden können
- [ ] Aufgaben erledigt/offen gesetzt werden können
- [ ] Fälligkeit, Priorität und Notiz gespeichert werden
- [ ] Aufgaben nach Neustart erhalten bleiben
- [ ] Suche funktioniert
- [ ] Backup funktioniert
- [ ] keine privaten Daten im Repository enthalten sind

