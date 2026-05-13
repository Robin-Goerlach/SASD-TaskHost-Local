# TaskHost Local – MVP-Lastenheft

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  
**Perspektive:** fachliche Anforderungen / Was soll erreicht werden?  

## 1. Zweck des Dokuments

Dieses Lastenheft beschreibt die fachlichen Anforderungen an die erste arbeitsfähige Version von **TaskHost Local**. Es beschreibt bewusst das **Was** und **Warum**, nicht jedes technische Detail der Umsetzung.

Das Ziel ist, eine klare und überprüfbare Grundlage zu schaffen, bevor weitere Entwicklungsschritte erfolgen.

## 2. Projektziel

TaskHost Local soll eine einfache, lokale, unter Windows lauffähige Aufgabenverwaltung bereitstellen.

Das MVP soll dem Nutzer ermöglichen, alltägliche Aufgaben zuverlässig zu erfassen, zu organisieren, wiederzufinden und als erledigt zu markieren, ohne dafür eine Cloud, einen Server, ein Benutzerkonto oder eine Synchronisation zu benötigen.

Leitziel:

> **Schnell eine arbeitsfähige lokale Aufgabenverwaltung bereitstellen, die funktional nützlich ist und später strukturiert weiterentwickelt werden kann.**

## 3. Zielgruppe

Die erste Zielgruppe ist der Entwickler/Nutzer selbst sowie das SASD-Umfeld.

Typische Nutzungssituationen:

- private Aufgaben sammeln
- SASD-Aufgaben verwalten
- Projektaufgaben festhalten
- kurzfristige To-dos erfassen
- einfache Fälligkeiten überwachen
- Aufgaben mit Notizen versehen
- erledigte Aufgaben nachvollziehen

## 4. Grundannahmen

Für das MVP gelten folgende Grundannahmen:

- Die Anwendung wird lokal auf einem Windows-PC genutzt.
- Die Daten werden lokal gespeichert.
- Es gibt nur einen lokalen Nutzer.
- Es gibt keine Anmeldung.
- Es gibt keine gemeinsame Nutzung durch mehrere Benutzer.
- Es gibt keine Cloud-Synchronisierung.
- Das Projekt soll schnell nutzbar werden.
- Funktionalität ist wichtiger als visuelle Perfektion.
- Der Code soll dennoch so strukturiert sein, dass Weiterentwicklung und Debugging möglich bleiben.

## 5. Abgrenzung zu TaskHost

TaskHost Local ist nicht das langfristige vollständige TaskHost-System.

TaskHost Local ist:

- lokale Windows-App
- Offline-Werkzeug
- schnelle Arbeitsversion
- möglicher Vorläufer eines späteren Desktop-Clients

TaskHost ist langfristig:

- Web-/API-Plattform
- serverbasierbares System
- potentiell multi-user- und sync-fähig

Für das MVP von TaskHost Local ist die Integration in TaskHost ausdrücklich nicht erforderlich.

## 6. Fachlicher Mindestumfang des MVP

### 6.1 Listenverwaltung

Die Anwendung soll Aufgaben in Listen organisieren können.

Mindestanforderungen:

- Listen anzeigen
- neue Liste anlegen
- bestehende Liste umbenennen
- Liste löschen
- Standardliste automatisch bereitstellen

Die Standardliste sollte z. B. „Eingang“ heißen.

Akzeptanzkriterien:

- Nach dem ersten Start existiert mindestens eine Liste.
- Eine neu angelegte Liste bleibt nach Neustart erhalten.
- Eine Liste kann umbenannt werden.
- Eine Liste kann gelöscht werden, sofern dadurch keine Daten versehentlich verloren gehen oder die Löschung bewusst bestätigt wird.

### 6.2 Aufgabenverwaltung

Die Anwendung soll Aufgaben verwalten können.

Mindestanforderungen:

- Aufgabe anlegen
- Aufgabe anzeigen
- Aufgabe bearbeiten
- Aufgabe löschen
- Aufgabe als erledigt markieren
- Aufgabe wieder als offen markieren
- Aufgabe einer Liste zuordnen

Akzeptanzkriterien:

- Eine neu angelegte Aufgabe erscheint in der ausgewählten Liste.
- Eine Aufgabe bleibt nach Neustart erhalten.
- Eine Aufgabe kann geändert werden.
- Eine Aufgabe kann gelöscht werden.
- Der Erledigt-Status kann geändert werden.

### 6.3 Aufgabendetails

Eine Aufgabe soll mindestens folgende Daten enthalten:

- Titel
- Beschreibung / Notiz
- Fälligkeitsdatum
- Priorität
- Erledigt-Status
- Erstellzeitpunkt
- Änderungszeitpunkt
- Erledigt-Zeitpunkt

Akzeptanzkriterien:

- Der Titel ist Pflicht.
- Notizen sind optional.
- Fälligkeit ist optional.
- Priorität kann gesetzt werden.
- Zeitstempel werden durch die Anwendung gepflegt.

### 6.4 Suche

Die Anwendung soll Aufgaben durchsuchen können.

Mindestanforderungen:

- Suche über Aufgabentitel
- Suche über Notiz/Beschreibung
- Einschränkung der angezeigten Aufgaben anhand des Suchbegriffs

Akzeptanzkriterien:

- Die Suche findet Aufgaben anhand des Titels.
- Die Suche findet Aufgaben anhand der Notiz.
- Ein leerer Suchbegriff zeigt wieder alle passenden Aufgaben der aktuellen Ansicht.

### 6.5 Backup

Die Anwendung soll eine einfache lokale Sicherung ermöglichen.

Mindestanforderungen:

- SQLite-Datenbank kann kopiert/gesichert werden.
- Sicherungsdatei erhält einen nachvollziehbaren Namen.

Akzeptanzkriterien:

- Der Benutzer kann eine Datenbanksicherung auslösen.
- Die Sicherungsdatei wird erfolgreich erzeugt.
- Der Pfad zur Sicherung wird dem Benutzer verständlich angezeigt.

## 7. Gewünschte UI-Eigenschaften

Die Oberfläche soll zunächst funktional, aber nicht lieblos sein.

Gewünschte Eigenschaften:

- klassische Menüleiste
- gut sichtbare Toolbar
- linke Navigation mit Listen und Smart Views
- mittlere Aufgabenliste
- optionaler rechter Detailbereich
- Aufgabenliste nicht als überladene technische Tabelle
- Details bei Bedarf separat anzeigen
- Fälligkeit, Favorit/Stern und Kurznotiz sollen unter oder neben dem Aufgabentitel erkennbar sein

Die Menü- und Toolbar-Struktur wird ausdrücklich als nützlich angesehen und soll erhalten bleiben.

## 8. Smart Views im MVP und danach

Für die erste Version können Smart Views noch einfach umgesetzt werden.

Wünschenswerte Ansichten:

- Eingang
- Heute
- Woche
- Favoriten
- Erledigt
- eigene Listen

Für das MVP können einige dieser Ansichten zunächst vereinfacht oder als spätere Ausbaustufe behandelt werden.

Priorität:

- Muss: eigene Listen und Aufgaben anzeigen
- Sollte: Heute, Woche, Erledigt
- Könnte: Favoriten

## 9. Nicht-Anforderungen für das MVP

Folgende Punkte gehören ausdrücklich nicht zum MVP:

- Cloud-Synchronisierung
- Multi-Geräte-Nutzung
- Benutzerkonten
- Login
- Rollen und Rechte
- geteilte Listen
- Kommentare
- Echtzeit-Zusammenarbeit
- REST-API-Anbindung
- mobile App
- Push-Benachrichtigungen
- komplexe Wiederholungsregeln
- Kalenderintegration
- Anhänge
- vollständige Tag-Verwaltung
- Mandantenfähigkeit
- Verschlüsselung auf Anwendungsebene

Diese Punkte sind nicht grundsätzlich ausgeschlossen, aber für das MVP zu groß.

## 10. Qualitätsanforderungen

### 10.1 Zuverlässigkeit

Die Anwendung soll Daten zuverlässig speichern und beim Neustart wieder laden.

### 10.2 Verständlichkeit

Fehlermeldungen sollen verständlich sein. Technische Details dürfen angezeigt werden, sollten aber nicht die einzige Information sein.

### 10.3 Wartbarkeit

Der Code soll so strukturiert sein, dass spätere Änderungen möglich bleiben:

- keine SQL-Logik direkt in Formularen
- Trennung von UI, Services, Repositories und Datenbankzugriff
- sprechende Namen
- ausreichend Kommentare

### 10.4 Datenschutz

Die Daten bleiben lokal auf dem Rechner des Nutzers.

Für das öffentliche GitHub-Repository gilt:

- keine echten privaten Aufgaben einchecken
- keine Datenbankdateien einchecken
- keine Backups einchecken
- keine Zugangsdaten einchecken

## 11. MVP-Erfolgskriterien

Das MVP gilt als arbeitsfähig, wenn folgende Punkte erfüllt sind:

- [ ] Anwendung startet ohne kritischen Fehler.
- [ ] Datenbank wird automatisch angelegt.
- [ ] Standardliste wird automatisch angelegt.
- [ ] Listen können angelegt werden.
- [ ] Listen können umbenannt werden.
- [ ] Listen können gelöscht werden.
- [ ] Aufgaben können angelegt werden.
- [ ] Aufgaben können bearbeitet werden.
- [ ] Aufgaben können gelöscht werden.
- [ ] Aufgaben können erledigt/offen gesetzt werden.
- [ ] Fälligkeit kann gespeichert werden.
- [ ] Priorität kann gespeichert werden.
- [ ] Notiz kann gespeichert werden.
- [ ] Aufgaben bleiben nach Neustart erhalten.
- [ ] Suche funktioniert.
- [ ] Backupfunktion funktioniert.

## 12. Offene fachliche Fragen

Folgende Punkte müssen später genauer entschieden werden:

- Sollen erledigte Aufgaben standardmäßig ausgeblendet werden?
- Sollen Listen mit Aufgaben gelöscht werden dürfen?
- Soll es einen Papierkorb geben?
- Wie sollen Favoriten/Sterne genau verwendet werden?
- Braucht V1 bereits Unteraufgaben?
- Sollen Fälligkeiten ohne Uhrzeit oder mit Uhrzeit verwaltet werden?
- Wie stark soll die Oberfläche an moderne Aufgabenlisten-Apps angelehnt sein?
- Wann wird eine TaskHost-kompatible Export-/Import-Struktur wichtig?

