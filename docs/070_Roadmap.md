# TaskHost Local – Roadmap

**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13  
**Bezug:** Lastenheft/Pflichtenheft MVP v0.2

## 1. Zweck des Dokuments

Dieses Dokument beschreibt eine pragmatische Roadmap für **TaskHost Local**.

Die Roadmap ist nicht als starres Versprechen zu verstehen, sondern als Orientierung, damit die Entwicklung nicht zu früh zu breit wird.

## 2. Entwicklungsprinzip

Die Entwicklung folgt dem Prinzip:

> Erst arbeitsfähig, dann angenehmer, dann erweiterbar, dann integrierbar.

Das bedeutet:

1. Grundfunktionen stabilisieren.
2. UI alltagstauglicher machen.
3. Smart Views ergänzen.
4. Details wie Unteraufgaben, Favoriten und Export/Import ergänzen.
5. Spätere TaskHost-Kompatibilität vorbereiten.
6. Erst danach über Sync/API nachdenken.

## 3. v0.1.0 – Initialer Stand

**Status:** begonnen / im Repository vorhanden

Enthalten:

- Repository angelegt,
- erste WinForms-App,
- SQLite-Anbindung,
- Ordnerstruktur,
- Listen-/Aufgaben-Grundcode,
- Build erfolgreich,
- initiale Dokumentation.

Bekanntes Problem:

- SQLite-Laufzeitfehler beim Laden der Aufgaben.

Ziel für v0.1.x:

- Laufzeitfehler beheben,
- Anwendung stabil starten,
- Grunddaten laden.

## 4. v0.1.1 – Stabilisierung des Startzustands

### Ziele

- SQLite-Syntaxfehler beheben,
- Datenbankinitialisierung prüfen,
- Standardliste „Eingang“ korrekt erzeugen,
- leere Aufgabenliste fehlerfrei anzeigen,
- Fehlermeldungen verbessern,
- manuelle Startprüfung dokumentieren.

### Abnahmekriterien

- App startet ohne Fehlerdialog.
- Datenbank wird erzeugt.
- Standardliste „Eingang“ wird angezeigt.
- Aufgabenliste kann leer sein, ohne Fehler zu erzeugen.
- Anwendung benötigt keine Internetverbindung.

## 5. v0.2.0 – Arbeitsfähiges CRUD-MVP

### Ziele

- Listen anlegen,
- Listen umbenennen,
- leere Listen löschen,
- Listen mit Aufgaben im MVP nicht löschen,
- Aufgaben anlegen,
- Aufgaben bearbeiten,
- Aufgaben nach Sicherheitsabfrage löschen,
- Aufgaben erledigt/offen setzen,
- Fälligkeit, Priorität und Notiz speichern,
- Suche funktionsfähig machen,
- Backupfunktion funktionsfähig machen,
- Menü und Toolbar vollständig nutzbar machen.

### Abnahmekriterien

- Aufgaben bleiben nach Neustart erhalten.
- Listen bleiben nach Neustart erhalten.
- Suche findet Aufgaben über Titel.
- Suche findet Aufgaben über Notiz/Beschreibung.
- Backup erzeugt eine nachvollziehbar benannte Datei.
- Leere Liste kann gelöscht werden.
- Liste mit Aufgaben wird nicht gelöscht.
- Aufgabe kann erledigt und wieder geöffnet werden.
- `completed_at` wird beim Wiederöffnen zurückgesetzt.

## 6. v0.3.0 – UI-Verbesserung Richtung Aufgaben-App

### Ziele

- Aufgabenliste weniger tabellarisch gestalten,
- Fälligkeit unter dem Aufgabentitel anzeigen,
- kurze Notiz/Vorschau unter dem Titel anzeigen,
- Priorität besser sichtbar machen,
- rechter Detailbereich einführen,
- Menü und Toolbar beibehalten,
- einfache Leerzustände anzeigen.

### Abnahmekriterien

- Aufgabenliste ist besser lesbar.
- Details werden separat angezeigt.
- Aufgabenliste bleibt übersichtlich.
- Lange Notizen überfrachten die Aufgabenliste nicht.
- Auswahl einer Aufgabe aktualisiert den Detailbereich.

## 7. v0.4.0 – Smart Views

### Ziele

- Alle-Aufgaben-Ansicht,
- Heute-Ansicht,
- Überfällig-Ansicht,
- Erledigt-Ansicht,
- optional Woche-Ansicht,
- Zähler in linker Navigation prüfen.

### Abnahmekriterien

- Alle Aufgaben zeigt Aufgaben über Listen hinweg.
- Heute zeigt fällige Aufgaben des aktuellen Tages.
- Überfällig zeigt offene Aufgaben mit vergangenem Fälligkeitsdatum.
- Erledigt zeigt abgeschlossene Aufgaben.
- Woche zeigt Aufgaben der aktuellen Woche, falls umgesetzt.

## 8. v0.5.0 – Favoriten und bessere Aufgabenzeilen

### Ziele

- Feld `is_starred` einführen,
- Favorit/Stern in Aufgabenzeile anzeigen,
- Favoriten-Smart-View ergänzen,
- Aufgabenzeile weiter verbessern.

### Abnahmekriterien

- Aufgabe kann als Favorit markiert werden.
- Favorit bleibt nach Neustart erhalten.
- Favoriten-Ansicht zeigt markierte Aufgaben.
- Favorit/Stern ist in Aufgabenliste oder Detailbereich sichtbar.

## 9. v0.6.0 – Unteraufgaben und Detailbereich-Ausbau

### Ziele

- Unteraufgaben/Checklisten,
- Fortschritt im Detailbereich,
- bessere Notizen,
- ggf. einfache Tags prüfen.

### Abnahmekriterien

- Aufgabe kann Unteraufgaben haben.
- Unteraufgaben können erledigt/offen gesetzt werden.
- Detailbereich zeigt Fortschritt.
- Unteraufgaben überladen die Hauptliste nicht.

## 10. v0.7.0 – Export, Import und Backup verbessern

### Ziele

- JSON-Export,
- JSON-Import,
- TaskHost-kompatibles Austauschformat prüfen,
- Backup komfortabler machen,
- Restore-Strategie prüfen.

### Abnahmekriterien

- Daten können exportiert werden.
- Export kann wieder importiert werden.
- Datenbankbackup ist leicht auffindbar.
- Export enthält keine unnötigen lokalen Pfade, wenn später Austausch mit TaskHost geplant ist.

## 11. Spätere Versionen

Mögliche spätere Themen:

- TaskHost-API-Adapter,
- optionale Synchronisierung,
- Konfliktbehandlung,
- Benutzerprofile,
- Datenbankverschlüsselung,
- Installer,
- Auto-Update,
- Avalonia-Variante für Windows/Linux,
- Migration in eine größere TaskHost-Monorepo-Struktur.

## 12. Was bewusst nicht früh umgesetzt wird

Folgende Themen sollen nicht vorzeitig begonnen werden:

- Cloud-Sync,
- Collaboration,
- Benutzerverwaltung,
- mobile Apps,
- komplexe Rechteverwaltung,
- Kalenderintegration,
- vollständige Wiederholungslogik,
- Hintergrunddienst,
- Netzwerkkommunikation,
- TaskHost-API-Anbindung.

Begründung:

Diese Themen würden die lokale Nutzbarkeit verzögern und gehören eher zur langfristigen TaskHost-Plattform.

## 13. Priorisierte nächste Arbeitsschritte

1. SQLite-Fehler beheben.
2. App ohne Fehler starten lassen.
3. CRUD-MVP stabilisieren.
4. Backup prüfen.
5. UI-Verbesserungen Richtung Aufgabenliste und Detailbereich beginnen.
6. README-Screenshot mit fiktiven Daten ergänzen.
7. GitHub Issues aus Roadmap und Known Issues ableiten.
