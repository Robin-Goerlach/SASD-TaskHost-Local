# TaskHost Local – Roadmap

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck des Dokuments

Dieses Dokument beschreibt eine pragmatische Roadmap für TaskHost Local. Die Roadmap ist nicht als starres Versprechen zu verstehen, sondern als Orientierung, damit die Entwicklung nicht zu früh zu breit wird.

## 2. Entwicklungsprinzip

Die Entwicklung folgt dem Prinzip:

> Erst arbeitsfähig, dann angenehmer, dann erweiterbar, dann integrierbar.

Das bedeutet:

1. Grundfunktionen stabilisieren.
2. UI alltagstauglicher machen.
3. Details wie Unteraufgaben ergänzen.
4. Export/Import und spätere TaskHost-Kompatibilität vorbereiten.
5. Erst danach über Sync/API nachdenken.

## 3. v0.1.0 – Initialer Stand

Status: begonnen

Enthalten:

- Repository angelegt
- erste WinForms-App
- SQLite-Anbindung
- Ordnerstruktur
- Listen-/Aufgaben-Grundcode
- Build erfolgreich

Bekanntes Problem:

- SQLite-Laufzeitfehler beim Laden der Aufgaben

Ziel für v0.1.x:

- Laufzeitfehler beheben
- Anwendung stabil starten
- Grunddaten laden

## 4. v0.1.1 – Stabilisierung des Startzustands

Ziele:

- SQLite-Syntaxfehler beheben
- Datenbankinitialisierung prüfen
- Standardliste korrekt erzeugen
- leere Aufgabenliste fehlerfrei anzeigen
- Fehlermeldungen verbessern

Abnahmekriterien:

- App startet ohne Fehlerdialog.
- Datenbank wird erzeugt.
- Standardliste wird angezeigt.
- Aufgabenliste kann leer sein, ohne Fehler zu erzeugen.

## 5. v0.2.0 – Arbeitsfähiges CRUD-MVP

Ziele:

- Listen anlegen
- Listen umbenennen
- Listen löschen
- Aufgaben anlegen
- Aufgaben bearbeiten
- Aufgaben löschen
- Aufgaben erledigt/offen setzen
- Fälligkeit, Priorität und Notiz speichern
- Suche funktionsfähig machen
- Backupfunktion funktionsfähig machen

Abnahmekriterien:

- Aufgaben bleiben nach Neustart erhalten.
- Listen bleiben nach Neustart erhalten.
- Suche findet Aufgaben.
- Backup erzeugt eine Datei.

## 6. v0.3.0 – UI-Verbesserung Richtung Aufgaben-App

Ziele:

- Aufgabenliste weniger tabellarisch gestalten
- Fälligkeit unter dem Aufgabentitel anzeigen
- kurze Notiz/Vorschau unter dem Titel anzeigen
- Favorit/Stern vorbereiten oder ergänzen
- rechter Detailbereich einführen
- Menü und Toolbar beibehalten

Abnahmekriterien:

- Aufgabenliste ist besser lesbar.
- Details werden separat angezeigt.
- Aufgabenliste bleibt übersichtlich.

## 7. v0.4.0 – Smart Views und Favoriten

Ziele:

- Heute-Ansicht
- Woche-Ansicht
- Erledigt-Ansicht
- Favoriten/Stern
- Zähler in linker Navigation

Abnahmekriterien:

- Heute zeigt fällige Aufgaben des aktuellen Tages.
- Woche zeigt Aufgaben der aktuellen Woche.
- Erledigt zeigt abgeschlossene Aufgaben.
- Favoriten können gesetzt und gefiltert werden.

## 8. v0.5.0 – Unteraufgaben und Detailbereich-Ausbau

Ziele:

- Unteraufgaben/Checklisten
- Fortschritt im Detailbereich
- bessere Notizen
- ggf. einfache Tags prüfen

Abnahmekriterien:

- Aufgabe kann Unteraufgaben haben.
- Unteraufgaben können erledigt/offen gesetzt werden.
- Detailbereich zeigt Fortschritt.

## 9. v0.6.0 – Export, Import und Backup verbessern

Ziele:

- JSON-Export
- JSON-Import
- TaskHost-kompatibles Austauschformat prüfen
- Backup komfortabler machen

Abnahmekriterien:

- Daten können exportiert werden.
- Export kann wieder importiert werden.
- Datenbankbackup ist leicht auffindbar.

## 10. Spätere Versionen

Mögliche spätere Themen:

- TaskHost-API-Adapter
- optionale Synchronisierung
- Konfliktbehandlung
- Benutzerprofile
- Verschlüsselung
- Installer
- Auto-Update
- Avalonia-Variante für Windows/Linux
- Migration in eine größere TaskHost-Monorepo-Struktur

## 11. Was bewusst nicht früh umgesetzt wird

Folgende Themen sollen nicht vorzeitig begonnen werden:

- Cloud-Sync
- Collaboration
- Benutzerverwaltung
- mobile Apps
- komplexe Rechteverwaltung
- Kalenderintegration
- vollständige Wiederholungslogik

Begründung:

Diese Themen würden die lokale Nutzbarkeit verzögern und gehören eher zur langfristigen TaskHost-Plattform.

