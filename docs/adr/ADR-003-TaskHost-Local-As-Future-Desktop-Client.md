# ADR-003: Keep TaskHost Local open as a future TaskHost desktop client

**Status:** Accepted  
**Date:** 2026-05-13  

## Context

TaskHost Local is currently needed as a local Windows application. At the same time, the broader TaskHost project may later need a desktop client.

It is not yet clear whether TaskHost Local will remain a standalone offline application or become the official Windows desktop client for TaskHost.

## Decision

TaskHost Local will be designed so that it can later become a TaskHost desktop client, but this is not required for the MVP.

## Rationale

This keeps multiple future paths open:

1. TaskHost Local remains a standalone local app.
2. TaskHost Local becomes TaskHost Desktop.
3. TaskHost Local remains an offline edition while a separate modern client is developed later.

## Consequences

Positive consequences:

- The project remains strategically aligned with TaskHost.
- Naming and concepts stay consistent.
- Later API integration is possible.

Negative consequences:

- Some decisions must consider future compatibility.
- The data model should not be designed too narrowly.

## Practical Guidance

For the MVP:

- no API integration
- no login
- no synchronization
- local SQLite only

For later versions:

- define export/import format
- consider UUIDs
- consider remote IDs
- consider sync status fields
- consider API adapter layer

