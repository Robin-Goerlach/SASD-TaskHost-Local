# TaskHost Local – Known Issues

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck des Dokuments

Dieses Dokument sammelt bekannte Fehler, technische Auffälligkeiten und Risiken, damit sie nicht vergessen werden.

## 2. KI-001 – SQLite-Syntaxfehler beim Laden der Aufgaben

**Status:** offen  
**Priorität:** hoch  
**Betroffen:** Initialer Stand nach erstem Commit  
**Komponente:** vermutlich `TaskRepository`  

### 2.1 Symptom

Die Anwendung baut erfolgreich und startet. Beim Laden der Aufgaben erscheint jedoch ein Fehlerdialog:

```text
Die Aufgaben konnten nicht geladen werden.

Details:
SQLite Error 1: near "=": syntax error.
```

### 2.2 Auswirkung

Die Aufgaben können nicht geladen werden. Dadurch ist die Anwendung aktuell noch nicht arbeitsfähig.

### 2.3 Vermutete Ursache

Die Ursache liegt wahrscheinlich in einer fehlerhaft zusammengesetzten SQL-Abfrage.

Mögliche Ursachen:

- leere WHERE-Bedingung
- ungültige Kombination von optionalen Filtern
- fehlerhafter Parametername
- Stringzusammensetzung erzeugt `WHERE = ...` oder `AND = ...`
- Alias oder Spaltenname fehlt vor einem Gleichheitszeichen

### 2.4 Nächster Analyse-Schritt

Zu prüfen sind insbesondere:

- SQL in `TaskRepository`
- Abfrage zum Laden von Aufgaben pro Liste
- Abfrage bei leerem Suchtext
- Abfrage bei nicht ausgewählter Liste
- zusammengesetzte Filterbedingungen

### 2.5 Akzeptanzkriterium für Behebung

Der Fehler gilt als behoben, wenn:

- Anwendung ohne Fehlerdialog startet
- leere Aufgabenliste angezeigt werden kann
- Aufgaben einer Liste geladen werden können
- Suche mit leerem und gefülltem Suchfeld funktioniert

## 3. KI-002 – Repository ist öffentlich

**Status:** bewusst akzeptiert  
**Priorität:** mittel  

### 3.1 Beschreibung

Das GitHub-Repository ist öffentlich. Deshalb dürfen keine privaten Daten, echten Aufgaben, Datenbanken, Backups oder Zugangsdaten eingecheckt werden.

### 3.2 Maßnahmen

- `.gitignore` prüfen
- keine `.db`-Dateien einchecken
- keine Backup-Dateien einchecken
- Screenshots nur mit Beispieldaten erstellen
- keine privaten Notizen oder Projektdaten einchecken

## 4. KI-003 – Erste UI ist noch nicht Ziel-UI

**Status:** akzeptiert  
**Priorität:** niedrig bis mittel  

### 4.1 Beschreibung

Die erste UI ist funktional angelegt und kann technisch noch tabellarisch wirken. Das langfristige Ziel ist eine Aufgabenlisten-orientierte Oberfläche mit optionalem Detailbereich.

### 4.2 Maßnahmen

- V1 funktional stabilisieren
- danach UI-Konzept aus `040_UI_Concept.md` schrittweise umsetzen

## 5. KI-004 – Noch keine Tests

**Status:** offen  
**Priorität:** mittel  

### 5.1 Beschreibung

Im MVP existieren zunächst keine automatisierten Tests.

### 5.2 Risiko

Änderungen an SQL, Repositories oder Services können unbeabsichtigte Fehler erzeugen.

### 5.3 Spätere Maßnahme

- Services und Repositories in eigene Projekte auslagern
- Unit-Tests für Service-Logik ergänzen
- Integrationstest mit temporärer SQLite-Datei ergänzen

