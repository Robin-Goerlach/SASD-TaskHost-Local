# TaskHost Local – Data Model

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck des Dokuments

Dieses Dokument beschreibt das lokale Datenmodell von **TaskHost Local**. Es dient als Grundlage für SQLite-Schema, Repository-Implementierung und spätere Erweiterungen.

## 2. Modellierungsprinzipien

Das Datenmodell soll:

- einfach sein
- lokal mit SQLite funktionieren
- die MVP-Funktionen abdecken
- später erweiterbar bleiben
- begrifflich zur TaskHost-Produktfamilie passen
- keine unnötige Cloud-/Multiuser-Komplexität enthalten

## 3. MVP-Entitäten

Für das MVP genügen zwei Kernentitäten:

1. `task_lists`
2. `tasks`

Später können weitere Entitäten ergänzt werden.

## 4. Tabelle `task_lists`

### 4.1 Zweck

`task_lists` speichert benutzerdefinierte Aufgabenlisten.

Beispiele:

- Eingang
- Arbeit
- Privat
- SASD
- Einkaufsliste

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

### 4.4 Hinweise

- `name` sollte nicht leer sein.
- Eine Standardliste „Eingang“ soll automatisch angelegt werden.
- Eine spätere Erweiterung könnte ein Feld `is_system` für Systemlisten nutzen.

## 5. Tabelle `tasks`

### 5.1 Zweck

`tasks` speichert die eigentlichen Aufgaben.

### 5.2 Felder

| Feld | Typ | Pflicht | Beschreibung |
|---|---|---:|---|
| `id` | INTEGER | ja | Primärschlüssel |
| `list_id` | INTEGER | ja | Fremdschlüssel auf `task_lists` |
| `title` | TEXT | ja | Aufgabentitel |
| `description` | TEXT | nein | Notiz/Beschreibung |
| `due_date` | TEXT | nein | Fälligkeitsdatum |
| `priority` | INTEGER | ja | Priorität |
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

### 5.4 Priorität

Für das MVP wird Priorität als Integer gespeichert.

Vorschlag:

| Wert | Bedeutung |
|---:|---|
| 0 | keine / normal |
| 1 | niedrig |
| 2 | mittel |
| 3 | hoch |

Die UI kann daraus Texte wie „Niedrig“, „Mittel“, „Hoch“ ableiten.

### 5.5 Datumsspeicherung

Datumswerte werden als Text gespeichert.

Empfehlung:

- intern möglichst ISO-nah speichern
- Anzeige lokalisiert formatieren
- für reine Fälligkeit genügt Datum ohne Uhrzeit

Beispiele:

```text
2026-05-13
2026-05-13T08:43:31
```

Für das MVP reicht ein pragmatischer Ansatz. Wichtig ist, dass die Speicherung konsistent erfolgt.

## 6. Beziehungen

Eine Liste kann viele Aufgaben enthalten.

```text
task_lists 1 ─── n tasks
```

Eine Aufgabe gehört im MVP genau zu einer Liste.

## 7. Smart Views

Smart Views wie „Heute“, „Woche“, „Favoriten“ oder „Erledigt“ müssen im MVP nicht zwingend als eigene Tabellen gespeichert werden.

Sie können als gefilterte Ansichten über `tasks` umgesetzt werden.

Beispiele:

- Heute: `due_date = current date`
- Woche: `due_date between week start and week end`
- Erledigt: `is_completed = 1`
- Offen: `is_completed = 0`

Für Favoriten wird später ein Feld benötigt.

## 8. Geplante Erweiterungen

### 8.1 Favoriten / Stern

Spätere Erweiterung:

```sql
ALTER TABLE tasks ADD COLUMN is_starred INTEGER NOT NULL DEFAULT 0;
```

### 8.2 Unteraufgaben

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

### 8.3 Tags

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

### 8.4 Anhänge

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

### 8.5 Erinnerungen

Mögliche Erweiterung:

```sql
ALTER TABLE tasks ADD COLUMN reminder_at TEXT NULL;
```

Oder eigene Tabelle, wenn mehrere Erinnerungen pro Aufgabe unterstützt werden sollen.

## 9. Schema-Versionierung

Für das MVP kann zunächst ohne formale Migrationen gearbeitet werden.

Später sollte eine Tabelle zur Schema-Version eingeführt werden:

```sql
CREATE TABLE IF NOT EXISTS schema_version (
    version INTEGER NOT NULL,
    applied_at TEXT NOT NULL
);
```

Damit können spätere Änderungen nachvollziehbar durchgeführt werden.

## 10. TaskHost-Kompatibilität

Das lokale Datenmodell soll nicht zwangsläufig identisch mit TaskHost sein, aber kompatible Konzepte verwenden.

Wichtige Kompatibilitätspunkte:

- eindeutige Aufgaben-ID
- Listen-ID
- Titel
- Beschreibung/Notiz
- Fälligkeit
- Erledigt-Status
- Priorität
- Favorit/Stern später
- Zeitstempel

Für spätere Synchronisierung könnten zusätzliche Felder notwendig werden:

- `uuid`
- `remote_id`
- `sync_status`
- `last_synced_at`
- `deleted_at`
- `version`

Diese Felder gehören nicht ins MVP, sollten aber bei späteren Migrationen berücksichtigt werden.

## 11. Datenschutzrelevante Daten

Aufgaben können private oder geschäftliche Informationen enthalten.

Daher gilt:

- Datenbankdateien nicht ins Git-Repository aufnehmen
- Backups nicht ins Git-Repository aufnehmen
- Beispiel- oder Testdaten nur anonymisiert verwenden
- Screenshots für README nur mit fiktiven Daten erstellen

## 12. Offene Datenmodell-Fragen

- Soll `due_date` nur Datum oder Datum+Uhrzeit enthalten?
- Soll `priority` mit Integer oder Text gespeichert werden?
- Soll es ein `deleted_at` für Papierkorb geben?
- Sollen Listen sortierbar sein?
- Sollen Aufgaben innerhalb einer Liste sortierbar sein?
- Soll Favorit bereits früh ins Schema?
- Wie wichtig ist UUID-Kompatibilität für spätere TaskHost-Synchronisierung?

