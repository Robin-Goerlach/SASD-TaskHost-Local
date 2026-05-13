# TaskHost Local – Projektübersicht

**Projektname:** SASD TaskHost Local  
**Repository:** `Robin-Goerlach/SASD-TaskHost-Local`  
**Projektfamilie:** TaskHost  
**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13  
**Sprache:** Deutsch

## 1. Zweck dieses Dokuments

Dieses Dokument gibt einen kompakten, aber belastbaren Überblick über **TaskHost Local**.

Es soll neuen Lesern, späteren Mitwirkenden und dem zukünftigen Ich des Entwicklers schnell erklären:

- warum dieses Projekt existiert,
- wie es zum bestehenden TaskHost-Projekt gehört,
- welche Funktionen kurzfristig wichtig sind,
- welche Funktionen bewusst nicht Teil des MVP sind,
- welche technische und strategische Richtung verfolgt wird.

Detailentscheidungen werden in separaten Dokumenten beschrieben.

## 2. Dokumentenlandkarte

| Dokument | Inhalt |
|---|---|
| `010_Strategic_Positioning.md` | strategische Einordnung und Verhältnis zu TaskHost |
| `020_Lastenheft_MVP.md` | fachliche Anforderungen an das MVP |
| `030_Pflichtenheft_MVP.md` | technische Umsetzung des MVP |
| `040_UI_Concept.md` | UI-Richtung, Menü, Toolbar, Aufgabenliste, Detailbereich |
| `050_Technical_Design.md` | Architektur, Technologie und technische Regeln |
| `060_Data_Model.md` | SQLite-Datenmodell, Smart Views, Erweiterbarkeit |
| `070_Roadmap.md` | Entwicklungsplanung nach Versionen |
| `080_Known_Issues.md` | bekannte Fehler, Risiken und offene Punkte |
| `090_Documentation_Checklist.md` | Dokumentations- und Projektcheckliste |
| `100_Manual_Test_Plan.md` | manueller Testplan für die MVP-Abnahme |
| `adr/` | Architecture Decision Records |

## 3. Kurzbeschreibung

**TaskHost Local** ist eine lokale Windows-Aufgabenverwaltung, entwickelt mit **C#**, **Windows Forms** und **SQLite**.

Die Anwendung soll kurzfristig eine arbeitsfähige, offline nutzbare Lösung zur Verwaltung von Aufgaben, Listen, Fälligkeiten, Notizen und Prioritäten bereitstellen.

Das Projekt ist bewusst pragmatisch angelegt:

> Funktion vor Schönheit, aber mit sauberer Struktur.

Der Code soll nicht als Wegwerf-Prototyp entstehen. Auch wenn die erste Oberfläche schlicht sein darf, sollen Architektur, Dateistruktur und Benennung so gestaltet werden, dass spätere Erweiterungen, Debugging und eine mögliche Integration in die TaskHost-Produktfamilie möglich bleiben.

## 4. Hintergrund

Es existiert bereits das Projekt **TaskHost**. Dieses ist langfristig als umfangreichere Aufgabenverwaltungsplattform denkbar, zum Beispiel mit:

- Web-App,
- REST-API,
- serverbasierter Datenhaltung,
- Authentifizierung,
- späterer Synchronisierung,
- optionaler Collaboration.

Der unmittelbare Bedarf ist aber einfacher und dringender:

> Eine schnell nutzbare lokale Aufgabenverwaltung unter Windows, die ohne Server, ohne Cloud, ohne Login und ohne Synchronisation funktioniert.

TaskHost Local entsteht deshalb als lokale Zwischen- und Arbeitslösung. Es soll TaskHost nicht ersetzen, sondern kurzfristig nutzbare Funktionalität bereitstellen und langfristig eventuell als Desktop-/Offline-Client dienen.

## 5. Strategische Leitlinie

Die strategische Leitlinie lautet:

> **TaskHost Local ist eine eigenständige lokale Windows-Aufgabenverwaltung, die kurzfristig produktiv nutzbar werden soll und langfristig als möglicher Desktop- oder Offline-Client der TaskHost-Produktfamilie vorbereitet wird.**

Daraus folgen drei Grundsätze:

1. **TaskHost bleibt das langfristige Hauptprodukt.**
2. **TaskHost Local wird zunächst eigenständig entwickelt.**
3. **TaskHost Local soll begrifflich und fachlich kompatibel zur TaskHost-Produktfamilie bleiben.**

## 6. Abgrenzung zu Wunderlist und ähnlichen Anwendungen

TaskHost Local orientiert sich funktional an bewährten Bedienkonzepten klassischer Aufgabenlisten-Anwendungen:

- Listen links,
- Aufgaben in der Mitte,
- Details bei Auswahl einer Aufgabe,
- einfache Fälligkeiten,
- klare Erledigt-/Offen-Logik,
- schnelle Erfassung.

TaskHost Local ist jedoch **kein Wunderlist-Klon** im rechtlichen oder gestalterischen Sinn.

Es werden nicht übernommen:

- fremde Marken,
- fremde Logos,
- fremde Icons,
- geschützte Texte,
- exakte UI-Gestaltung,
- irreführende Produktbezeichnungen.

Die Formulierung „inspiriert von klassischen Aufgabenlisten-Anwendungen“ ist angemessener als „Clone“.

## 7. Aktueller Projektstand

Aktueller Stand nach den ersten Commits:

- Repository wurde angelegt.
- Erstes C# WinForms/SQLite-Projekt wurde eingecheckt.
- Dateiberechtigungen wurden normalisiert.
- Build ist erfolgreich.
- Anwendung startet grundsätzlich.
- Beim Laden der Aufgaben tritt ein SQLite-Syntaxfehler auf.
- Initiale Dokumentation wurde angelegt.
- Lastenheft und Pflichtenheft wurden auf v0.2 geschärft.
- Restliche Dokumentation wurde auf dieselbe Richtung ausgerichtet.

Der bekannte Laufzeitfehler wird in `080_Known_Issues.md` geführt.

## 8. Ziele der ersten arbeitsfähigen Version

Die erste arbeitsfähige Version soll ermöglichen:

- Anwendung starten ohne Fehlerdialog,
- Standardliste „Eingang“ anzeigen,
- eigene Listen anlegen, umbenennen und löschen,
- Aufgaben anlegen, bearbeiten, löschen,
- Aufgaben als erledigt/offen markieren,
- Titel, Notiz, Fälligkeitsdatum und Priorität speichern,
- Aufgaben nach Neustart wiederfinden,
- Aufgaben suchen,
- lokale SQLite-Datenbank sichern.

## 9. Nicht-Ziele der ersten Version

TaskHost Local V1 soll bewusst **nicht** enthalten:

- Cloud-Synchronisierung,
- Multi-Geräte-Synchronisierung,
- gemeinsame Listen,
- Benutzerkonten,
- Rechteverwaltung,
- Kommentare / Collaboration,
- mobile Apps,
- Push-Benachrichtigungen,
- Hintergrunddienst,
- komplexe Wiederholungslogik,
- vollständige TaskHost-API-Integration,
- Telemetrie,
- automatische Update-Prüfung,
- Netzwerkkommunikation zur Laufzeit.

Diese Punkte können später erneut bewertet werden.

## 10. Entwicklungsprinzipien

Für dieses Projekt gelten folgende Arbeitsprinzipien:

- schnell zu einer arbeitsfähigen Version kommen,
- Code sauber strukturieren,
- keine SQL-Logik direkt in Formularen,
- verständliche Kommentare verwenden,
- einfache Architektur bevorzugen,
- keine unnötige Framework-Komplexität,
- spätere Integration nicht verbauen,
- bekannte Fehler dokumentieren statt ignorieren,
- öffentliche Repository-Nutzung ohne private Daten oder Secrets,
- strategische Entscheidungen in Dokumentation und ADRs festhalten.

## 11. Aktuelle nächste Schritte

1. SQLite-Laufzeitfehler beheben.
2. Grundstart ohne Fehlerdialog erreichen.
3. CRUD-Funktionen manuell prüfen.
4. Backup-Funktion prüfen.
5. README-Screenshot mit fiktiven Daten ergänzen.
6. GitHub Issues aus Known Issues und Roadmap ableiten.
7. UI schrittweise Richtung Aufgabenlisten-/Detailbereich verbessern.
