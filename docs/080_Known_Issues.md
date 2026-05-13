# TaskHost Local – Known Issues

**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13

## 1. Zweck des Dokuments

Dieses Dokument sammelt bekannte Fehler, technische Auffälligkeiten, Risiken und offene Projektpunkte, damit sie nicht vergessen werden.

Known Issues sind keine Schande. Sie helfen, frühe Projektstände ehrlich zu dokumentieren und gezielt weiterzuentwickeln.

## 2. Statuswerte

| Status | Bedeutung |
|---|---|
| offen | Problem besteht noch |
| in Arbeit | Problem wird gerade bearbeitet |
| behoben | Problem wurde behoben, bleibt historisch dokumentiert |
| akzeptiert | Risiko ist bekannt und wird bewusst akzeptiert |
| verschoben | Problem wird später bearbeitet |

## 3. KI-001 – SQLite-Syntaxfehler beim Laden der Aufgaben

**Status:** offen  
**Priorität:** hoch  
**Betroffen:** Initialer Stand nach erstem Commit  
**Komponente:** vermutlich `TaskRepository`

### 3.1 Symptom

Die Anwendung baut erfolgreich und startet.

Beim Laden der Aufgaben erscheint jedoch ein Fehlerdialog:

```text
Die Aufgaben konnten nicht geladen werden.
Details: SQLite Error 1: near "=": syntax error.
```

### 3.2 Auswirkung

Die Aufgaben können nicht geladen werden. Dadurch ist die Anwendung aktuell noch nicht arbeitsfähig.

### 3.3 Vermutete Ursache

Die Ursache liegt wahrscheinlich in einer fehlerhaft zusammengesetzten SQL-Abfrage.

Mögliche Ursachen:

- leere WHERE-Bedingung,
- ungültige Kombination von optionalen Filtern,
- fehlerhafter Parametername,
- Stringzusammensetzung erzeugt `WHERE = ...` oder `AND = ...`,
- Alias oder Spaltenname fehlt vor einem Gleichheitszeichen.

### 3.4 Nächster Analyse-Schritt

Zu prüfen sind insbesondere:

- SQL in `TaskRepository`,
- Abfrage zum Laden von Aufgaben pro Liste,
- Abfrage bei leerem Suchtext,
- Abfrage bei nicht ausgewählter Liste,
- zusammengesetzte Filterbedingungen.

### 3.5 Akzeptanzkriterium für Behebung

Der Fehler gilt als behoben, wenn:

- Anwendung ohne Fehlerdialog startet,
- leere Aufgabenliste angezeigt werden kann,
- Aufgaben einer Liste geladen werden können,
- Suche mit leerem und gefülltem Suchfeld funktioniert.

## 4. KI-002 – Repository ist öffentlich

**Status:** bewusst akzeptiert  
**Priorität:** mittel

### 4.1 Beschreibung

Das GitHub-Repository ist öffentlich. Deshalb dürfen keine privaten Daten, echten Aufgaben, Datenbanken, Backups oder Zugangsdaten eingecheckt werden.

### 4.2 Maßnahmen

- `.gitignore` prüfen,
- keine `.db`-Dateien einchecken,
- keine Backup-Dateien einchecken,
- Screenshots nur mit Beispieldaten erstellen,
- keine privaten Notizen oder Projektdaten einchecken.

## 5. KI-003 – Erste UI ist noch nicht Ziel-UI

**Status:** akzeptiert  
**Priorität:** niedrig bis mittel

### 5.1 Beschreibung

Die erste UI ist funktional angelegt und kann technisch noch tabellarisch wirken.

Das langfristige Ziel ist eine Aufgabenlisten-orientierte Oberfläche mit:

- linker Navigation,
- mittlerer Aufgabenliste,
- rechtem Detailbereich,
- Menüleiste,
- Toolbar.

### 5.2 Maßnahmen

- V1 funktional stabilisieren,
- danach UI-Konzept aus `040_UI_Concept.md` schrittweise umsetzen.

## 6. KI-004 – Noch keine automatisierten Tests

**Status:** offen  
**Priorität:** mittel

### 6.1 Beschreibung

Im MVP existieren zunächst keine automatisierten Tests.

### 6.2 Risiko

Änderungen an SQL, Repositories oder Services können unbeabsichtigte Fehler erzeugen.

### 6.3 Spätere Maßnahme

- Services und Repositories in eigene Projekte auslagern,
- Unit-Tests für Service-Logik ergänzen,
- Integrationstest mit temporärer SQLite-Datei ergänzen.

## 7. KI-005 – Dokumentation musste mit Lastenheft/Pflichtenheft v0.2 synchronisiert werden

**Status:** behoben / zu prüfen nach Import  
**Priorität:** mittel

### 7.1 Beschreibung

Nach der Überarbeitung von Lastenheft und Pflichtenheft war die übrige Dokumentation teilweise noch auf dem Stand der ersten Arbeitsfassung.

### 7.2 Maßnahme

Die folgenden Dokumente wurden mit diesem Dokumentationspatch auf v0.2 gebracht:

- `README.md`,
- `000_Project_Overview.md`,
- `010_Strategic_Positioning.md`,
- `040_UI_Concept.md`,
- `050_Technical_Design.md`,
- `060_Data_Model.md`,
- `070_Roadmap.md`,
- `080_Known_Issues.md`,
- `090_Documentation_Checklist.md`,
- `100_Manual_Test_Plan.md`,
- neue ADRs.

### 7.3 Akzeptanzkriterium

Der Punkt gilt als erledigt, wenn der Patch eingespielt und committed wurde.

## 8. KI-006 – README-Screenshot fehlt

**Status:** offen  
**Priorität:** niedrig bis mittel

### 8.1 Beschreibung

Das README enthält noch keinen echten Screenshot der Anwendung.

### 8.2 Maßnahme

Nach Behebung des Startfehlers soll ein Screenshot mit fiktiven Daten ergänzt werden.

Wichtig:

- keine echten Aufgaben,
- keine privaten Informationen,
- keine Kundendaten,
- keine vertraulichen Projektnamen.

## 9. KI-007 – Lizenzentscheidung offen

**Status:** offen  
**Priorität:** mittel

### 9.1 Beschreibung

Das Repository ist öffentlich, aber die Lizenzentscheidung ist noch offen.

Ohne LICENSE-Datei ist der Code sichtbar, aber nicht automatisch frei weiterverwendbar.

### 9.2 Mögliche Entscheidungen

- keine Lizenz vorerst,
- MIT License,
- andere Open-Source-Lizenz,
- späteres Umziehen in private/organisatorische Struktur.

### 9.3 Empfehlung

Lizenzentscheidung erst treffen, wenn die App grundsätzlich lauffähig ist und klar ist, ob sie als Portfolio-/Open-Source-Projekt dienen soll.

## 10. KI-008 – Smart Views noch nicht implementiert

**Status:** offen  
**Priorität:** mittel

### 10.1 Beschreibung

Smart Views wie „Alle Aufgaben“, „Heute“, „Überfällig“ und „Erledigt“ sind dokumentiert, aber noch nicht implementiert.

### 10.2 Maßnahme

Smart Views sollen nach dem CRUD-MVP ergänzt werden.

## 11. KI-009 – Favoriten/Stern benötigt Schema-Erweiterung

**Status:** offen  
**Priorität:** niedrig bis mittel

### 11.1 Beschreibung

Favoriten/Stern sind als UI-Zielbild vorgesehen, benötigen aber im Datenmodell ein Feld wie `is_starred`.

### 11.2 Maßnahme

Nicht vor dem stabilen CRUD-MVP umsetzen. Vor Einführung ADR oder Data-Model-Anpassung prüfen.

## 12. KI-010 – Backup ist noch kein fachlicher Export

**Status:** akzeptiert  
**Priorität:** niedrig

### 12.1 Beschreibung

Die MVP-Backupfunktion kopiert die SQLite-Datei. Das ist kein JSON-Export und kein TaskHost-kompatibles Austauschformat.

### 12.2 Maßnahme

JSON-Export/Import später separat spezifizieren.
