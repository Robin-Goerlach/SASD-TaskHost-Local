# ADR-002: Develop TaskHost Local standalone before TaskHost integration

**Status:** Accepted  
**Date:** 2026-05-13  

## Context

The broader TaskHost project already exists and is intended to become the long-term main product. However, immediate integration would require handling API communication, login, tokens, offline storage, synchronization and conflict resolution.

This would slow down the goal of quickly obtaining a usable local task management application.

## Decision

TaskHost Local will be developed as a standalone local Windows application first.

The first version will not integrate with the TaskHost API.

## Rationale

Standalone development allows:

- faster delivery of a usable application
- lower complexity
- no dependency on server availability
- no authentication or token handling
- no sync conflicts
- easier debugging

## Consequences

Positive consequences:

- The application can become useful sooner.
- Development stays focused.
- The local data model can be stabilized first.

Negative consequences:

- Data will initially exist only locally.
- Later synchronization will require additional design work.
- Some future TaskHost concepts may require migration.

## Compatibility Rule

Even though TaskHost Local is standalone first, it should remain conceptually compatible with the TaskHost family.

This means:

- use compatible terminology
- avoid unnecessary divergence in data concepts
- prepare future export/import
- do not build features that make later integration impossible

