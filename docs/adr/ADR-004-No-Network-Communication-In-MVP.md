# ADR-004 – No Network Communication in MVP

**Status:** Accepted  
**Date:** 2026-05-13  
**Project:** SASD TaskHost Local

## Context

TaskHost Local is intended to provide a quickly usable local Windows task management application.

The existing TaskHost project may later provide API, web and synchronization functionality. However, integrating these concerns into the first local version would significantly increase complexity.

The MVP should be usable without:

- login,
- server,
- cloud,
- synchronization,
- internet connection.

Tasks may contain private or business information. Therefore, avoiding telemetry and implicit network communication is also important from a privacy and trust perspective.

## Decision

The MVP of TaskHost Local must not perform network communication.

This includes:

- no TaskHost API connection,
- no HTTP client for application logic,
- no telemetry,
- no automatic update checks,
- no cloud synchronization,
- no background sync service,
- no login or token handling.

## Consequences

Positive consequences:

- simpler MVP,
- faster development,
- fewer security concerns,
- clearer offline promise,
- easier debugging,
- no dependency on TaskHost server availability.

Negative consequences:

- no multi-device synchronization,
- no collaboration,
- no server backup,
- no central account model,
- later sync will require additional design work.

## Notes

This decision does not prevent a future TaskHost API adapter. It only states that such integration is not part of the MVP.
