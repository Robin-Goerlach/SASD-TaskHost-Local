# TaskHost Local – UI-Konzept

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck des Dokuments

Dieses Dokument beschreibt die gewünschte UI-Richtung für **TaskHost Local**. Es basiert auf den bisherigen Überlegungen, Screenshots und Diskussionen im Projektchat.

Ziel ist nicht, sofort ein perfektes Design zu erstellen, sondern die Richtung festzuhalten, damit die Anwendung nicht in eine falsche UI-Struktur läuft.

## 2. Grundentscheidung

Die Benutzeroberfläche soll zunächst funktional und einfach bleiben, aber langfristig näher an modernen Aufgabenlisten-Anwendungen liegen als an einer reinen Datenbanktabelle.

Besonders wichtig:

- Menü und Toolbar werden als hilfreich angesehen und sollen erhalten bleiben.
- Die Aufgabenliste soll übersichtlich bleiben.
- Aufgabendetails sollen bei Bedarf separat angezeigt werden.
- Die Aufgabenliste soll Platz für Titel, Fälligkeit, Favorit und kurze Zusatzinformationen bieten.
- Ein rechter Detailbereich soll später die Aufgabe ausführlicher anzeigen.

## 3. Grundlayout

Das bevorzugte Zielbild besteht aus drei Hauptbereichen:

```text
+---------------------------------------------------------------+
| Menü / Toolbar                                                |
+-------------------+-----------------------------+-------------+
| Navigation        | Aufgabenliste               | Details     |
| Listen / Views    | aktuelle Ansicht             | Aufgabe     |
+-------------------+-----------------------------+-------------+
| Suche / Status / Backup                                      |
+---------------------------------------------------------------+
```

## 4. Menüleiste

Die Menüleiste soll erhalten bleiben.

Vorgeschlagene Menüs:

```text
Datei
- Datenbank sichern
- Beenden

Listen
- Neue Liste
- Liste umbenennen
- Liste löschen

Aufgaben
- Neue Aufgabe
- Aufgabe bearbeiten
- Aufgabe löschen
- Erledigt setzen
- Offen setzen

Hilfe
- Über TaskHost Local
```

Die Menüleiste ist besonders nützlich, weil sie klassische Windows-Bedienbarkeit bietet und Funktionen auffindbar macht.

## 5. Toolbar

Die Toolbar wird ausdrücklich als positiv bewertet und soll beibehalten werden.

Vorgeschlagene Buttons:

- Neue Liste
- Umbenennen
- Löschen
- Neue Aufgabe
- Bearbeiten
- Löschen
- Erledigt
- Offen
- Datenbank sichern

Die Toolbar sollte nicht überladen werden. Häufig genutzte Funktionen gehören in die Toolbar, seltene Funktionen eher ins Menü.

## 6. Linke Navigation

Die linke Navigation soll zwei Arten von Einträgen enthalten:

### 6.1 Smart Views

Beispiele:

- Eingang
- Heute
- Woche
- Favoriten
- Erledigt

### 6.2 Eigene Listen

Beispiele:

- Arbeit
- Privat
- SASD
- Einkaufsliste
- Projekte

Die linke Navigation soll später optisch klarer werden als eine einfache ListBox. Für V1 kann eine einfache Steuerung genügen.

## 7. Aufgabenliste

Die Aufgabenliste ist der wichtigste Arbeitsbereich.

### 7.1 Problem mit reiner Tabellenansicht

Eine klassische `DataGridView` ist schnell umzusetzen, wirkt aber technisch und nimmt viel Platz für Spalten ein.

Nachteile einer reinen Tabelle:

- viele Details konkurrieren miteinander
- Titel haben wenig Raum
- Notizen und Fälligkeiten wirken wie Datenbankspalten
- die Oberfläche fühlt sich weniger wie eine Aufgaben-App an

### 7.2 Gewünschte spätere Darstellung

Langfristig soll jede Aufgabe eher wie eine Listenzeile oder einfache Karte dargestellt werden.

Eine Aufgabenzeile sollte enthalten können:

```text
[ ]  Aufgabentitel                                      ☆
     Fällig: 15.05.2026 · Projekt: SASD · Priorität: Mittel
```

Mögliche Elemente:

- Checkbox für erledigt/offen
- Titel
- Stern/Favorit
- Fälligkeitsdatum
- Priorität
- Liste/Projekt
- kurze Notiz oder Vorschautext

### 7.3 Auswahlverhalten

Beim Klick auf eine Aufgabe:

- Aufgabe wird in der Liste markiert.
- Details werden rechts angezeigt.
- Bearbeitung kann direkt im Detailbereich oder über Dialog erfolgen.

## 8. Rechter Detailbereich

Der rechte Detailbereich ist ein zentrales Zielbild.

Er soll Details anzeigen, die nicht dauerhaft in der Aufgabenliste stehen müssen.

Mögliche Inhalte:

- Titel
- Erledigt-Checkbox
- Favorit/Stern
- Fälligkeitsdatum
- Priorität
- zugehörige Liste
- Notizen
- später Unteraufgaben/Checkliste
- später Anhänge
- später Kommentare oder Verlauf, falls überhaupt gewünscht

Der Detailbereich kann zunächst nur lesen oder einfache Bearbeitung ermöglichen. Später kann er zum Hauptbearbeitungsbereich werden.

## 9. Warum Details separat anzeigen?

Die Trennung von Liste und Details hat mehrere Vorteile:

- die Aufgabenliste bleibt übersichtlich
- der Aufgabentitel bekommt mehr Platz
- Fälligkeit und Kurzinfos können unter dem Titel stehen
- lange Notizen überfrachten die Liste nicht
- spätere Unteraufgaben passen besser in den Detailbereich
- die UI fühlt sich stärker wie eine Aufgaben-App an

Diese Entscheidung wurde im Chat ausdrücklich als sinnvoll angesehen.

## 10. Suche und Statusbereich

Im unteren Bereich kann eine Suche angezeigt werden.

Gewünschte Elemente:

- Suchfeld
- Button zum Leeren der Suche
- Backup-Button
- Statuszeile mit Datenbankpfad
- Anzahl sichtbarer Aufgaben

Der aktuelle Datenbankpfad ist für Debugging und Transparenz hilfreich, kann später optional ausgeblendet werden.

## 11. Visuelle Richtung

Die Anwendung soll zuerst funktional bleiben, aber folgende visuelle Eigenschaften anstreben:

- ruhig
- hell
- gut lesbar
- klare Abstände
- dezente Rahmen
- zurückhaltende Farben
- keine überladene Icon-Nutzung
- Windows-typische Bedienbarkeit

Icons dürfen helfen, sollten aber nicht wichtiger als Text werden.

## 12. V1-UI und Ziel-UI

### 12.1 V1 darf pragmatisch sein

Für V1 sind einfache Windows-Forms-Steuerelemente akzeptabel:

- `MenuStrip`
- `ToolStrip`
- `SplitContainer`
- `ListBox` oder `ListView`
- `DataGridView`
- `TextBox`
- `Button`
- Dialogformulare

### 12.2 Ziel-UI nach V1

Nach Stabilisierung der Grundfunktionen sollte geprüft werden:

- Ersatz der Tabelle durch eigene Aufgabenzeilen
- rechter Detailbereich
- bessere Smart-View-Navigation
- Favoriten/Stern direkt in der Zeile
- Fälligkeit unter dem Titel
- kleine Status-/Metadatenzeile pro Aufgabe

## 13. Grobe UI-Roadmap

### UI v0.1

- Menü
- Toolbar
- linke Listen
- zentrale Aufgabenanzeige
- Dialoge für Listen und Aufgaben
- Suche unten
- Backupbutton

### UI v0.2

- Wunderlist-ähnlichere Aufgabenliste
- rechter Detailbereich
- bessere Anzeige von Fälligkeit, Priorität und Stern

### UI v0.3

- Unteraufgaben im Detailbereich
- Smart Views besser sichtbar
- Erledigte Aufgaben einklappbar

### UI v0.4

- Feinschliff
- Screenshots für README
- Tastaturkürzel
- besserer leerer Zustand

## 14. Offene UI-Fragen

Folgende Fragen sind später zu klären:

- Soll der Detailbereich dauerhaft sichtbar sein oder ein-/ausblendbar?
- Soll Bearbeitung im Detailbereich direkt speichern oder über Speichern-Button erfolgen?
- Soll Doppelklick weiterhin einen Dialog öffnen?
- Wie werden erledigte Aufgaben angezeigt?
- Soll es Drag & Drop für Sortierung geben?
- Soll die linke Navigation Smart Views und Listen optisch trennen?
- Welche Farben sollen zur SASD-/TaskHost-Familie passen?

