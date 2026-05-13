# TaskHost Local – Manueller Testplan

**Dokumentstatus:** Arbeitsfassung v0.1  
**Stand:** 2026-05-13  
**Bezug:** MVP-Abnahme nach Lastenheft/Pflichtenheft v0.2

## 1. Zweck

Dieser Testplan beschreibt manuelle Prüfungen für die erste arbeitsfähige Version von **TaskHost Local**.

Er ersetzt keine späteren automatisierten Tests, hilft aber dabei, frühe Entwicklungsstände nachvollziehbar zu prüfen.

## 2. Testumgebung

Empfohlene Umgebung:

- Windows 10 oder Windows 11,
- .NET 8 SDK,
- Visual Studio oder Kommandozeile,
- lokaler Benutzer ohne besondere Administratorrechte.

## 3. Vorbedingungen

Vor einem sauberen Test sollte geprüft werden:

- Repository ist aktuell,
- Build läuft erfolgreich,
- keine echten privaten Aufgaben werden verwendet,
- bestehende Testdatenbank wurde gesichert oder gelöscht, wenn ein frischer Start geprüft werden soll.

Datenbankpfad:

```text
%AppData%\SASD\TaskHostLocal\taskhost.db
```

## 4. Build-Test

### Schritte

```powershell
dotnet restore
dotnet build
```

### Erwartetes Ergebnis

- Restore erfolgreich.
- Build erfolgreich.
- Keine Compilerfehler.

## 5. Start-Test

### Schritte

```powershell
dotnet run --project .\TaskHostLocal.WinForms\TaskHostLocal.WinForms.csproj
```

### Erwartetes Ergebnis

- Anwendung startet.
- Kein Fehlerdialog beim Start.
- Hauptfenster wird angezeigt.

## 6. Datenbankinitialisierung

### Schritte

1. Anwendung starten.
2. Prüfen, ob Datenbankdatei unter `%AppData%\SASD\TaskHostLocal\taskhost.db` angelegt wurde.
3. Prüfen, ob Standardliste „Eingang“ sichtbar ist.

### Erwartetes Ergebnis

- Datenbankdatei existiert.
- Standardliste „Eingang“ existiert.
- Keine doppelte Standardliste nach mehrfachem Start.

## 7. Listenverwaltung

### Testfall 7.1 – Liste anlegen

Schritte:

1. Neue Liste anlegen.
2. Namen `Testliste` verwenden.
3. Speichern.

Erwartet:

- Liste erscheint in der Navigation.

### Testfall 7.2 – Liste umbenennen

Schritte:

1. `Testliste` auswählen.
2. Umbenennen in `Testliste umbenannt`.
3. Speichern.

Erwartet:

- Neuer Name wird angezeigt.

### Testfall 7.3 – Leere Liste löschen

Schritte:

1. Leere Testliste auswählen.
2. Löschen auslösen.
3. Sicherheitsabfrage bestätigen.

Erwartet:

- Liste wird entfernt.

### Testfall 7.4 – Liste mit Aufgaben nicht löschen

Schritte:

1. Liste mit mindestens einer Aufgabe auswählen.
2. Löschen auslösen.

Erwartet:

- Liste wird im MVP nicht gelöscht.
- Benutzer erhält verständliche Meldung.

## 8. Aufgabenverwaltung

### Testfall 8.1 – Aufgabe anlegen

Schritte:

1. Liste „Eingang“ auswählen.
2. Neue Aufgabe anlegen.
3. Titel `Testaufgabe` eingeben.
4. Notiz, Fälligkeit und Priorität setzen.
5. Speichern.

Erwartet:

- Aufgabe erscheint in der Aufgabenliste.
- Daten werden korrekt angezeigt.

### Testfall 8.2 – Aufgabe bearbeiten

Schritte:

1. Aufgabe auswählen.
2. Bearbeiten öffnen.
3. Titel und Notiz ändern.
4. Speichern.

Erwartet:

- Geänderte Werte werden angezeigt.
- Änderungen bleiben nach Neustart erhalten.

### Testfall 8.3 – Aufgabe erledigen

Schritte:

1. Offene Aufgabe auswählen.
2. Erledigt/offen umschalten.

Erwartet:

- Aufgabe wird als erledigt markiert.
- `completed_at` wird gesetzt.

### Testfall 8.4 – Aufgabe wieder öffnen

Schritte:

1. Erledigte Aufgabe auswählen.
2. Erledigt/offen umschalten.

Erwartet:

- Aufgabe wird wieder offen.
- `completed_at` wird zurückgesetzt.

### Testfall 8.5 – Aufgabe löschen

Schritte:

1. Aufgabe auswählen.
2. Löschen auslösen.
3. Sicherheitsabfrage bestätigen.

Erwartet:

- Aufgabe wird entfernt.
- Aufgabenliste wird aktualisiert.

## 9. Suche

### Testfall 9.1 – Suche nach Titel

Schritte:

1. Aufgabe mit eindeutigem Titel anlegen.
2. Suchtext eingeben.

Erwartet:

- Aufgabe wird gefunden.

### Testfall 9.2 – Suche nach Notiz

Schritte:

1. Aufgabe mit eindeutigem Wort in der Notiz anlegen.
2. Suchtext eingeben.

Erwartet:

- Aufgabe wird gefunden.

### Testfall 9.3 – Leere Suche

Schritte:

1. Suchfeld leeren.

Erwartet:

- Normale Aufgabenliste wird wieder angezeigt.
- Kein SQL-Fehler tritt auf.

## 10. Backup

### Schritte

1. Datenbank sichern auslösen.
2. Backup-Datei prüfen.

### Erwartetes Ergebnis

- Backup-Datei wird erstellt.
- Dateiname enthält Datum/Uhrzeit.
- Speicherort wird nachvollziehbar angezeigt.
- Backup-Datei wird nicht ins Repository eingecheckt.

## 11. Neustart und Persistenz

### Schritte

1. Liste und Aufgabe anlegen.
2. Anwendung schließen.
3. Anwendung erneut starten.

### Erwartetes Ergebnis

- Liste ist weiterhin vorhanden.
- Aufgabe ist weiterhin vorhanden.
- Erledigt-Status, Notiz, Fälligkeit und Priorität bleiben erhalten.

## 12. Netzwerkfreiheit

### Ziel

Das MVP soll keine Netzwerkkommunikation durchführen.

### Manuelle Prüfung

- Keine Login-Maske vorhanden.
- Keine Serveradresse konfigurierbar.
- Keine Auto-Update-Funktion vorhanden.
- Keine Telemetrie-Hinweise vorhanden.
- Keine TaskHost-API-Anbindung vorhanden.

Eine tiefere technische Prüfung kann später mit geeigneten Tools erfolgen.

## 13. Abnahmestatus

| Bereich | Bestanden | Bemerkung |
|---|---|---|
| Build | [ ] | |
| Start ohne Fehler | [ ] | |
| Datenbankinitialisierung | [ ] | |
| Listenverwaltung | [ ] | |
| Aufgabenverwaltung | [ ] | |
| Suche | [ ] | |
| Backup | [ ] | |
| Persistenz nach Neustart | [ ] | |
| Netzwerkfreiheit | [ ] | |

## 14. Ergebnis

Ein Stand gilt als arbeitsfähiges MVP, wenn alle Muss-Prüfungen bestanden sind:

- Build erfolgreich,
- Start ohne Fehlerdialog,
- Standardliste vorhanden,
- Listen-CRUD funktioniert,
- Aufgaben-CRUD funktioniert,
- Suche funktioniert,
- Backup funktioniert,
- Daten bleiben nach Neustart erhalten.
