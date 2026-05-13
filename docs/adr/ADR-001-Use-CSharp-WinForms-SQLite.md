# ADR-001: Use C#, Windows Forms and SQLite for the first local version

**Status:** Accepted  
**Date:** 2026-05-13  

## Context

TaskHost Local is intended to become a quickly usable local Windows task management application. The main priority for the first version is practical usability, not visual perfection or architectural sophistication.

Several technology options were considered:

- C# with Windows Forms
- C# with WPF
- C# with Avalonia
- Java with Swing
- Java with AWT
- TypeScript/React with Electron or Tauri

The project should be easy to build, easy to debug and suitable for a simple local CRUD-style application.

## Decision

The first version of TaskHost Local will use:

- C#
- .NET 8 Windows
- Windows Forms
- SQLite
- Microsoft.Data.Sqlite

## Rationale

This decision was made because:

- C# is familiar to the developer.
- Windows Forms is fast for simple desktop CRUD applications.
- SQLite is sufficient for local single-user storage.
- The project can become usable quickly.
- The code can still be structured cleanly.
- The UI can later be replaced if needed.

## Consequences

Positive consequences:

- Fast initial development.
- Simple local deployment.
- Low infrastructure requirements.
- No server required.
- No login or cloud dependency.

Negative consequences:

- Windows Forms is Windows-focused.
- The UI may look less modern than Avalonia, WPF or web-based alternatives.
- Cross-platform support is not part of V1.

## Notes

A later version may use Avalonia or another UI framework if cross-platform support becomes important.

