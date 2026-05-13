# TaskHost Local – Data Model

**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13  
**Bezug:** Lastenheft/Pflichtenheft MVP v0.2

## 1. Zweck des Dokuments

Dieses Dokument beschreibt das lokale Datenmodell von **TaskHost Local**.

Es dient als Grundlage für:

- SQLite-Schema,
- Repository-Implementierung,
- UI-Anzeigen,
- spätere Erweiterungen,
- mögliche TaskHost-Kompatibilität.

## 2. Modellierungsprinzipien

Das Datenmodell soll:

- einfach sein,
- lokal mit SQLite funktionieren,
- die MVP-Funktionen abdecken,
- später erweiterbar bleiben,
- begrifflich zur TaskHost-Produktfamilie passen,
- keine unnötige Cloud-/Multiuser-Komplexität enthalten,
- keine spätere Migration unnötig erschweren.

## 3. MVP-Entitäten

Für das MVP genügen zwei Kernentitäten:

1. `task_lists`
2. `tasks`

Später können weitere Entitäten ergänzt werden.

Nicht Teil des MVP:

- `subtasks`,
- `tags`,
- `attachments`,
- `reminders`,
- `comments`,
- `users`,
- `sync_state`.

## 4. Tabelle `task_lists`

### 4.1 Zweck

`task_lists` speichert echte Aufgabenlisten.

Beispiele:

- Eingang,
- Arbeit,
- Privat,
- SASD,
- Einkaufsliste.

Wichtig:

> „Eingang“ ist eine echte Liste und keine Smart View.

### 4.2 Felder

| Feld | Typ | Pflicht | Beschreibung |
|---|---|---:|---|
| `id` | INTEGER | ja | Primärschlüssel |
| `name` | TEXT | ja | Anzeigename der Liste |
| `sort_order` | INTEGER | ja | Sortierreihenfolge |
| `created_at` | TEXT | ja | Erstellzeitpunkt |
| `updated_at` | TEXT | ja | Änderungszeitpunkt |

### 4.3 SQL-Vorschlag

```sql
CREATE TABLE IF NOT EXISTS task_lists (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    sort_order INTEGER NOT NULL DEFAULT 0,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL
);
```

### 4.4 Regeln

- `name` darf nicht leer sein.
- Eine Standardliste „Eingang“ wird automatisch angelegt.
- Für V1 sollen Listen nur gelöscht werden dürfen, wenn sie keine Aufgaben enthalten.
- Vor dem Löschen einer Liste ist eine Sicherheitsabfrage erforderlich.
- `sort_order` ist für spätere Sortierung vorgesehen.

### 4.5 Spätere Erweiterung

Eine spätere Erweiterung könnte Systemlisten kenntlich machen:

```sql
ALTER TABLE task_lists ADD COLUMN is_system INTEGER NOT NULL DEFAULT 0;
```

Das ist für das MVP nicht erforderlich.

## 5. Tabelle `tasks`

### 5.1 Zweck

`tasks` speichert die eigentlichen Aufgaben.

Jede Aufgabe gehört im MVP genau zu einer echten Liste.

### 5.2 Felder

| Feld | Typ | Pflicht | Beschreibung |
|---|---|---:|---|
| `id` | INTEGER | ja | Primärschlüssel |
| `list_id` | INTEGER | ja | Fremdschlüssel auf `task_lists` |
| `title` | TEXT | ja | Aufgabentitel |
| `description` | TEXT | nein | Notiz/Beschreibung |
| `due_date` | TEXT | nein | Fälligkeitsdatum |
| `priority` | INTEGER | ja | Priorität 0–3 |
| `is_completed` | INTEGER | ja | 0 = offen, 1 = erledigt |
| `created_at` | TEXT | ja | Erstellzeitpunkt |
| `updated_at` | TEXT | ja | Änderungszeitpunkt |
| `completed_at` | TEXT | nein | Zeitpunkt der Erledigung |

### 5.3 SQL-Vorschlag

```sql
CREATE TABLE IF NOT EXISTS tasks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    list_id INTEGER NOT NULL,
    title TEXT NOT NULL,
    description TEXT NULL,
    due_date TEXT NULL,
    priority INTEGER NOT NULL DEFAULT 0,
    is_completed INTEGER NOT NULL DEFAULT 0,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL,
    completed_at TEXT NULL,
    FOREIGN KEY (list_id) REFERENCES task_lists(id)
);
```

### 5.4 Regeln

- `title` darf nicht leer sein.
- `list_id` muss auf eine vorhandene Liste verweisen.
- `priority` muss zwischen 0 und 3 liegen.
- `is_completed` wird als `0` oder `1` gespeichert.
- Beim Erledigen wird `completed_at` gesetzt.
- Beim Wiederöffnen wird `completed_at` auf `NULL` gesetzt.
- Aufgaben werden im MVP physisch gelöscht; ein Papierkorb ist nicht Teil des MVP.
- Vor dem Löschen einer Aufgabe ist eine Sicherheitsabfrage erforderlich.

## 6. Priorität

Für das MVP wird Priorität als Integer gespeichert.

| Wert | Bedeutung |
|---:|---|
| 0 | normal / keine besondere Priorität |
| 1 | niedrig |
| 2 | mittel |
| 3 | hoch |

Die UI kann daraus Texte wie „Normal“, „Niedrig“, „Mittel“ und „Hoch“ ableiten.

Ungültige Werte sollen beim Speichern verhindert oder auf `0` zurückgesetzt werden.

## 7. Datumsspeicherung

Datumswerte werden als Text gespeichert.

Für das MVP ist `due_date` als reines Fälligkeitsdatum zu verstehen.

Empfehlung:

- intern ISO-nah speichern,
- Anzeige lokalisiert formatieren,
- für Fälligkeit zunächst Datum ohne Uhrzeit verwenden.

Beispiel:

```text
2026-05-13
```

Zeitpunkte wie `created_at`, `updated_at` und `completed_at` können Datum und Uhrzeit enthalten:

```text
2026-05-13T08:43:31
```

Wichtig ist, dass die Speicherung konsistent erfolgt.

## 8. Beziehungen

Eine Liste kann viele Aufgaben enthalten.

```text
task_lists 1 ─── n tasks
```

Eine Aufgabe gehört im MVP genau zu einer Liste.

## 9. Standardliste „Eingang“

Beim ersten Start soll automatisch eine Standardliste angelegt werden:

```text
Eingang
```

Regeln:

- Die Liste wird nur angelegt, wenn noch keine passende Standardliste existiert.
- Sie ist eine echte Liste in `task_lists`.
- Neue Aufgaben ohne besondere Zuordnung können standardmäßig in „Eingang“ landen.

## 10. Smart Views

Smart Views sind gefilterte Ansichten über `tasks`.

Sie werden **nicht** als Datensätze in `task_lists` gespeichert.

Wichtige Smart Views:

| Smart View | Bedeutung | Beispielbedingung |
|---|---|---|
| Alle Aufgaben | alle Aufgaben, optional ohne erledigte oder mit Gruppierung | keine Listenfilterung |
| Heute | Aufgaben mit heutiger Fälligkeit | `due_date = current date` |
| Überfällig | offene Aufgaben mit Fälligkeit vor heute | `due_date < current date AND is_completed = 0` |
| Erledigt | abgeschlossene Aufgaben | `is_completed = 1` |
| Woche | Aufgaben der aktuellen Woche | `due_date BETWEEN week_start AND week_end` |
| Favoriten | markierte Aufgaben | benötigt später `is_starred = 1` |

Für Favoriten wird eine Schema-Erweiterung benötigt.

## 11. Suche

Die Suche soll mindestens über folgende Felder laufen:

- `title`,
- `description`.

Für das MVP genügt Suche innerhalb der aktuell ausgewählten echten Liste.

Die Implementierung soll aber so geschrieben werden, dass globale Suche und Smart-View-Suche später nicht erschwert werden.

## 12. Geplante Erweiterungen

### 12.1 Favoriten / Stern

Spätere Erweiterung:

```sql
ALTER TABLE tasks ADD COLUMN is_starred INTEGER NOT NULL DEFAULT 0;
```

Erst danach kann eine echte Favoriten-Smart-View umgesetzt werden.

### 12.2 Unteraufgaben

Mögliche Tabelle:

```sql
CREATE TABLE IF NOT EXISTS subtasks (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    task_id INTEGER NOT NULL,
    title TEXT NOT NULL,
    is_completed INTEGER NOT NULL DEFAULT 0,
    sort_order INTEGER NOT NULL DEFAULT 0,
    created_at TEXT NOT NULL,
    updated_at TEXT NOT NULL,
    FOREIGN KEY (task_id) REFERENCES tasks(id)
);
```

### 12.3 Tags

Mögliche Tabellen:

```sql
CREATE TABLE IF NOT EXISTS tags (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS task_tags (
    task_id INTEGER NOT NULL,
    tag_id INTEGER NOT NULL,
    PRIMARY KEY (task_id, tag_id),
    FOREIGN KEY (task_id) REFERENCES tasks(id),
    FOREIGN KEY (tag_id) REFERENCES tags(id)
);
```

### 12.4 Anhänge

Mögliche Tabelle:

```sql
CREATE TABLE IF NOT EXISTS attachments (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    task_id INTEGER NOT NULL,
    file_name TEXT NOT NULL,
    file_path TEXT NOT NULL,
    created_at TEXT NOT NULL,
    FOREIGN KEY (task_id) REFERENCES tasks(id)
);
```

### 12.5 Erinnerungen

Fälligkeitsdatum ist Teil des MVP.

Aktive Erinnerungen sind nicht Teil des MVP.

Mögliche spätere Erweiterung:

```sql
ALTER TABLE tasks ADD COLUMN reminder_at TEXT NULL;
```

Oder eine eigene Tabelle, wenn mehrere Erinnerungen pro Aufgabe unterstützt werden sollen.

## 13. Schema-Versionierung

Für das MVP kann zunächst ohne formale Migrationen gearbeitet werden.

Später sollte eine Tabelle zur Schema-Version eingeführt werden:

```sql
CREATE TABLE IF NOT EXISTS schema_version (
    version INTEGER NOT NULL,
    applied_at TEXT NOT NULL
);
```

Damit können spätere Änderungen nachvollziehbar durchgeführt werden.

## 14. TaskHost-Kompatibilität

Das lokale Datenmodell soll nicht zwangsläufig identisch mit TaskHost sein, aber kompatible Konzepte verwenden.

Wichtige Kompatibilitätspunkte:

- eindeutige Aufgaben-ID,
- Listen-ID,
- Titel,
- Beschreibung/Notiz,
- Fälligkeit,
- Erledigt-Status,
- Priorität,
- Favorit/Stern später,
- Zeitstempel.

Für spätere Synchronisierung könnten zusätzliche Felder notwendig werden:

- `uuid`,
- `remote_id`,
- `sync_status`,
- `last_synced_at`,
- `deleted_at`,
- `version`.

Diese Felder gehören nicht ins MVP, sollten aber bei späteren Migrationen berücksichtigt werden.

## 15. Datenschutzrelevante Daten

Aufgaben können private oder geschäftliche Informationen enthalten.

Daher gilt:

- Datenbankdateien nicht ins Git-Repository aufnehmen,
- Backups nicht ins Git-Repository aufnehmen,
- Beispiel- oder Testdaten nur anonymisiert verwenden,
- Screenshots für README nur mit fiktiven Daten erstellen.

## 16. Bewusste Entscheidungen v0.2

| Frage | Entscheidung für MVP |
|---|---|
| Ist „Eingang“ eine Smart View? | Nein, echte Standardliste |
| Sind Smart Views Tabellen? | Nein, gefilterte Abfragen |
| Wird `priority` als Text gespeichert? | Nein, Integer 0–3 |
| Enthält `due_date` Uhrzeit? | Für MVP als Datum ohne Uhrzeit geplant |
| Gibt es Papierkorb/Soft Delete? | Nein, später möglich |
| Gibt es Favoriten im MVP-Schema? | Nein, spätere Erweiterung |
| Gibt es aktive Erinnerungen? | Nein, nur Fälligkeit |
| Gibt es Sync-Felder? | Nein, später möglich |

## 17. Offene Datenmodell-Fragen für spätere Versionen

- Soll `due_date` später zusätzlich Uhrzeiten unterstützen?
- Soll es ein `deleted_at` für Papierkorb geben?
- Sollen Listen sortierbar sein?
- Sollen Aufgaben innerhalb einer Liste sortierbar sein?
- Soll Favorit früh oder erst mit Smart Views eingeführt werden?
- Wie wichtig ist UUID-Kompatibilität für spätere TaskHost-Synchronisierung?
- Soll eine spätere Datenbankverschlüsselung unterstützt werden?
