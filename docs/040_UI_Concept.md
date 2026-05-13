# TaskHost Local – UI-Konzept

**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13  
**Bezug:** Lastenheft/Pflichtenheft MVP v0.2

## 1. Zweck des Dokuments

Dieses Dokument beschreibt die gewünschte UI-Richtung für **TaskHost Local**.

Es basiert auf den bisherigen Überlegungen, Screenshots und Diskussionen im Projektchat. Ziel ist nicht, sofort ein perfektes Design zu erstellen, sondern die Richtung festzuhalten, damit die Anwendung nicht in eine falsche UI-Struktur läuft.

Die wichtigste UI-Leitlinie lautet:

> Die Oberfläche darf in der ersten Version schlicht sein, soll aber langfristig wie eine Aufgabenverwaltung wirken und nicht wie eine reine Datenbanktabelle.

## 2. Grundentscheidung

Die Benutzeroberfläche soll zunächst funktional und einfach bleiben, aber langfristig näher an modernen Aufgabenlisten-Anwendungen liegen.

Besonders wichtig:

- Menüleiste und Toolbar sind Muss-Anforderungen für das MVP.
- Die Aufgabenliste soll übersichtlich bleiben.
- Aufgabendetails sollen separat angezeigt werden können.
- Die Aufgabenliste soll Platz für Titel, Fälligkeit, Priorität, Favorit und kurze Zusatzinformationen bieten.
- Ein rechter Detailbereich ist ein wichtiges Zielbild.
- Eine einfache `DataGridView` ist als pragmatische Zwischenlösung erlaubt.

## 3. Grundlayout

Das bevorzugte Zielbild besteht aus drei Hauptbereichen:

```text
+--------------------------------------------------------------------------+
| Menüleiste                                                               |
+--------------------------------------------------------------------------+
| Toolbar                                                                  |
+----------------------+--------------------------------+------------------+
| Navigation           | Aufgabenliste                  | Details          |
|                      |                                |                  |
| Systemliste          | Aufgabe 1                      | Titel            |
| - Eingang            | Fällig: Heute · Prio: Hoch     | Notiz            |
|                      |                                | Fälligkeit       |
| Smart Views          | Aufgabe 2                      | Priorität        |
| - Alle Aufgaben      | Fällig: 15.05.2026             | Liste            |
| - Heute              |                                |                  |
| - Überfällig         |                                |                  |
| - Erledigt           |                                |                  |
|                      |                                |                  |
| Eigene Listen        |                                |                  |
| - Arbeit             |                                |                  |
| - Privat             |                                |                  |
+----------------------+--------------------------------+------------------+
| Suche / Status / Backup                                                  |
+--------------------------------------------------------------------------+
```

## 4. Menüleiste

Die Menüleiste ist für V1 eine **Muss-Anforderung**.

Sie bietet klassische Windows-Bedienbarkeit und macht Funktionen auffindbar, auch wenn Icons oder Toolbar-Buttons später verändert werden.

Vorgeschlagene Struktur:

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
- Erledigt/offen umschalten

Ansicht
- Aktualisieren
- später: Smart Views / Detailbereich ein- oder ausblenden

Hilfe
- Über TaskHost Local
```

Die Menüleiste darf zunächst funktional und schlicht bleiben. Wichtig ist, dass zentrale Aktionen erreichbar sind.

## 5. Toolbar

Die Toolbar ist ebenfalls eine **Muss-Anforderung**.

Sie wurde im Projektchat ausdrücklich positiv bewertet und soll erhalten bleiben.

Vorgeschlagene Toolbar-Aktionen:

- neue Aufgabe,
- Aufgabe bearbeiten,
- Aufgabe löschen,
- erledigt/offen umschalten,
- neue Liste,
- Liste umbenennen,
- Liste löschen,
- Datenbank sichern,
- aktualisieren.

Die Toolbar soll nicht überladen werden. Häufig genutzte Funktionen gehören in die Toolbar, seltene Funktionen eher ins Menü.

## 6. Linke Navigation

Die linke Navigation soll fachlich klar zwischen Systemliste, Smart Views und eigenen Listen unterscheiden.

### 6.1 Systemliste

Die Systemliste ist eine echte gespeicherte Liste.

Für das MVP gibt es mindestens:

- Eingang

Die Liste „Eingang“ ist **keine Smart View**. Sie ist eine echte Aufgabenliste in der Tabelle `task_lists`.

### 6.2 Smart Views

Smart Views sind gefilterte Ansichten über vorhandene Aufgaben. Sie werden nicht als normale Listen gespeichert.

Wichtige Smart Views:

- Alle Aufgaben,
- Heute,
- Überfällig,
- Erledigt,
- Woche,
- Favoriten.

Priorisierung:

| Smart View | Priorität | Bemerkung |
|---|---:|---|
| Alle Aufgaben | hoch | sehr hilfreich zur Gesamtübersicht |
| Heute | hoch | zentrale Tagesplanung |
| Überfällig | hoch | verhindert verlorene Aufgaben |
| Erledigt | mittel | Rückblick und Kontrolle |
| Woche | mittel | hilfreich, aber nicht zwingend im ersten CRUD-MVP |
| Favoriten | mittel/später | benötigt `is_starred` im Datenmodell |

### 6.3 Eigene Listen

Eigene Listen werden vom Benutzer angelegt.

Beispiele:

- Arbeit,
- Privat,
- SASD,
- Einkaufsliste,
- Projekte.

Für V1 kann eine einfache `ListBox`, `TreeView` oder `ListView` genügen. Langfristig sollte die linke Navigation optisch klarer werden.

## 7. Aufgabenliste

Die Aufgabenliste ist der wichtigste Arbeitsbereich.

### 7.1 Problem mit reiner Tabellenansicht

Eine klassische `DataGridView` ist schnell umzusetzen, wirkt aber technisch und nimmt viel Platz für Spalten ein.

Nachteile einer reinen Tabelle:

- viele Details konkurrieren miteinander,
- Titel haben wenig Raum,
- Notizen und Fälligkeiten wirken wie Datenbankspalten,
- die Oberfläche fühlt sich weniger wie eine Aufgaben-App an.

### 7.2 Erlaubte V1-Zwischenlösung

Für die erste arbeitsfähige Version darf eine `DataGridView` verwendet werden.

Das ist akzeptabel, solange die Architektur nicht verhindert, später auf eine bessere Aufgabenlisten-Darstellung umzusteigen.

### 7.3 Zielbild nach V1

Langfristig soll jede Aufgabe eher wie eine Listenzeile oder einfache Karte dargestellt werden.

Beispiel:

```text
[ ] Aufgabentitel                                      ☆
    Fällig: 15.05.2026 · Priorität: Mittel · Liste: SASD
    Kurze Notizvorschau oder Zusatztext
```

Mögliche Elemente:

- Checkbox für erledigt/offen,
- Titel,
- Stern/Favorit,
- Fälligkeitsdatum,
- Priorität,
- Liste/Projekt,
- kurze Notiz oder Vorschautext.

### 7.4 Auswahlverhalten

Beim Klick auf eine Aufgabe:

- Aufgabe wird in der Liste markiert.
- Details werden rechts angezeigt.
- Bearbeitung kann zunächst über Dialog erfolgen.
- Später kann Bearbeitung direkt im Detailbereich möglich sein.

## 8. Rechter Detailbereich

Der rechte Detailbereich ist ein zentrales Zielbild.

Er soll Details anzeigen, die nicht dauerhaft in der Aufgabenliste stehen müssen.

Mögliche Inhalte:

- Titel,
- Erledigt-Checkbox,
- Favorit/Stern,
- Fälligkeitsdatum,
- Priorität,
- zugehörige Liste,
- Notizen,
- später Unteraufgaben/Checkliste,
- später Anhänge,
- später Verlauf oder Kommentare, falls überhaupt gewünscht.

Der Detailbereich kann zunächst nur lesend sein oder einfache Bearbeitung ermöglichen. Später kann er zum Hauptbearbeitungsbereich werden.

## 9. Warum Details separat anzeigen?

Die Trennung von Liste und Details hat mehrere Vorteile:

- die Aufgabenliste bleibt übersichtlich,
- der Aufgabentitel bekommt mehr Platz,
- Fälligkeit und Kurzinfos können unter dem Titel stehen,
- lange Notizen überfrachten die Liste nicht,
- spätere Unteraufgaben passen besser in den Detailbereich,
- die UI fühlt sich stärker wie eine Aufgaben-App an.

Diese Entscheidung wurde im Projektchat ausdrücklich als sinnvoll angesehen.

## 10. Suche und Statusbereich

Im unteren Bereich kann eine Suche angezeigt werden.

Gewünschte Elemente:

- Suchfeld,
- Button zum Leeren der Suche,
- Backup-Button,
- Statuszeile mit Datenbankpfad,
- Anzahl sichtbarer Aufgaben.

Der aktuelle Datenbankpfad ist für Debugging und Transparenz hilfreich, kann später optional ausgeblendet werden.

## 11. Visuelle Richtung

Die Anwendung soll zuerst funktional bleiben, aber folgende visuelle Eigenschaften anstreben:

- ruhig,
- hell,
- gut lesbar,
- klare Abstände,
- dezente Rahmen,
- zurückhaltende Farben,
- keine überladene Icon-Nutzung,
- Windows-typische Bedienbarkeit.

Icons dürfen helfen, sollten aber nicht wichtiger als Text werden.

## 12. V1-UI und Ziel-UI

### 12.1 V1 darf pragmatisch sein

Für V1 sind einfache Windows-Forms-Steuerelemente akzeptabel:

- `MenuStrip`,
- `ToolStrip`,
- `SplitContainer`,
- `ListBox`, `TreeView` oder `ListView`,
- `DataGridView`,
- `TextBox`,
- `Button`,
- Dialogformulare.

### 12.2 Ziel-UI nach V1

Nach Stabilisierung der Grundfunktionen sollte geprüft werden:

- Ersatz der Tabelle durch eigene Aufgabenzeilen,
- rechter Detailbereich,
- bessere Smart-View-Navigation,
- Favoriten/Stern direkt in der Zeile,
- Fälligkeit unter dem Titel,
- kleine Status-/Metadatenzeile pro Aufgabe,
- bessere Leerzustände.

## 13. Grobe UI-Roadmap

### UI v0.1

- Menü,
- Toolbar,
- linke Listen,
- zentrale Aufgabenanzeige,
- Dialoge für Listen und Aufgaben,
- Suche unten,
- Backupbutton.

### UI v0.2

- stabilere Bedienung,
- sichere Löschdialoge,
- Menü/Toolbar vollständig nutzbar,
- bessere Statusmeldungen.

### UI v0.3

- Aufgabenliste stärker im Aufgaben-App-Stil,
- rechter Detailbereich,
- bessere Anzeige von Fälligkeit, Priorität und Notizvorschau.

### UI v0.4

- Smart Views besser sichtbar,
- Alle Aufgaben,
- Heute,
- Überfällig,
- Erledigt,
- Favoriten nach Schema-Erweiterung.

### UI v0.5+

- Unteraufgaben im Detailbereich,
- erledigte Aufgaben einklappbar,
- Tastaturkürzel,
- Feinschliff,
- README-Screenshots.

## 14. Offene UI-Fragen

Folgende Fragen sind später zu klären:

- Soll der Detailbereich dauerhaft sichtbar oder ein-/ausblendbar sein?
- Soll Bearbeitung im Detailbereich direkt speichern oder über einen Speichern-Button erfolgen?
- Soll Doppelklick weiterhin einen Dialog öffnen?
- Wie werden erledigte Aufgaben angezeigt?
- Soll es Drag & Drop für Sortierung geben?
- Soll die linke Navigation Smart Views und Listen optisch trennen?
- Welche Farben sollen zur SASD-/TaskHost-Familie passen?
- Welche Icons dürfen rechtlich unproblematisch verwendet werden?

## 15. UI-Abgrenzung für das MVP

Nicht Teil des MVP:

- perfekte moderne Oberfläche,
- selbst gezeichnete komplexe Controls,
- Drag & Drop,
- Themes,
- Animationen,
- vollständige Tastatursteuerung,
- Touch-Optimierung,
- High-End-Iconset,
- exakter Nachbau fremder Aufgaben-Apps.

Das MVP muss funktional und verständlich sein. Optischer Feinschliff folgt nach Stabilisierung.
