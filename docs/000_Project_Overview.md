# TaskHost Local – Projektübersicht

**Projektname:** SASD TaskHost Local  
**Repository:** `Robin-Goerlach/SASD-TaskHost-Local`  
**Projektfamilie:** TaskHost  
**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  
**Sprache:** Deutsch  

## 1. Zweck dieses Dokuments

Dieses Dokument gibt einen kompakten Überblick über **TaskHost Local**. Es soll neuen Lesern, späteren Mitwirkenden und dem zukünftigen Ich des Entwicklers schnell erklären, warum dieses Projekt existiert, wie es zu **TaskHost** gehört und welche Richtung aktuell verfolgt wird.

Die Detailentscheidungen werden in separaten Dokumenten beschrieben:

- `010_Strategic_Positioning.md`
- `020_Lastenheft_MVP.md`
- `030_Pflichtenheft_MVP.md`
- `040_UI_Concept.md`
- `050_Technical_Design.md`
- `060_Data_Model.md`
- `070_Roadmap.md`
- `080_Known_Issues.md`
- `090_Documentation_Checklist.md`
- `adr/`

## 2. Kurzbeschreibung

**TaskHost Local** ist eine lokale Windows-Aufgabenverwaltung, entwickelt mit **C#**, **Windows Forms** und **SQLite**. Die Anwendung soll kurzfristig eine arbeitsfähige, offline nutzbare Lösung zur Verwaltung von Aufgaben, Listen, Fälligkeiten, Notizen und Prioritäten bereitstellen.

Das Projekt ist bewusst pragmatisch angelegt: **Funktion vor Schönheit**, aber mit einer sauberen Struktur, damit spätere Erweiterungen, Debugging und eine mögliche Integration in die TaskHost-Produktfamilie möglich bleiben.

## 3. Hintergrund

Es existiert bereits das Projekt **TaskHost**, das langfristig als Web-/API-System mit weitergehenden Funktionen wie Backend, Weboberfläche, später ggf. Synchronisierung, Mehrbenutzerbetrieb oder Collaboration entwickelt werden soll.

Der unmittelbare Bedarf ist jedoch einfacher:

> Eine schnell nutzbare lokale Aufgabenverwaltung unter Windows, die ohne Server, ohne Cloud, ohne Login und ohne Synchronisation funktioniert.

TaskHost Local entsteht deshalb als lokale Zwischenlösung, soll aber nicht gegen TaskHost konkurrieren.

## 4. Strategische Einordnung

Die strategische Leitlinie lautet:

> **TaskHost Local ist eine eigenständige lokale Windows-Aufgabenverwaltung, die kurzfristig produktiv nutzbar werden soll und langfristig als möglicher Desktop-Client der TaskHost-Produktfamilie vorbereitet wird.**

Daraus folgen drei Grundsätze:

1. **TaskHost bleibt das langfristige Hauptprodukt.**
2. **TaskHost Local wird zunächst eigenständig entwickelt.**
3. **TaskHost Local soll begrifflich und fachlich kompatibel zur TaskHost-Produktfamilie bleiben.**

## 5. Abgrenzung zu Wunderlist

TaskHost Local orientiert sich an bewährten Bedienkonzepten klassischer Aufgabenlisten-Anwendungen, insbesondere an einer klaren Listen-/Aufgaben-/Detailstruktur.

TaskHost Local ist jedoch **kein Wunderlist-Klon** im rechtlichen oder gestalterischen Sinn. Es werden keine Marken, Logos, Texte, Icons oder geschützten Designelemente übernommen. Die Oberfläche soll funktional inspiriert, aber eigenständig gestaltet werden.

## 6. Aktueller Projektstand

Aktueller technischer Stand nach dem ersten Commit:

- Repository wurde angelegt.
- Erstes C# WinForms/SQLite-Projekt wurde eingecheckt.
- Build ist erfolgreich.
- Anwendung startet.
- Zur Laufzeit tritt beim Laden der Aufgaben ein SQLite-Syntaxfehler auf.
- Dateiberechtigungen wurden normalisiert (`100755` → `100644`).

Der Laufzeitfehler wird dokumentiert und später gezielt behoben.

## 7. Nicht-Ziele der ersten Version

TaskHost Local V1 soll bewusst **nicht** enthalten:

- Cloud-Synchronisierung
- Multi-Geräte-Synchronisierung
- gemeinsame Listen
- Benutzerkonten
- Rechteverwaltung
- Kommentare / Collaboration
- mobile Apps
- Push-Benachrichtigungen
- komplexe Wiederholungslogik
- vollständige TaskHost-API-Integration

Diese Punkte können später erneut bewertet werden.

## 8. Entwicklungsprinzipien

Für dieses Projekt gelten folgende Arbeitsprinzipien:

- schnell zu einer arbeitsfähigen Version kommen
- Code sauber strukturieren
- keine SQL-Logik direkt in Formularen
- verständliche Kommentare verwenden
- einfache, nachvollziehbare Architektur bevorzugen
- keine unnötige Framework-Komplexität
- spätere Integration nicht verbauen
- bekannte Fehler dokumentieren statt ignorieren
- öffentliche Repository-Nutzung ohne private Daten oder Secrets

