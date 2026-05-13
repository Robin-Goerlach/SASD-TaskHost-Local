# TaskHost Local v0.1.0

Eine kleine lokale Aufgabenverwaltung für Windows.

Technik:

- C#
- .NET 8
- Windows Forms
- SQLite über `Microsoft.Data.Sqlite`
- kein Entity Framework
- keine Cloud
- keine Synchronisierung

## Ziel dieser Version

Diese Version ist bewusst schlicht. Funktion geht vor Schönheit.

Enthalten:

- Listen anzeigen, anlegen, umbenennen, löschen
- Aufgaben anzeigen, anlegen, bearbeiten, löschen
- Aufgaben als erledigt/offen markieren
- Titel, Notiz, Fälligkeitsdatum, Priorität
- Suche über Titel und Notiz
- automatische SQLite-Datenbank unter `%AppData%\SASD\TaskHostLocal\taskhost.db`
- einfache Datenbanksicherung

Nicht enthalten:

- Cloud-Synchronisierung
- geteilte Listen
- Benutzerverwaltung
- Anhänge
- Wiederholungen
- Erinnerungen
- Tags
- schönes UI-Design

## Start in Visual Studio

1. Visual Studio öffnen.
2. `TaskHostLocal.sln` öffnen.
3. Falls nötig NuGet-Pakete wiederherstellen.
4. Projekt `TaskHostLocal.WinForms` starten.

Voraussetzung: Workload **.NET-Desktopentwicklung**.

## Start per Kommandozeile

```powershell
dotnet restore

dotnet run --project .\TaskHostLocal.WinForms\TaskHostLocal.WinForms.csproj
```

## Speicherort der Datenbank

Die Datenbank liegt absichtlich nicht im Programmverzeichnis, sondern im Benutzerprofil:

```text
%AppData%\SASD\TaskHostLocal\taskhost.db
```

Das ist wichtig, weil Programme unter `C:\Program Files` normalerweise nicht einfach schreiben dürfen.

## Architektur

Die Anwendung ist klein, aber bereits getrennt:

```text
MainForm / Forms     -> Oberfläche
Services             -> Fachlogik
Repositories         -> SQLite-Zugriff
Database             -> Verbindung und Initialisierung
Models               -> Datenmodelle
```

Wichtig: SQL steht nicht direkt im Formularcode. Das erleichtert spätere Weiterentwicklung und Debugging.

## Nächste sinnvolle Schritte

- Aufgaben nach Fälligkeit einfärben
- "Heute"- und "Überfällig"-Ansicht
- Papierkorb statt endgültigem Löschen
- JSON-Export/Import
- Unteraufgaben
- Tags
- einfache Erinnerungen, solange die App läuft
