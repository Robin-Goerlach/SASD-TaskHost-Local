# TaskHost Local – Strategische Einordnung

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck des Dokuments

Dieses Dokument legt fest, wie **TaskHost Local** strategisch einzuordnen ist. Es beantwortet insbesondere folgende Fragen:

- Ist TaskHost Local ein eigenständiges Produkt?
- Ist TaskHost Local der zukünftige Windows-Client von TaskHost?
- Wird TaskHost Local sofort in TaskHost integriert?
- Wie verhindern wir, dass zwei konkurrierende Aufgabenverwaltungen entstehen?

## 2. Ausgangslage

Es existiert bereits das Projekt **TaskHost**. Dieses Projekt soll langfristig als umfassendere Aufgabenverwaltungsplattform entwickelt werden. Der bestehende TaskHost-Ansatz geht eher in Richtung Web-/API-System und kann perspektivisch Funktionen wie Backend, Weboberfläche, spätere Synchronisierung, Benutzerverwaltung und Collaboration unterstützen.

Der aktuelle praktische Bedarf ist jedoch kurzfristiger:

- lokale Aufgabenverwaltung
- Windows-Desktop-App
- sofort benutzbar
- keine Cloud
- keine Synchronisierung
- keine Benutzerverwaltung
- keine komplexe Serverinfrastruktur

Die Sorge besteht darin, dass TaskHost zwar strategisch wichtig ist, aber zu lange braucht, bis er im Alltag wirklich nutzbar wird.

## 3. Strategische Hauptentscheidung

TaskHost Local wird als **eigenständiges lokales Desktop-Projekt** entwickelt, bleibt aber fachlich Teil der TaskHost-Produktfamilie.

Die Leitentscheidung lautet:

> **TaskHost Local ist die kurzfristig nutzbare lokale Windows-Variante der TaskHost-Idee. Es wird zunächst eigenständig entwickelt, soll aber langfristig als möglicher Desktop-Client oder Offline-Client von TaskHost vorbereitet bleiben.**

## 4. Verhältnis zu TaskHost

### 4.1 TaskHost

TaskHost bleibt das langfristige Hauptprojekt.

TaskHost steht perspektivisch für:

- Web-App
- REST-API
- serverbasierte Datenhaltung
- mögliche Synchronisierung
- mögliche Zusammenarbeit mehrerer Benutzer
- mögliche Nutzung über verschiedene Geräte
- langfristige Produktplattform

### 4.2 TaskHost Local

TaskHost Local steht kurzfristig für:

- lokale Windows-App
- einfache Aufgabenverwaltung
- SQLite-Datenbank
- keine Cloud-Abhängigkeit
- keine Anmeldung
- schnelle Nutzbarkeit
- Interims- und Produktivwerkzeug für den lokalen Alltag

## 5. Warum keine sofortige Integration?

Eine sofortige Integration in TaskHost würde das Projekt unnötig verlangsamen.

Eine direkte Integration würde sofort Fragen auslösen wie:

- Wie erfolgt der Login?
- Wie werden Tokens gespeichert?
- Wie wird die API konfiguriert?
- Wie funktioniert Offline-Nutzung?
- Wie werden Konflikte zwischen lokalem Stand und Serverstand gelöst?
- Wie wird synchronisiert?
- Wie werden Benutzerrechte abgebildet?
- Wie werden Listen geteilt?
- Wie werden Serverausfälle behandelt?

Diese Fragen sind für das langfristige Produkt wichtig, aber für die erste lokale Nutzversion zu schwergewichtig.

Für V1 gilt daher:

> **Keine TaskHost-API-Anbindung in der ersten Version.**

## 6. Warum trotzdem fachlich kompatibel bleiben?

Auch wenn TaskHost Local zuerst eigenständig ist, soll es nicht in eine völlig andere Richtung laufen.

Deshalb sollen Begriffe und Datenstrukturen so gewählt werden, dass sie später zu TaskHost passen:

- Liste / TaskList
- Aufgabe / TaskItem
- Notiz / Note oder Description
- Fälligkeit / DueDate
- Priorität / Priority
- Favorit / Starred
- Erledigt / Completed
- Unteraufgabe / SubTask
- Anhang / Attachment
- Erinnerung / Reminder
- intelligente Ansicht / SmartView

So bleibt eine spätere Migration, ein Import/Export oder eine API-Anbindung möglich.

## 7. Eigenständig oder späterer Windows-Client?

Die Antwort ist bewusst zweistufig:

1. **Heute:** TaskHost Local ist eigenständig.
2. **Später:** TaskHost Local kann ein Windows-Client von TaskHost werden.

Das bedeutet:

- V1 speichert ausschließlich lokal in SQLite.
- V1 benötigt keinen TaskHost-Server.
- V1 ist ohne Internet nutzbar.
- Das Datenmodell soll nicht unnötig inkompatibel zu TaskHost werden.
- Eine spätere Sync- oder API-Schicht darf architektonisch möglich bleiben.

## 8. Name des Projekts

Der empfohlene Name lautet:

> **TaskHost Local**

Der Repository-Name lautet aktuell:

> **SASD-TaskHost-Local**

Dieser Name ist sinnvoll, weil er drei Dinge kommuniziert:

1. Das Projekt gehört zum SASD-Umfeld.
2. Das Projekt gehört zur TaskHost-Familie.
3. Es handelt sich um die lokale Variante.

Der alternative Name **TaskFlow** wurde verworfen, weil er generischer wirkt, bereits mehrfach im Aufgabenverwaltungsumfeld genutzt wird und die Verbindung zu TaskHost schwächer macht.

## 9. Rechtliche und kommunikative Abgrenzung

TaskHost Local darf sich funktional an bewährten Aufgabenverwaltungs-Apps orientieren. Es darf aber nicht als exakte Kopie oder als Wiederbelebung fremder Marken erscheinen.

Daher gilt:

- kein Markenname fremder Produkte im Projektnamen
- keine fremden Logos
- keine kopierten Icons
- kein 1:1 Nachbau geschützter UI-Elemente
- keine irreführende Beschreibung als offizieller Nachfolger
- eigene Farbwelt und eigene Screenshots

Formulierungen wie „inspiriert von klassischen Aufgabenlisten-Anwendungen“ sind besser als „Wunderlist-Klon“.

## 10. Repository-Strategie

TaskHost Local bleibt zunächst in einem eigenen Repository.

Vorteile:

- geringere Komplexität
- klarer Projektfokus
- unabhängige Builds
- keine Vermischung mit PHP-/JavaScript-Websystemen
- einfacher für schnelle Experimente

Eine spätere Integration ist möglich, z. B. als:

```text
TaskHost/
├── api/
├── app/
├── clients/
│   └── windows/
└── docs/
```

Diese Integration sollte aber erst erfolgen, wenn TaskHost Local stabil genug ist.

## 11. Langfristiges Zielbild

Langfristig sind drei Wege denkbar:

### Variante A: TaskHost Local bleibt eigenständig

Die App bleibt eine einfache lokale Windows-Aufgabenverwaltung ohne Serverbindung.

### Variante B: TaskHost Local wird TaskHost Desktop

Die App wird zum offiziellen Windows-Desktop-Client der TaskHost-Produktfamilie.

### Variante C: TaskHost Local bleibt lokale Edition, TaskHost Desktop wird neu gebaut

TaskHost Local bleibt die schnelle Offline-Edition. Ein späterer moderner Desktop-Client könnte z. B. mit Avalonia entwickelt werden.

Die aktuelle Architektur soll alle drei Varianten offenhalten.

## 12. Aktuelle Empfehlung

Die aktuelle Empfehlung lautet:

1. TaskHost Local eigenständig weiterentwickeln.
2. V1 auf lokale Nutzbarkeit konzentrieren.
3. TaskHost-Kompatibilität begrifflich und strukturell berücksichtigen.
4. Keine Cloud-/Sync-/API-Komplexität in V1 aufnehmen.
5. UI so entwickeln, dass sie später näher an ein modernes Aufgabenlisten-Layout gebracht werden kann.
6. Bekannte Fehler dokumentieren und gezielt beheben.

