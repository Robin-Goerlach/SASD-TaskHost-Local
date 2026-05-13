# Changelog

Alle nennenswerten Änderungen an **SASD TaskHost Local** sollen in dieser Datei dokumentiert werden.

Das Format orientiert sich pragmatisch an einer einfachen Versionshistorie. Die Versionierung ist vorerst projektintern zu verstehen und wird erst nach einem stabilen lauffähigen Stand als Release-Version relevant.

## Unreleased

### Added

- Projekt als lokales Windows-Forms-/SQLite-Vorhaben angelegt.
- Erste Repository-Struktur erstellt.
- Erste Dokumentationsstruktur aufgebaut.
- Strategische Einordnung zu TaskHost dokumentiert.
- Lastenheft und Pflichtenheft für das MVP geschärft.
- UI-Zielrichtung mit Menü, Toolbar, Aufgabenliste und Detailbereich dokumentiert.
- Datenmodell, Roadmap, Known Issues und manuelle Testplanung ergänzt.

### Changed

- Dateirechte im Repository normalisiert (`100755` → `100644`).
- Dokumentation stärker auf Lastenheft/Pflichtenheft v0.2 ausgerichtet.

### Known Issues

- Beim Programmstart tritt aktuell ein SQLite-Syntaxfehler beim Laden der Aufgaben auf.
- Es existieren noch keine automatisierten Tests.
- README-Screenshot und Lizenzentscheidung fehlen noch.

## v0.1.0 – Initialer Arbeitsstand

### Added

- Erste C# WinForms-Anwendung.
- SQLite-Anbindung über `Microsoft.Data.Sqlite`.
- Grundstruktur mit Forms, Models, Services, Repositories und Database.
- Grundlegende Listen- und Aufgabenlogik.
- Erste README-Datei.

### Status

- Build erfolgreich.
- Anwendung startet, ist aber wegen Laufzeitfehler noch nicht arbeitsfähig.
