# TaskHost Local – MVP-Lastenheft

**Projekt:** SASD TaskHost Local  
**Dokument:** MVP-Lastenheft  
**Datei:** `docs/020_Lastenheft_MVP.md`  
**Status:** Überarbeitete Arbeitsfassung  
**Sprache:** Deutsch  
**Version:** 0.2  
**Stand:** 2026-05-13  
**Repository:** `Robin-Goerlach/SASD-TaskHost-Local`  
**Bezug:** TaskHost-Produktfamilie / lokaler Windows-Client / strategischer Vorläufer eines möglichen Desktop-Clients

---

## 1. Zweck dieses Dokuments

Dieses Lastenheft beschreibt, **was** die erste arbeitsfähige Version von **TaskHost Local** leisten soll. Es legt die fachlichen Anforderungen, Abgrenzungen, Qualitätsziele und Abnahmekriterien für das MVP fest.

Das Dokument ist bewusst ausführlicher als eine reine Ideensammlung, aber noch kein endgültiges Produkt- oder Kundenlastenheft. Es dient als belastbare Grundlage für die weitere Entwicklung, für Debugging-Entscheidungen, für spätere Pflichtenhefte und für die strategische Einordnung innerhalb der TaskHost-Produktfamilie.

Die wichtigste Leitlinie lautet:

> **TaskHost Local soll kurzfristig eine lokal nutzbare, einfache und robuste Windows-Aufgabenverwaltung bereitstellen. Das Programm soll eigenständig funktionieren, aber fachlich und begrifflich so aufgebaut sein, dass eine spätere Integration in die TaskHost-Produktfamilie nicht unnötig erschwert wird.**

---

## 2. Hintergrund und Motivation

Der Nutzer benötigt kurzfristig eine einfache lokale Aufgabenverwaltung, die im Alltag tatsächlich verwendbar ist. Das langfristige Projekt **TaskHost** existiert bereits als größer gedachte Web-/API-orientierte Lösung mit potenzieller Synchronisierung, Benutzern, Zusammenarbeit und weiteren Funktionen. Dieses größere Ziel ist sinnvoll, aber für den unmittelbaren Eigenbedarf zu umfangreich und dadurch nicht schnell genug nutzbar.

TaskHost Local soll diese Lücke schließen:

- Es soll **lokal** auf einem Windows-PC laufen.
- Es soll **ohne Cloud, Server, Login und Synchronisierung** funktionieren.
- Es soll schnell genug entstehen, um praktisch genutzt werden zu können.
- Es soll trotzdem sauber strukturiert sein, damit spätere Erweiterungen und Debugging nicht unnötig schwer werden.
- Es soll nicht als Wegwerf-Prototyp entstehen, sondern als kleiner, geordneter Startpunkt.

Die Anwendung darf sich an bewährten Bedienkonzepten klassischer Aufgabenlisten-Anwendungen orientieren, soll aber **keine Kopie von Wunderlist** sein. Name, Logo, Gestaltung und Texte müssen eigenständig bleiben.

---

## 3. Strategische Einordnung

### 3.1 Verhältnis zu TaskHost

TaskHost Local ist Teil der TaskHost-Projektfamilie, ersetzt aber nicht das größere TaskHost-Projekt.

**TaskHost** bleibt das langfristige Hauptsystem:

- Web-App
- REST-API
- Benutzerverwaltung
- potenzielle Cloud- oder Server-Nutzung
- spätere Synchronisierung
- mögliche Zusammenarbeit mehrerer Nutzer

**TaskHost Local** ist dagegen zunächst:

- eine lokale Windows-Desktop-Anwendung,
- eine schnelle persönliche Arbeitsversion,
- ein eigenständiges Offline-Werkzeug,
- eine mögliche fachliche Vorstufe eines späteren Windows-Desktop-Clients.

### 3.2 Eigenständig, aber kompatibel

TaskHost Local wird für das MVP **eigenständig** entwickelt. Es soll in V1 keine direkte Abhängigkeit vom vorhandenen TaskHost-Repository, von einer TaskHost-API oder von einem Server geben.

Gleichzeitig soll die fachliche Modellierung so gewählt werden, dass spätere Wege offen bleiben:

- Export/Import in einem TaskHost-kompatiblen Format,
- spätere API-Anbindung,
- spätere Migration lokaler Daten,
- spätere Nutzung als Desktop-Client.

Daraus ergibt sich eine wichtige Qualitätsanforderung:

> Begriffe, Datenfelder und fachliche Konzepte sollen nicht willkürlich vom TaskHost-Universum abweichen, wenn es keinen guten Grund dafür gibt.

Beispiele für fachlich kompatible Begriffe:

- TaskList / Liste
- TaskItem / Aufgabe
- Description / Notiz / Beschreibung
- DueDate / Fälligkeitsdatum
- Priority / Priorität
- Completed / Erledigt
- Starred / Favorit
- SubTask / Unteraufgabe
- Attachment / Anhang
- Reminder / Erinnerung
- SmartView / gefilterte Systemansicht

---

## 4. Projektziel des MVP

Das MVP soll eine lokal installierbare und lokal nutzbare Windows-Aufgabenverwaltung bereitstellen.

Der Benutzer soll Aufgaben schnell erfassen, organisieren, wiederfinden und erledigen können. Der Schwerpunkt liegt auf Alltagstauglichkeit, Verständlichkeit und Stabilität, nicht auf perfektem Design oder maximaler Funktionsfülle.

### 4.1 Hauptziel

Das Hauptziel ist erreicht, wenn der Benutzer die Anwendung für einfache persönliche Aufgabenverwaltung einsetzen kann:

- Aufgaben in Listen erfassen,
- Aufgaben bearbeiten,
- Aufgaben erledigen oder wieder öffnen,
- Aufgaben löschen,
- Fälligkeiten und Prioritäten setzen,
- Notizen ergänzen,
- Aufgaben suchen,
- Daten lokal dauerhaft speichern,
- lokale Sicherung der Datenbank auslösen.

### 4.2 Nicht-Ziel des MVP

Das MVP soll **nicht** versuchen, eine vollständige Cloud-Aufgabenverwaltung nachzubauen. Insbesondere sind Synchronisierung, Mehrbenutzerbetrieb, mobile Apps und Zusammenarbeit ausdrücklich nicht Teil des MVP.

---

## 5. Zielgruppe und Nutzungskontext

### 5.1 Primäre Zielgruppe

Die primäre Zielgruppe des MVP ist zunächst der Entwickler und Nutzer selbst, also ein einzelner Anwender, der lokal auf einem Windows-System Aufgaben verwalten möchte.

### 5.2 Sekundäre Zielgruppe

Als sekundäre Zielgruppe kommen später in Frage:

- Nutzer, die eine einfache lokale Aufgabenverwaltung bevorzugen,
- Windows-Anwender, die keine Cloud-Lösung verwenden möchten,
- Nutzer, die eine spätere Verbindung zu TaskHost nutzen könnten,
- Entwickler, die das Projekt als überschaubares C#-/WinForms-/SQLite-Beispiel verstehen wollen.

### 5.3 Nutzungssituation

Die Anwendung wird im MVP typischerweise auf einem einzelnen Windows-PC verwendet. Es wird nicht vorausgesetzt, dass eine Internetverbindung vorhanden ist. Die Anwendung soll auch dann nutzbar sein, wenn kein Netzwerk verfügbar ist.

Typische Nutzung:

- Programm starten,
- Liste auswählen,
- Aufgabe erfassen,
- Aufgabe später wiederfinden,
- Aufgabe abhaken,
- wichtige Aufgaben über Fälligkeit oder Priorität erkennen,
- regelmäßig lokale Sicherung erstellen.

---

## 6. Geltungsbereich des MVP

### 6.1 Enthalten im MVP

Das MVP umfasst:

- lokale Windows-Desktop-Anwendung,
- lokale SQLite-Datenbank,
- automatische Datenbankinitialisierung,
- Standardliste „Eingang“,
- Verwaltung eigener Aufgabenlisten,
- Verwaltung von Aufgaben,
- Fälligkeitsdatum,
- Prioritätsstufen,
- Notizen/Beschreibung,
- Erledigt-/Offen-Status,
- Suche,
- einfache lokale Datenbanksicherung,
- Menüleiste,
- Toolbar,
- einfache, robuste Bedienoberfläche,
- klare Trennung zwischen Oberfläche, Fachlogik und Datenzugriff.

### 6.2 Nicht enthalten im MVP

Nicht Teil des MVP sind:

- Cloud-Synchronisierung,
- Multi-Geräte-Synchronisierung,
- Benutzerkonten,
- Login,
- Rollen/Rechte,
- geteilte Listen,
- Zusammenarbeit mehrerer Nutzer,
- Kommentare zwischen Nutzern,
- REST-API-Anbindung,
- direkte Verbindung zum TaskHost-Server,
- mobile Apps,
- Weboberfläche,
- aktive Hintergrundbenachrichtigungen,
- Push-Benachrichtigungen,
- komplexe Wiederholungsregeln,
- Kalenderintegration,
- vollständige Tag-Verwaltung,
- Anhänge,
- Mandantenfähigkeit,
- vollständige Import-/Export-Schnittstelle,
- perfekte visuelle Nachbildung anderer Produkte.

### 6.3 Später vorgesehene Erweiterungen

Für spätere Versionen können vorgesehen werden:

- Unteraufgaben / Checklisten,
- Favoriten / Stern-Markierung,
- bessere Smart Views,
- Export/Import als JSON,
- TaskHost-kompatibles Austauschformat,
- Anhänge,
- Tags,
- Erinnerungen,
- Wiederholungsaufgaben,
- rechter Detailbereich mit erweiterten Feldern,
- optionale API-Anbindung,
- optionale Synchronisierung.

Diese Punkte dürfen im MVP vorbereitet, aber nicht zulasten der schnellen Nutzbarkeit erzwungen werden.

---

## 7. Fachliche Anforderungen

Die Anforderungen sind nach Priorität klassifiziert:

- **Muss:** Für das MVP erforderlich.
- **Soll:** Für eine gute erste Nutzbarkeit wichtig, kann aber bei technischen Problemen kurzzeitig nachrangig behandelt werden.
- **Kann:** Sinnvoll, aber nicht zwingend für das erste MVP.
- **Später:** Bewusst außerhalb des MVP.

---

## 8. Listenverwaltung

### LH-F-010 – Standardliste „Eingang“

**Priorität:** Muss

Beim ersten Start der Anwendung muss automatisch eine Standardliste mit dem Namen **„Eingang“** angelegt werden, sofern noch keine Liste existiert.

Die Standardliste dient als Auffangort für neue Aufgaben. Sie ist eine echte Aufgabenliste, keine gefilterte Smart View.

### LH-F-020 – Aufgabenlisten anzeigen

**Priorität:** Muss

Die Anwendung muss vorhandene Aufgabenlisten anzeigen. Der Benutzer muss eine Liste auswählen können. Nach Auswahl einer Liste werden die zugehörigen Aufgaben angezeigt.

### LH-F-030 – Aufgabenliste anlegen

**Priorität:** Muss

Der Benutzer muss eine neue Aufgabenliste anlegen können. Eine Liste benötigt mindestens einen Namen.

### LH-F-040 – Aufgabenliste umbenennen

**Priorität:** Muss

Der Benutzer muss eine vorhandene Aufgabenliste umbenennen können.

### LH-F-050 – Aufgabenliste löschen

**Priorität:** Muss, eingeschränkt

Der Benutzer muss eine Aufgabenliste löschen können, sofern dadurch keine Aufgaben unbeabsichtigt verloren gehen.

Für das MVP gilt:

- Eine Liste darf nur gelöscht werden, wenn sie keine Aufgaben enthält.
- Vor dem Löschen muss eine Sicherheitsabfrage angezeigt werden.
- Das Löschen von Listen mit enthaltenen Aufgaben wird auf eine spätere Version verschoben.

Diese Einschränkung ist bewusst gewählt, weil ein Papierkorb und Wiederherstellungsfunktionen im MVP noch nicht vorgesehen sind.

### LH-F-060 – Trennung zwischen Standardliste und Smart Views

**Priorität:** Muss

Das Projekt muss fachlich zwischen echten Aufgabenlisten und gefilterten Ansichten unterscheiden.

- **„Eingang“** ist eine echte Liste.
- **„Heute“**, **„Überfällig“**, **„Alle Aufgaben“** oder **„Erledigt“** sind Smart Views bzw. gefilterte Ansichten.

Diese Trennung soll im Datenmodell und in der Oberfläche nicht vermischt werden.

---

## 9. Aufgabenverwaltung

### LH-F-100 – Aufgabe anlegen

**Priorität:** Muss

Der Benutzer muss in der aktuell ausgewählten Liste eine neue Aufgabe anlegen können.

Eine Aufgabe benötigt mindestens:

- Titel,
- zugehörige Liste,
- Erstellungszeitpunkt,
- Änderungszeitpunkt,
- Status offen/erledigt.

### LH-F-110 – Aufgabe anzeigen

**Priorität:** Muss

Die Anwendung muss Aufgaben der ausgewählten Liste anzeigen. Eine Aufgabe soll mindestens mit Titel und Erledigt-Status sichtbar sein.

Für eine bessere Nutzbarkeit sollen zusätzlich angezeigt werden können:

- Fälligkeitsdatum,
- Priorität,
- kurzer Notiz- oder Beschreibungshinweis,
- später optional Favorit/Stern.

### LH-F-120 – Aufgabe bearbeiten

**Priorität:** Muss

Der Benutzer muss vorhandene Aufgaben bearbeiten können.

Bearbeitbare Felder im MVP:

- Titel,
- Beschreibung / Notiz,
- Fälligkeitsdatum,
- Priorität,
- Erledigt-Status.

### LH-F-130 – Aufgabe löschen

**Priorität:** Muss

Der Benutzer muss eine Aufgabe löschen können.

Für das MVP gilt:

- Vor dem endgültigen Löschen muss eine Sicherheitsabfrage angezeigt werden.
- Ein Papierkorb ist im MVP nicht erforderlich.
- Die Datenbanksicherung soll als zusätzliche Schutzmaßnahme gegen Datenverlust dienen.

### LH-F-140 – Aufgabe als erledigt markieren

**Priorität:** Muss

Der Benutzer muss eine offene Aufgabe als erledigt markieren können. Beim Erledigen soll ein Erledigungszeitpunkt gespeichert werden.

### LH-F-150 – Aufgabe wieder öffnen

**Priorität:** Muss

Der Benutzer muss eine erledigte Aufgabe wieder auf offen setzen können. In diesem Fall soll der Erledigungszeitpunkt gelöscht oder entsprechend zurückgesetzt werden.

### LH-F-160 – Aufgabe einer Liste zuordnen

**Priorität:** Muss

Jede Aufgabe muss genau einer echten Aufgabenliste zugeordnet sein.

Das Verschieben einer Aufgabe in eine andere Liste ist für das MVP wünschenswert, aber nicht zwingend für die erste lauffähige Fehlerbereinigung. Es soll jedoch bei der Datenmodellierung berücksichtigt werden.

### LH-F-170 – Zeitstempel

**Priorität:** Soll

Aufgaben sollen folgende Zeitstempel besitzen:

- erstellt am,
- geändert am,
- erledigt am.

Die Zeitstempel dienen der Nachvollziehbarkeit und späteren Sortierung oder Synchronisierung.

---

## 10. Fälligkeit, Priorität und Notizen

### LH-F-200 – Fälligkeitsdatum

**Priorität:** Muss

Eine Aufgabe muss optional ein Fälligkeitsdatum besitzen können.

Das Fälligkeitsdatum dient dazu, Aufgaben in Ansichten wie „Heute“ oder „Überfällig“ auffindbar zu machen.

### LH-F-210 – Keine aktiven Erinnerungen im MVP

**Priorität:** Muss als Abgrenzung

Das MVP unterstützt Fälligkeitsdaten, aber noch keine aktiven Erinnerungen.

Nicht Teil des MVP:

- Hintergrundbenachrichtigungen,
- Windows-Toast-Benachrichtigungen,
- Erinnerungen bei geschlossener Anwendung,
- wiederkehrende Erinnerungen.

Diese Funktionen können später ergänzt werden.

### LH-F-220 – Prioritätsstufen

**Priorität:** Muss

Eine Aufgabe muss eine einfache Priorität besitzen können.

Für das MVP reichen wenige einfache Stufen, z. B.:

- Normal / keine besondere Priorität,
- Niedrig,
- Mittel,
- Hoch.

Die genaue technische Speicherung wird im Datenmodell beschrieben. Fachlich ist wichtig, dass der Benutzer einfache Dringlichkeitsunterschiede abbilden kann, ohne ein komplexes Priorisierungssystem lernen zu müssen.

### LH-F-230 – Notiz / Beschreibung

**Priorität:** Muss

Eine Aufgabe muss eine optionale Notiz oder Beschreibung besitzen können.

Diese Beschreibung soll für zusätzliche Informationen genutzt werden, z. B.:

- kurze Erläuterung,
- Kontext,
- Link-Hinweis,
- Erinnerung an Details,
- Arbeitsnotiz.

Für das MVP genügt ein einfaches mehrzeiliges Textfeld.

---

## 11. Suche und Filter

### LH-F-300 – Suche über Aufgaben

**Priorität:** Muss

Die Anwendung muss eine Suchfunktion bereitstellen.

Für das MVP soll die Suche mindestens folgende Felder berücksichtigen:

- Aufgabentitel,
- Beschreibung / Notiz.

### LH-F-310 – Suche innerhalb der aktuellen Liste

**Priorität:** Soll

Die Suche soll mindestens innerhalb der aktuell ausgewählten Liste funktionieren.

Eine spätere globale Suche über alle Listen ist sinnvoll und soll vorbereitet werden.

### LH-F-320 – Globale Suche

**Priorität:** Kann / später

Eine Suche über alle Aufgaben und alle Listen ist wünschenswert, muss aber nicht zwingend in der ersten nutzbaren Version enthalten sein.

---

## 12. Ansichten und Smart Views

### LH-F-400 – Eigene Listen

**Priorität:** Muss

Der Benutzer muss eigene Listen auswählen und deren Aufgaben anzeigen können.

### LH-F-410 – Alle Aufgaben

**Priorität:** Soll

Die Anwendung soll eine Ansicht „Alle Aufgaben“ bereitstellen, in der alle nicht gelöschten Aufgaben sichtbar sind.

Diese Ansicht ist wichtig, um Aufgaben wiederzufinden, unabhängig davon, in welcher Liste sie liegen.

### LH-F-420 – Heute

**Priorität:** Soll

Die Anwendung soll eine Ansicht „Heute“ bereitstellen, in der Aufgaben mit heutigem Fälligkeitsdatum angezeigt werden.

### LH-F-430 – Überfällig

**Priorität:** Soll

Die Anwendung soll eine Ansicht „Überfällig“ bereitstellen, in der offene Aufgaben mit vergangenem Fälligkeitsdatum angezeigt werden.

Diese Ansicht ist fachlich wichtig, weil überfällige Aufgaben sonst leicht unsichtbar werden.

### LH-F-440 – Erledigt

**Priorität:** Soll

Die Anwendung soll eine Ansicht für erledigte Aufgaben bereitstellen.

### LH-F-450 – Woche

**Priorität:** Kann

Eine Ansicht „Woche“ für Aufgaben der nächsten Tage ist wünschenswert, aber nicht zwingend für das erste MVP.

### LH-F-460 – Favoriten

**Priorität:** Kann / später

Eine Ansicht „Favoriten“ oder „Wichtig“ ist wünschenswert. Sie setzt eine Stern- oder Favoriten-Markierung voraus.

Für das MVP darf diese Funktion zunächst zurückgestellt werden, wenn dadurch die erste Nutzbarkeit schneller erreicht wird.

---

## 13. Benutzeroberfläche und Bedienkonzept

### LH-UI-010 – Grundlayout

**Priorität:** Muss

Die Anwendung soll eine klare, einfache Oberfläche besitzen. Für das MVP ist ein klassisches Windows-Forms-Layout ausreichend.

Das Grundlayout soll sich an folgender Struktur orientieren:

- linke Spalte: Listen und spätere Smart Views,
- mittlerer Bereich: Aufgabenliste,
- optional rechter Bereich: Details zur ausgewählten Aufgabe,
- oben: Menüleiste und Toolbar,
- unten oder oben im Inhaltsbereich: Suche und Statusinformationen.

### LH-UI-020 – Menüleiste

**Priorität:** Muss

Die Anwendung muss eine klassische Menüleiste besitzen.

Die Menüleiste ist für das MVP eine feste UI-Anforderung, weil sie Funktionen auffindbar macht und zu einem pragmatischen Windows-Desktop-Werkzeug passt.

Mögliche Menüs:

- Datei,
- Listen,
- Aufgaben,
- Ansicht,
- Hilfe.

### LH-UI-030 – Toolbar

**Priorität:** Muss

Die Anwendung muss eine gut sichtbare Toolbar besitzen.

Die Toolbar soll häufige Aktionen schnell erreichbar machen, z. B.:

- neue Aufgabe,
- Aufgabe bearbeiten,
- Aufgabe löschen,
- erledigt/offen umschalten,
- neue Liste,
- Backup.

Menü und Toolbar wurden als wichtige positive Elemente des bisherigen Entwurfs bewertet und sollen in der weiteren Entwicklung erhalten bleiben.

### LH-UI-040 – Aufgabenliste nicht überladen

**Priorität:** Soll

Die Aufgabenliste soll nicht dauerhaft mit allen Detailfeldern überladen werden.

Für die erste lauffähige Version darf eine einfache tabellarische Darstellung verwendet werden. Die Zielrichtung ist jedoch eine listenartige Darstellung, bei der eine Aufgabe kompakt sichtbar ist mit:

- Titel,
- Erledigt-Status,
- Fälligkeitsdatum,
- Priorität,
- kurzer Notizvorschau,
- später optional Favorit/Stern.

### LH-UI-050 – Detailbereich

**Priorität:** Soll

Die Anwendung soll mittelfristig einen optional sichtbaren oder fest eingeblendeten rechten Detailbereich besitzen.

Beim Auswählen einer Aufgabe sollen dort zusätzliche Details angezeigt und bearbeitet werden können, z. B.:

- vollständiger Titel,
- Beschreibung,
- Fälligkeitsdatum,
- Priorität,
- Status,
- später Unteraufgaben,
- später Anhänge,
- später Notizen oder Verlauf.

Der Detailbereich soll helfen, die Aufgabenliste übersichtlich zu halten.

### LH-UI-060 – Bedienung vor Schönheit

**Priorität:** Muss

Für das MVP gilt: Funktion und Bedienbarkeit haben Vorrang vor perfekter Optik.

Die Oberfläche darf schlicht sein, muss aber verständlich, stabil und verwendbar sein.

### LH-UI-070 – Keine exakte Kopie fremder Produkte

**Priorität:** Muss

Die Oberfläche darf sich an bewährten Aufgabenlisten-Konzepten orientieren, darf aber keine exakte Kopie von Wunderlist oder einem anderen Produkt sein.

Eigenständige Namen, Texte, Farben und Gestaltung sind zu verwenden.

---

## 14. Datenspeicherung und Backup

### LH-DATA-010 – Lokale Speicherung

**Priorität:** Muss

Alle Daten des MVP müssen lokal gespeichert werden.

Für das MVP ist eine lokale SQLite-Datenbank vorgesehen.

### LH-DATA-020 – Speicherort

**Priorität:** Muss

Die Anwendungsdaten sollen nicht im Programmverzeichnis gespeichert werden, sondern in einem benutzerspezifischen Anwendungsdatenordner, z. B.:

```text
%AppData%\SASD\TaskHostLocal\taskhost.db
```

Dadurch werden Administratorrechte vermieden und die Daten bleiben vom Programmcode getrennt.

### LH-DATA-030 – Automatische Initialisierung

**Priorität:** Muss

Die Anwendung muss beim Start prüfen, ob die Datenbank existiert. Falls nicht, muss sie automatisch angelegt und initialisiert werden.

### LH-DATA-040 – Datenbeständigkeit

**Priorität:** Muss

Angelegte Listen und Aufgaben müssen nach dem Schließen und erneuten Starten der Anwendung erhalten bleiben.

### LH-DATA-050 – Lokale Datenbanksicherung

**Priorität:** Muss

Die Anwendung muss eine einfache Backup-Funktion besitzen, mit der die lokale SQLite-Datenbank in eine Sicherungsdatei kopiert werden kann.

Die Sicherungsdatei soll einen nachvollziehbaren Namen erhalten, z. B. mit Zeitstempel.

### LH-DATA-060 – Export/Import nicht im MVP

**Priorität:** Muss als Abgrenzung

Ein fachlicher JSON-Export oder JSON-Import ist nicht Teil des MVP.

Er soll jedoch als spätere Erweiterung vorgesehen werden, insbesondere für:

- Migration,
- Datenaustausch,
- TaskHost-kompatiblen Export,
- mögliche spätere API-Anbindung.

---

## 15. Datenschutz, Sicherheit und Netzwerkfreiheit

### LH-SEC-010 – Keine Netzwerkkommunikation im MVP

**Priorität:** Muss

Die Anwendung darf im MVP keine Netzwerkkommunikation durchführen.

Insbesondere gibt es im MVP:

- keine Telemetrie,
- keine Cloud-Anbindung,
- keine automatische Übertragung von Aufgaben,
- keine Verbindung zu einem TaskHost-Server,
- keine Benutzeranmeldung,
- keine Online-Lizenzprüfung.

### LH-SEC-020 – Lokale Datenhoheit

**Priorität:** Muss

Die Aufgaben und Listen des Benutzers verbleiben lokal auf dem verwendeten Rechner.

Der Benutzer soll nachvollziehen können, wo die Daten gespeichert sind.

### LH-SEC-030 – Keine sensiblen Daten im Repository

**Priorität:** Muss

Das öffentliche Repository darf keine echten Benutzerdaten, keine privaten Aufgaben, keine produktiven Datenbanken, keine Zugangsdaten und keine privaten Sicherungen enthalten.

Die `.gitignore` muss sicherstellen, dass lokale Datenbankdateien und Build-Artefakte nicht versehentlich committed werden.

### LH-SEC-040 – Einfache Schutzmaßnahmen gegen Datenverlust

**Priorität:** Muss

Da im MVP kein Papierkorb vorgesehen ist, müssen Löschaktionen bestätigt werden. Zusätzlich muss eine Backup-Funktion verfügbar sein.

---

## 16. Technische Randbedingungen

Dieses Lastenheft beschreibt primär fachliche Anforderungen. Dennoch sind einige technische Randbedingungen bereits festgelegt, weil sie zur strategischen Entscheidung des MVP gehören.

### LH-T-010 – Zielplattform

**Priorität:** Muss

Das MVP ist eine Windows-Desktop-Anwendung.

Linux-Unterstützung ist für das MVP nicht erforderlich.

### LH-T-020 – Technologieentscheidung

**Priorität:** Muss

Für das MVP wird die folgende technische Richtung verwendet:

- C#,
- .NET 8 oder kompatibel,
- Windows Forms,
- SQLite,
- Microsoft.Data.Sqlite,
- kein Entity Framework im MVP.

Diese Entscheidung dient der schnellen lokalen Nutzbarkeit und einfachen Debugbarkeit.

### LH-T-030 – Strukturierter Code

**Priorität:** Muss

Auch wenn das MVP pragmatisch entwickelt wird, soll der Code strukturiert und verständlich bleiben.

Insbesondere gilt:

- keine SQL-Logik direkt im Formularcode,
- klare Trennung zwischen UI, Services, Repositories und Datenbankzugriff,
- verständliche Namen,
- erklärende Kommentare,
- XML-Dokumentationskommentare bei wichtigen Klassen und Methoden.

### LH-T-040 – Kein großer Architekturumbau ohne Entscheidung

**Priorität:** Muss

Größere technische Richtungsänderungen sollen nicht nebenbei im Code erfolgen.

Vorher strategisch zu prüfen sind insbesondere:

- Einführung von Entity Framework,
- Aufteilung in mehrere Projekte,
- neue Tabellen mit langfristiger Wirkung,
- API-/Sync-Anbindung,
- anderes UI-Framework,
- grundlegende Änderung des Datenmodells.

---

## 17. Qualitätsanforderungen

### LH-Q-010 – schnelle Nutzbarkeit

**Priorität:** Muss

Das MVP soll möglichst schnell in einen Zustand gebracht werden, in dem der Benutzer es im Alltag testweise verwenden kann.

### LH-Q-020 – Stabilität vor Funktionsfülle

**Priorität:** Muss

Eine kleine stabile Funktion ist wichtiger als viele halbfertige Funktionen.

### LH-Q-030 – Verständlichkeit

**Priorität:** Muss

Der Code und die Dokumentation sollen so verständlich sein, dass spätere Weiterentwicklung und Debugging erleichtert werden.

### LH-Q-040 – Erweiterbarkeit

**Priorität:** Soll

Die Struktur soll spätere Erweiterungen ermöglichen, ohne das MVP unnötig zu verkomplizieren.

### LH-Q-050 – TaskHost-Kompatibilität

**Priorität:** Soll

Die fachlichen Konzepte sollen so gewählt werden, dass eine spätere Verbindung zur TaskHost-Produktfamilie realistisch bleibt.

Diese Kompatibilität ist keine Anforderung an eine sofortige API-Anbindung, sondern eine Schutzplanke für Namen, Datenfelder und Modellierung.

---

## 18. Abnahmekriterien für das MVP

Das MVP gilt erst dann als arbeitsfähig, wenn ein manueller Testlauf erfolgreich durchgeführt wurde.

### 18.1 Technische Mindestabnahme

```text
[ ] Projekt lässt sich wiederherstellen / restore ausführen.
[ ] Projekt lässt sich bauen.
[ ] Anwendung startet ohne Fehlermeldung.
[ ] Datenbank wird automatisch angelegt, falls sie fehlt.
[ ] Standardliste „Eingang“ wird automatisch angelegt.
[ ] Anwendung kann geschlossen und neu gestartet werden.
```

### 18.2 Listen-Abnahme

```text
[ ] Vorhandene Listen werden angezeigt.
[ ] Neue Liste kann angelegt werden.
[ ] Liste kann umbenannt werden.
[ ] Leere Liste kann nach Sicherheitsabfrage gelöscht werden.
[ ] Liste mit Aufgaben wird im MVP nicht versehentlich gelöscht.
```

### 18.3 Aufgaben-Abnahme

```text
[ ] Aufgabe kann angelegt werden.
[ ] Aufgabe erscheint in der ausgewählten Liste.
[ ] Aufgabe bleibt nach Neustart erhalten.
[ ] Aufgabe kann bearbeitet werden.
[ ] Aufgabe kann gelöscht werden, aber erst nach Sicherheitsabfrage.
[ ] Aufgabe kann als erledigt markiert werden.
[ ] Erledigte Aufgabe kann wieder geöffnet werden.
[ ] Fälligkeitsdatum kann gesetzt und geändert werden.
[ ] Priorität kann gesetzt und geändert werden.
[ ] Notiz/Beschreibung kann gespeichert werden.
```

### 18.4 Suche und Backup

```text
[ ] Suche findet Aufgaben über Titel.
[ ] Suche findet Aufgaben über Notiz/Beschreibung.
[ ] Backup-Funktion erstellt eine Sicherungsdatei.
[ ] Backup-Datei hat einen nachvollziehbaren Namen.
[ ] Speicherort der Backup-Datei wird dem Benutzer angezeigt oder ist nachvollziehbar.
```

### 18.5 Datenschutz und Offline-Verhalten

```text
[ ] Anwendung benötigt keine Internetverbindung.
[ ] Anwendung führt keine erkennbare Netzwerkkommunikation aus.
[ ] Aufgaben werden lokal gespeichert.
[ ] Keine lokale Datenbankdatei ist Teil des Git-Repositories.
```

---

## 19. Bewusste Verschiebungen nach dem MVP

Folgende Anforderungen sind sinnvoll, werden aber bewusst nicht in das erste MVP aufgenommen:

### 19.1 Unteraufgaben / Checklisten

Unteraufgaben sind für eine spätere Version sinnvoll, aber nicht zwingend für die erste Nutzbarkeit.

### 19.2 Favoriten / Stern

Favoriten sind für eine Aufgabenverwaltung sehr hilfreich. Sie sollen vorbereitet werden, können aber nach der ersten stabilen CRUD-Version umgesetzt werden.

### 19.3 Anhänge

Anhänge erhöhen Komplexität bei Speicherorten, Dateipfaden und Backup. Sie sollen später geplant werden.

### 19.4 Tags

Tags sind nützlich, aber für V1 nicht notwendig.

### 19.5 Erinnerungen

Aktive Erinnerungen erfordern UI-, Zeitgeber- und möglicherweise Windows-Benachrichtigungslogik. Sie werden verschoben.

### 19.6 Wiederholungsaufgaben

Wiederholungen sind fachlich komplexer als einfache Fälligkeitsdaten und werden verschoben.

### 19.7 API-Anbindung und Synchronisierung

Eine spätere Verbindung zur TaskHost-API ist strategisch interessant, aber nicht Teil des MVP.

---

## 20. Risiken und offene Punkte

### 20.1 Risiko: zu frühe Integration

Wenn TaskHost Local zu früh mit TaskHost API, Login oder Sync verbunden wird, verliert das Projekt seinen Hauptnutzen: schnelle lokale Nutzbarkeit.

**Gegenmaßnahme:** V1 bleibt offline und eigenständig.

### 20.2 Risiko: zu großes MVP

Wenn zu viele Funktionen gleichzeitig umgesetzt werden, verzögert sich die erste Nutzbarkeit.

**Gegenmaßnahme:** Muss-Anforderungen priorisieren, Kann-/Später-Funktionen bewusst zurückstellen.

### 20.3 Risiko: Datenmodell läuft von TaskHost weg

Wenn TaskHost Local fachlich völlig andere Begriffe oder Felder verwendet, wird spätere Integration schwer.

**Gegenmaßnahme:** Begriffe und Konzepte an der TaskHost-Produktfamilie orientieren, ohne V1 zu überfrachten.

### 20.4 Risiko: UI wird zu tabellarisch

Eine reine technische Tabelle kann für Aufgabenverwaltung unübersichtlich wirken.

**Gegenmaßnahme:** Für V1 ist eine Tabelle akzeptabel, aber die Zielrichtung bleibt listenartige Darstellung mit separatem Detailbereich.

### 20.5 Risiko: Datenverlust durch Löschen

Ohne Papierkorb kann unbeabsichtigtes Löschen problematisch sein.

**Gegenmaßnahme:** Sicherheitsabfragen, eingeschränktes Löschen von Listen, Backup-Funktion.

---

## 21. Zusammenfassung der MVP-Leitplanken

Für das MVP gelten folgende Leitplanken:

```text
TaskHost Local ist lokal.
TaskHost Local ist offline.
TaskHost Local ist Windows-orientiert.
TaskHost Local nutzt C#, Windows Forms und SQLite.
TaskHost Local bleibt zunächst eigenständig.
TaskHost Local bleibt fachlich kompatibel zur TaskHost-Produktfamilie.
TaskHost Local bietet Listen, Aufgaben, Fälligkeiten, Prioritäten, Notizen, Suche und Backup.
TaskHost Local enthält im MVP keine Cloud, keine Synchronisierung, keine Benutzerkonten und keine API-Anbindung.
TaskHost Local behält Menü und Toolbar als feste UI-Elemente.
TaskHost Local soll langfristig eine übersichtliche Aufgabenliste mit separatem Detailbereich erhalten.
```

---

## 22. Änderungshinweise zu Version 0.2

Diese überarbeitete Fassung ergänzt und schärft insbesondere:

- Menüleiste und Toolbar als Muss-Anforderungen,
- klare Trennung von Standardliste „Eingang“ und Smart Views,
- Ergänzung von „Alle Aufgaben“ und „Überfällig“,
- stärkere TaskHost-Kompatibilität als Qualitätsanforderung,
- explizite Netzwerkfreiheit und keine Telemetrie im MVP,
- sichere Löschregeln,
- Abgrenzung von Backup gegenüber fachlichem Export/Import,
- klare Abgrenzung von Fälligkeit gegenüber aktiven Erinnerungen,
- Prioritätsstufen,
- Zielrichtung einer listenartigen Aufgabenanzeige mit Detailbereich,
- ausführliche manuelle MVP-Abnahmekriterien.
