# SASD TaskHost Local

**TaskHost Local** ist eine lokale Windows-Aufgabenverwaltung für die TaskHost-Projektfamilie. Das Projekt ist bewusst pragmatisch angelegt: Es soll schnell arbeitsfähig werden, lokal ohne Server funktionieren und trotzdem so sauber strukturiert sein, dass spätere Weiterentwicklung, Debugging und eine mögliche Integration in TaskHost nicht verbaut werden.

> English summary: TaskHost Local is a small local Windows task management application built with C#, Windows Forms and SQLite. It is part of the TaskHost project family, but the MVP is intentionally offline-only and does not include cloud sync, collaboration or a TaskHost API connection.

## Projektstatus

| Bereich | Status |
|---|---|
| Repository | angelegt |
| Grundprojekt | vorhanden |
| Build | erfolgreich |
| Anwendung | startet, aber aktueller Laufzeitfehler bekannt |
| Dokumentation | initial aufgebaut, Lastenheft/Pflichtenheft geschärft |
| Lizenz | noch nicht festgelegt |
| Screenshot | noch zu ergänzen |

Aktueller bekannter Startfehler:

```text
SQLite Error 1: near "=": syntax error.
```

Der Fehler ist in `docs/080_Known_Issues.md` dokumentiert und soll im Code-Chat bzw. in der Code-Weiterentwicklung gezielt behoben werden.

## Ziel des MVP

Das MVP soll eine einfache, lokale und alltagstaugliche Aufgabenverwaltung bereitstellen.

Der Fokus liegt auf:

- lokalen Aufgabenlisten,
- Aufgaben mit Titel, Notiz, Fälligkeit und Priorität,
- erledigt/offen-Status,
- einfacher Suche,
- lokaler SQLite-Speicherung,
- einfacher Datenbanksicherung,
- klassischer Windows-Bedienung mit Menüleiste und Toolbar.

Das Projekt soll **nicht** durch Cloud, Synchronisierung, Login, Benutzerverwaltung oder Collaboration verlangsamt werden.

## Nicht-Ziele des MVP

Nicht Bestandteil der ersten Version:

- Cloud-Synchronisierung,
- Multi-Geräte-Synchronisierung,
- geteilte Listen,
- Benutzerkonten,
- Rechteverwaltung,
- Kommentare oder Collaboration,
- mobile Apps,
- aktive Windows-Benachrichtigungen,
- Hintergrunddienst,
- komplexe Wiederholungslogik,
- direkte TaskHost-API-Anbindung,
- Telemetrie,
- automatische Update-Prüfung,
- Netzwerkkommunikation zur Laufzeit.

## Verhältnis zu TaskHost

TaskHost Local ist **kein konkurrierendes Produkt** zum bestehenden TaskHost-Projekt.

Die strategische Einordnung lautet:

> TaskHost Local ist eine eigenständige lokale Windows-Aufgabenverwaltung, die kurzfristig produktiv nutzbar werden soll und langfristig als möglicher Desktop- oder Offline-Client der TaskHost-Produktfamilie vorbereitet wird.

Für das MVP bedeutet das:

- TaskHost Local arbeitet eigenständig.
- Es gibt keine API-Anbindung.
- Es gibt keinen Login.
- Es gibt keine Synchronisierung.
- Die Begriffe und Datenstrukturen sollen dennoch kompatibel genug bleiben, damit Migration, Export/Import oder spätere API-Anbindung nicht unnötig erschwert werden.

## Technik

| Bereich | Entscheidung |
|---|---|
| Sprache | C# |
| Framework | .NET 8 Windows |
| Oberfläche | Windows Forms |
| Datenbank | SQLite |
| Datenbankbibliothek | Microsoft.Data.Sqlite |
| ORM | keines |
| Zielplattform MVP | Windows |
| Architektur | einfache Schichtung: Forms → Services → Repositories → Database |

## Start in Visual Studio

1. Visual Studio öffnen.
2. `TaskHostLocal.sln` öffnen.
3. NuGet-Pakete wiederherstellen, falls Visual Studio dies nicht automatisch erledigt.
4. Projekt `TaskHostLocal.WinForms` starten.

Voraussetzung:

- Workload **.NET-Desktopentwicklung**.

## Start per Kommandozeile

```powershell
dotnet restore
dotnet build
dotnet run --project .\TaskHostLocal.WinForms\TaskHostLocal.WinForms.csproj
```

Da Windows Forms verwendet wird, sollte die Anwendung unter Windows gestartet werden. Ein Build aus WSL kann funktionieren, der eigentliche Programmstart sollte aber bevorzugt in einer Windows-Umgebung erfolgen.

## Speicherort der Datenbank

Die lokale SQLite-Datenbank liegt absichtlich nicht im Programmverzeichnis, sondern im Benutzerprofil:

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
- Daten liegen benutzerspezifisch,
- Backups können nachvollziehbar erstellt werden.

## Architekturüberblick

```text
TaskHostLocal.WinForms
├── Forms/          Dialoge und zusätzliche Fenster
├── Models/         einfache Datenmodelle
├── Services/       fachliche Operationen und Koordination
├── Repositories/   SQLite-Zugriff mit parametrisierten SQL-Abfragen
├── Database/       Datenbankpfad, Verbindung, Initialisierung
├── MainForm.cs     Hauptfenster und UI-Koordination
└── Program.cs      Anwendungseinstieg
```

Wichtige Architekturregel:

> SQL gehört nicht in Formularcode. Formulare rufen Services auf, Services verwenden Repositories, Repositories kapseln SQLite.

## Dokumentation

Wichtige Dokumente:

| Dokument | Zweck |
|---|---|
| `docs/000_Project_Overview.md` | Projektüberblick |
| `docs/010_Strategic_Positioning.md` | strategische Einordnung und Verhältnis zu TaskHost |
| `docs/020_Lastenheft_MVP.md` | fachliche Anforderungen an das MVP |
| `docs/030_Pflichtenheft_MVP.md` | technische Umsetzung des MVP |
| `docs/040_UI_Concept.md` | UI-Richtung und Zielbild |
| `docs/050_Technical_Design.md` | technische Architektur |
| `docs/060_Data_Model.md` | SQLite-Datenmodell und Erweiterungen |
| `docs/070_Roadmap.md` | Entwicklungsplanung |
| `docs/080_Known_Issues.md` | bekannte Fehler und Risiken |
| `docs/090_Documentation_Checklist.md` | Dokumentations- und Projektcheckliste |
| `docs/100_Manual_Test_Plan.md` | manueller Testplan für MVP-Abnahme |
| `docs/adr/` | Architekturentscheidungen |

## Datenschutz und lokale Daten

Aufgaben können private oder geschäftliche Informationen enthalten. Deshalb gilt:

- keine echten Aufgaben in Screenshots,
- keine `.db`-Dateien ins Repository,
- keine Backup-Dateien ins Repository,
- keine Zugangsdaten oder Secrets ins Repository,
- keine Telemetrie im MVP,
- keine Netzwerkkommunikation im MVP.

## Nächste technische Schritte

1. SQLite-Startfehler beheben.
2. App ohne Fehlerdialog starten lassen.
3. Standardliste und leere Aufgabenliste prüfen.
4. Listen-CRUD stabilisieren.
5. Aufgaben-CRUD stabilisieren.
6. Backup-Funktion prüfen.
7. README-Screenshot mit fiktiven Daten ergänzen.
8. GitHub Issues aus Known Issues und Roadmap ableiten.

## Lizenz

Die Lizenzentscheidung ist noch offen.

Solange keine `LICENSE`-Datei vorhanden ist, gilt reguläres Urheberrecht. Eine spätere Veröffentlichung unter MIT License ist möglich, sollte aber bewusst entschieden werden.
