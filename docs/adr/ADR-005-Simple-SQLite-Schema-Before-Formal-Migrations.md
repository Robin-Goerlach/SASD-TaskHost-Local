# ADR-005 – Simple SQLite Schema Before Formal Migrations

**Status:** Accepted  
**Date:** 2026-05-13  
**Project:** SASD TaskHost Local

## Context

TaskHost Local starts as a small local Windows desktop application with SQLite storage.

The MVP only needs a small number of tables:

- `task_lists`,
- `tasks`.

More advanced schema elements such as subtasks, tags, attachments, reminders, favorites, schema versions and sync metadata are planned as possible later extensions.

Adding a formal migration framework at the very beginning would increase complexity before the application is even usable.

## Decision

For the early MVP, TaskHost Local uses a simple SQLite schema initialized through application code.

The initial schema is created with `CREATE TABLE IF NOT EXISTS` statements.

Formal schema migrations are deferred until schema changes become frequent or user data must be migrated safely between versions.

## Consequences

Positive consequences:

- faster MVP development,
- easier understanding of the database,
- fewer moving parts,
- no migration framework required for the first version.

Negative consequences:

- schema changes must be handled carefully,
- early development may require manual database reset,
- future releases need a migration strategy before real user data is at risk.

## Future Direction

A later version should introduce a schema version table, for example:

```sql
CREATE TABLE IF NOT EXISTS schema_version (
    version INTEGER NOT NULL,
    applied_at TEXT NOT NULL
);
```

After that, schema changes should be applied as explicit migration steps.
