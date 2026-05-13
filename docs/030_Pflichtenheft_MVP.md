# TaskHost Local – MVP-Pflichtenheft

**Projekt:** SASD TaskHost Local  
**Dokument:** Pflichtenheft MVP  
**Version:** 0.2  
**Datum:** 2026-05-13  
**Status:** Arbeitsfassung, strategisch geschärft nach Überarbeitung des Lastenhefts  
**Sprache:** Deutsch  
**Bezug:** `docs/020_Lastenheft_MVP.md`, Version 0.2  
**Zugehörige Dokumente:**  
- `docs/010_Strategic_Positioning.md`
- `docs/020_Lastenheft_MVP.md`
- `docs/040_UI_Concept.md`
- `docs/050_Technical_Design.md`
- `docs/060_Data_Model.md`
- `docs/070_Roadmap.md`
- `docs/080_Known_Issues.md`
- `docs/adr/ADR-001-Use-CSharp-WinForms-SQLite.md`
- `docs/adr/ADR-002-Develop-Standalone-Before-TaskHost-Integration.md`
- `docs/adr/ADR-003-TaskHost-Local-As-Future-Desktop-Client.md`

---

## 1. Zweck dieses Pflichtenhefts

Dieses Pflichtenheft beschreibt, **wie** das im Lastenheft definierte MVP von **TaskHost Local** technisch und fachlich umgesetzt werden soll.

TaskHost Local ist eine lokale Windows-Aufgabenverwaltung mit C#, Windows Forms und SQLite. Die Anwendung soll kurzfristig produktiv nutzbar werden und bewusst auf Cloud, Synchronisierung, Benutzerverwaltung und Collaboration verzichten. Gleichzeitig soll die Umsetzung so erfolgen, dass eine spätere Einordnung in die TaskHost-Produktfamilie nicht erschwert wird.

Dieses Dokument richtet sich an die Entwicklung, an spätere Wartung und an die strategische Weiterentwicklung des Projekts. Es soll verhindern, dass die erste lauffähige Version zwar schnell entsteht, aber durch unsaubere Entscheidungen später schwer erweiterbar oder schwer debugbar wird.

---

## 2. Abgrenzung zwischen Lastenheft und Pflichtenheft

Das Lastenheft beschreibt die fachlichen Anforderungen aus Sicht des Nutzers und des Produkts.

Dieses Pflichtenheft beschreibt die geplante Umsetzung dieser Anforderungen:

- verwendete Technologie,
- grundlegende Architektur,
- Projektstruktur,
- Datenbankstruktur,
- Verhalten der wichtigsten Funktionen,
- UI-Verhalten,
- Sicherheits- und Datenschutzregeln,
- Abnahmekriterien.

Dieses Pflichtenheft ersetzt keine detaillierte Code-Dokumentation und keinen Quellcodekommentar. Es legt aber verbindlich fest, welche technische Richtung für das MVP gilt.

---

## 3. Umsetzungsschwerpunkt des MVP

Der Umsetzungsschwerpunkt liegt auf einer **einfachen, stabilen und lokal nutzbaren Aufgabenverwaltung**.

Für das MVP gilt:

- Funktion hat Vorrang vor Schönheit.
- Die App muss lokal und offline funktionieren.
- Die App soll schnell startbar und einfach bedienbar sein.
- Die Daten müssen zuverlässig lokal gespeichert werden.
- Der Code muss aufgeräumt, nachvollziehbar und gut kommentiert sein.
- Die UI darf zunächst schlicht sein, muss aber eine Weiterentwicklung in Richtung einer übersichtlichen Aufgabenlisten-App ermöglichen.
- Es darf keine unnötige Komplexität durch Cloud, Sync, Benutzerkonten, API-Anbindung oder Framework-Wechsel entstehen.

Das MVP soll keine Wegwerf-Bastelei sein. Es darf einfach sein, soll aber so strukturiert sein, dass spätere Erweiterungen möglich bleiben.

---

## 4. Technologiestack

### 4.1 Programmiersprache und Laufzeit

Für das MVP wird verwendet:

| Bereich | Entscheidung |
|---|---|
| Sprache | C# |
| Laufzeit | .NET 8 für Windows |
| UI-Technologie | Windows Forms |
| Datenbank | SQLite |
| SQLite-Zugriff | `Microsoft.Data.Sqlite` |
| ORM | Kein Entity Framework im MVP |
| Paketverwaltung | NuGet |
| IDE-Zielumgebung | Visual Studio / `dotnet` CLI |

### 4.2 Begründung

Windows Forms wird gewählt, weil für das Interimsprojekt eine schnelle, funktionale und gut debugbare Lösung wichtiger ist als eine moderne plattformübergreifende Oberfläche. C# passt zum vorhandenen Know-how und zur geplanten SASD-Entwicklungsrichtung.

SQLite wird gewählt, weil die Anwendung lokal, offline und ohne separate Datenbankinstallation funktionieren soll.

Entity Framework wird im MVP bewusst nicht verwendet. Die Datenstruktur ist klein genug, um mit klaren SQL-Anweisungen und Repository-Klassen einfacher, transparenter und schneller arbeiten zu können.

---

## 5. Architektur und Schichtenmodell

### 5.1 Grundstruktur

Die Anwendung wird in einer einfachen, aber klar getrennten Schichtenstruktur umgesetzt:

```text
Forms / UI
→ Services
→ Repositories
→ Database
→ SQLite
```

### 5.2 Bedeutung der Schichten

| Schicht | Aufgabe |
|---|---|
| Forms / UI | Darstellung, Benutzeraktionen, Dialoge, Menü, Toolbar |
| Services | Fachliche Abläufe, Validierung, Koordination zwischen UI und Repository |
| Repositories | Persistenzlogik, SQL-Abfragen, Mapping zwischen Datenbank und Modellen |
| Database | Verbindungserzeugung, Datenbankpfad, Initialisierung |
| Models | Datenobjekte wie Aufgabenlisten und Aufgaben |

### 5.3 Wichtige Architekturregel

**SQL darf nicht direkt im Formularcode stehen.**

Formulare dürfen Benutzeraktionen entgegennehmen und Services aufrufen. Datenbankdetails bleiben in Repository- und Database-Klassen.

Nicht gewünscht:

```csharp
private void btnSave_Click(object sender, EventArgs e)
{
    using var connection = new SqliteConnection(...);
    // SQL direkt im Formular
}
```

Gewünscht:

```csharp
private void btnSave_Click(object sender, EventArgs e)
{
    _taskService.UpdateTask(task);
}
```

Der Service ruft anschließend das passende Repository auf.

---

## 6. Projektstruktur

Für das MVP bleibt die Anwendung zunächst in einem WinForms-Projekt, aber mit klarer Ordnerstruktur.

```text
SASD-TaskHost-Local/
├── README.md
├── TaskHostLocal.sln
├── TaskHostLocal.WinForms/
│   ├── Program.cs
│   ├── MainForm.cs
│   ├── Forms/
│   │   ├── TaskEditForm.cs
│   │   └── ListEditForm.cs
│   ├── Models/
│   │   ├── TaskItem.cs
│   │   └── TaskList.cs
│   ├── Services/
│   │   ├── TaskService.cs
│   │   ├── ListService.cs
│   │   └── BackupService.cs
│   ├── Repositories/
│   │   ├── TaskRepository.cs
│   │   └── ListRepository.cs
│   ├── Database/
│   │   ├── DbConnectionFactory.cs
│   │   └── DatabaseInitializer.cs
│   └── TaskHostLocal.WinForms.csproj
└── docs/
```

Eine spätere Aufteilung in mehrere Projekte, zum Beispiel `TaskHostLocal.Core`, `TaskHostLocal.Data.Sqlite` und `TaskHostLocal.WinForms`, ist möglich, aber **nicht Teil des MVP**. Eine solche Aufteilung darf erst erfolgen, wenn der Nutzen klar ist und die Anwendung stabil läuft.

---

## 7. Datenbank und Datenmodell

### 7.1 Speicherort

Die lokale SQLite-Datenbank wird unterhalb des Benutzerprofils gespeichert:

```text
%AppData%\SASD\TaskHostLocal\taskhost.db
```

Beispiel:

```text
C:\Users\<Benutzer>\AppData\Roaming\SASD\TaskHostLocal\taskhost.db
```

Dieser Ort wird gewählt, weil dort normale Benutzer Schreibrechte haben und keine Administratorrechte erforderlich sind.

### 7.2 Datenbanktabellen im MVP

Das MVP verwendet mindestens folgende Tabellen:

```text
task_lists
tasks
```

Weitere Tabellen wie `subtasks`, `tags`, `attachments`, `reminders` oder `sync_state` sind für spätere Versionen vorgesehen und werden im MVP nicht zwingend angelegt.

### 7.3 Tabelle `task_lists`

Die Tabelle `task_lists` speichert echte Aufgabenlisten.

| Spalte | Typ | Pflicht | Beschreibung |
|---|---|---:|---|
| `id` | INTEGER PRIMARY KEY AUTOINCREMENT | ja | Technische ID der Liste |
| `name` | TEXT | ja | Anzeigename der Liste |
| `sort_order` | INTEGER | ja | Sortierreihenfolge |
| `created_at` | TEXT | ja | Erstellzeitpunkt |
| `updated_at` | TEXT | ja | Änderungszeitpunkt |

Regeln:

- Der Name darf nicht leer sein.
- Die Liste „Eingang“ wird beim ersten Start automatisch angelegt.
- „Eingang“ ist eine echte Liste, keine Smart View.
- Eine Liste darf im MVP nur gelöscht werden, wenn sie keine Aufgaben enthält.

### 7.4 Tabelle `tasks`

Die Tabelle `tasks` speichert Aufgaben.

| Spalte | Typ | Pflicht | Beschreibung |
|---|---|---:|---|
| `id` | INTEGER PRIMARY KEY AUTOINCREMENT | ja | Technische ID der Aufgabe |
| `list_id` | INTEGER | ja | Verweis auf `task_lists.id` |
| `title` | TEXT | ja | Titel der Aufgabe |
| `description` | TEXT | nein | Notiz oder Beschreibung |
| `due_date` | TEXT | nein | Optionales Fälligkeitsdatum |
| `priority` | INTEGER | ja | Priorität als numerischer Wert |
| `is_completed` | INTEGER | ja | 0 = offen, 1 = erledigt |
| `created_at` | TEXT | ja | Erstellzeitpunkt |
| `updated_at` | TEXT | ja | Änderungszeitpunkt |
| `completed_at` | TEXT | nein | Zeitpunkt der Erledigung |

Regeln:

- Jede Aufgabe gehört genau zu einer echten Liste.
- `list_id` darf nicht leer sein.
- Aufgaben in Smart Views werden nicht doppelt gespeichert.
- Smart Views sind gefilterte Abfragen auf `tasks`.
- `completed_at` ist nur gesetzt, wenn `is_completed = 1` ist.
- Beim Wiederöffnen einer Aufgabe wird `completed_at` auf `NULL` gesetzt.

### 7.5 Zeitformate

Zeitpunkte werden im MVP als Text gespeichert. Die Werte sollen einheitlich in einem ISO-ähnlichen Format abgelegt werden.

Empfehlung:

```text
yyyy-MM-ddTHH:mm:ss
```

Beispiel:

```text
2026-05-13T09:45:00
```

Für reine Fälligkeitsdaten kann ein Datumswert ohne Uhrzeit verwendet werden:

```text
yyyy-MM-dd
```

Eine spätere Version kann Zeit- und Zeitzonenregeln genauer formalisieren.

---

## 8. Datenbankinitialisierung

Beim Programmstart muss geprüft werden, ob die Datenbankdatei und die erforderlichen Tabellen vorhanden sind.

### 8.1 Ablauf

Beim Start:

1. Anwendungsdatenordner ermitteln.
2. Ordner `%AppData%\SASD\TaskHostLocal` anlegen, falls nicht vorhanden.
3. SQLite-Datenbankdatei öffnen oder erstellen.
4. Tabellen mit `CREATE TABLE IF NOT EXISTS` anlegen.
5. Prüfen, ob mindestens eine Aufgabenliste existiert.
6. Falls keine Liste existiert, Standardliste „Eingang“ anlegen.

### 8.2 Fehlerbehandlung

Wenn die Datenbank nicht angelegt oder geöffnet werden kann, muss die Anwendung eine verständliche Fehlermeldung anzeigen.

Die Fehlermeldung soll nicht nur eine technische Exception ausgeben, sondern dem Benutzer vermitteln:

- dass die lokale Datenbank nicht geöffnet werden konnte,
- welcher Datenbankpfad betroffen ist,
- dass die Anwendung dadurch nicht korrekt weiterarbeiten kann.

---

## 9. Listenverwaltung

### 9.1 Listen anzeigen

Die Anwendung zeigt echte Listen in der linken Navigation an.

Die Liste „Eingang“ muss vorhanden sein. Weitere vom Benutzer angelegte Listen werden darunter angezeigt.

### 9.2 Liste anlegen

Beim Anlegen einer Liste:

1. Benutzer wählt „Neue Liste“ über Menü, Toolbar oder Button.
2. Ein Eingabedialog wird geöffnet.
3. Benutzer gibt einen Namen ein.
4. Leere Namen werden abgelehnt.
5. Die Liste wird gespeichert.
6. Die Listenanzeige wird aktualisiert.
7. Die neue Liste kann ausgewählt werden.

### 9.3 Liste umbenennen

Beim Umbenennen einer Liste:

1. Benutzer wählt eine vorhandene Liste.
2. Benutzer ruft „Liste umbenennen“ auf.
3. Ein Dialog zeigt den aktuellen Namen.
4. Benutzer ändert den Namen.
5. Leere Namen werden abgelehnt.
6. Die Änderung wird gespeichert.
7. Die Listenanzeige wird aktualisiert.

### 9.4 Liste löschen

Für das MVP gilt eine sichere Löschregel:

- Nur leere Listen dürfen gelöscht werden.
- Vor dem Löschen muss eine Sicherheitsabfrage angezeigt werden.
- Listen mit Aufgaben dürfen im MVP nicht gelöscht werden.
- Wenn eine Liste Aufgaben enthält, wird dem Benutzer eine verständliche Meldung angezeigt.
- Die Standardliste „Eingang“ sollte im MVP nicht gelöscht werden können.

Diese Regel vermeidet versehentlichen Datenverlust und vereinfacht die erste stabile Version. Das spätere Löschen einer Liste inklusive Aufgaben, Verschieben vor dem Löschen oder Papierkorb-Funktionalität wird auf spätere Versionen verschoben.

---

## 10. Aufgabenverwaltung

### 10.1 Aufgaben anzeigen

Wenn eine echte Liste ausgewählt ist, werden die zugehörigen Aufgaben angezeigt.

Für das MVP ist eine tabellarische Darstellung über `DataGridView` zulässig. Die langfristige Zielrichtung bleibt jedoch eine kompaktere listenartige Darstellung mit Titel, Fälligkeit, Priorität, Stern/Favorit und kurzer Notizvorschau.

### 10.2 Aufgabe anlegen

Beim Anlegen einer Aufgabe:

1. Benutzer wählt „Neue Aufgabe“ über Menü, Toolbar oder Button.
2. Ein Aufgaben-Dialog wird geöffnet.
3. Benutzer gibt mindestens einen Titel ein.
4. Optional können Beschreibung/Notiz, Fälligkeitsdatum und Priorität gesetzt werden.
5. Leere Titel werden abgelehnt.
6. Die Aufgabe wird der aktuell ausgewählten echten Liste zugeordnet.
7. Die Aufgabe wird gespeichert.
8. Die Aufgabenliste wird aktualisiert.

### 10.3 Aufgabe bearbeiten

Beim Bearbeiten einer Aufgabe:

1. Benutzer wählt eine Aufgabe.
2. Benutzer ruft „Aufgabe bearbeiten“ auf.
3. Der Aufgaben-Dialog wird mit den vorhandenen Werten geöffnet.
4. Benutzer ändert die Werte.
5. Leere Titel werden abgelehnt.
6. Änderungen werden gespeichert.
7. `updated_at` wird aktualisiert.
8. Die Aufgabenliste wird neu geladen.

### 10.4 Aufgabe löschen

Für das MVP gilt:

- Aufgaben werden physisch aus der Datenbank gelöscht.
- Ein Papierkorb ist nicht Bestandteil des MVP.
- Vor dem Löschen muss immer eine Sicherheitsabfrage angezeigt werden.
- Nach erfolgreichem Löschen wird die Aufgabenliste aktualisiert.
- Wenn keine Aufgabe ausgewählt ist, wird keine Löschung durchgeführt und optional ein Hinweis angezeigt.

### 10.5 Aufgabe erledigen

Beim Markieren als erledigt:

```text
is_completed = 1
completed_at = aktueller Zeitpunkt
updated_at = aktueller Zeitpunkt
```

Die Aufgabenliste wird anschließend aktualisiert.

### 10.6 Aufgabe wieder öffnen

Beim Wiederöffnen einer erledigten Aufgabe:

```text
is_completed = 0
completed_at = NULL
updated_at = aktueller Zeitpunkt
```

Die Aufgabenliste wird anschließend aktualisiert.

### 10.7 Aufgabe verschieben

Jede Aufgabe besitzt bereits eine `list_id`. Dadurch ist das Verschieben in eine andere Liste fachlich und technisch vorbereitet.

Für das MVP gilt:

- Die Datenbankstruktur muss das Verschieben ermöglichen.
- Eine vollständige UI-Funktion „Aufgabe verschieben“ ist für die erste stabile CRUD-Version nicht zwingend erforderlich.
- Wenn die Aufgabe im Bearbeitungsdialog einer anderen Liste zugeordnet werden kann, muss die `list_id` korrekt aktualisiert werden.
- Falls die UI diese Funktion noch nicht anbietet, wird sie als spätere Erweiterung dokumentiert.

---

## 11. Fälligkeit, Priorität und Notizen

### 11.1 Fälligkeitsdatum

Das MVP unterstützt ein optionales Fälligkeitsdatum.

Regeln:

- Eine Aufgabe kann ohne Fälligkeitsdatum gespeichert werden.
- Eine Aufgabe kann mit Fälligkeitsdatum gespeichert werden.
- Das Fälligkeitsdatum wird in der Aufgabenliste oder im Detailbereich angezeigt.
- Das Fälligkeitsdatum dient im MVP nur der Anzeige und Filterung.
- Das MVP erzeugt keine aktiven Erinnerungen.
- Das MVP erzeugt keine Windows-Benachrichtigungen.
- Das MVP nutzt keinen Hintergrunddienst für Erinnerungen.

### 11.2 Aktive Erinnerungen

Aktive Erinnerungen sind **nicht** Teil des MVP.

Nicht enthalten:

- Windows Toast Notifications,
- Tray-Icon-Erinnerungen,
- Hintergrunddienst,
- Timer für Erinnerungen,
- Benachrichtigung bei geschlossenem Programm.

Diese Funktionen können später in einer eigenen Version geplant werden.

### 11.3 Wiederholungen

Wiederholungsaufgaben sind **nicht** Teil des MVP.

Nicht enthalten:

- täglich,
- wöchentlich,
- monatlich,
- benutzerdefinierte Wiederholungsregeln,
- automatische Neuanlage nach Erledigung.

### 11.4 Prioritäten

Prioritäten werden im MVP als einfache numerische Stufen umgesetzt:

| Wert | Bedeutung |
|---:|---|
| 0 | Normal / keine besondere Priorität |
| 1 | Niedrig |
| 2 | Mittel |
| 3 | Hoch |

Regeln:

- Der Standardwert ist `0`.
- Ungültige Werte sollen beim Speichern verhindert oder auf `0` zurückgesetzt werden.
- Die UI soll die Priorität verständlich anzeigen.
- Die Darstellung darf zunächst schlicht sein, zum Beispiel als Text oder Zahl.
- Eine farbliche oder ikonografische Darstellung ist eine spätere UI-Verbesserung.

### 11.5 Notizen / Beschreibung

Jede Aufgabe kann optional eine Beschreibung oder Notiz besitzen.

Regeln:

- Leere Beschreibung ist erlaubt.
- Beschreibung kann mehrzeilig sein.
- Die Suche berücksichtigt die Beschreibung.
- Die Aufgabenliste kann optional eine kurze Vorschau anzeigen.
- Der vollständige Text gehört in den Aufgaben-Dialog oder später in den Detailbereich.

---

## 12. Suche und Filter

### 12.1 Grundsuche im MVP

Die Suchfunktion muss mindestens Titel und Beschreibung/Notiz durchsuchen.

Mindestanforderung:

- Suche über Aufgabentitel.
- Suche über Beschreibung/Notiz.
- Suche funktioniert innerhalb der aktuell angezeigten Aufgabenmenge.

Für die erste Version ist es zulässig, die Suche auf die aktuell ausgewählte echte Liste zu beschränken.

### 12.2 Technische Umsetzung

Die Repository-Schicht muss Suchabfragen so zusammensetzen, dass keine ungültigen SQL-Fragmente entstehen.

Besonders zu beachten:

- `WHERE`-Klauseln dürfen nicht mit leeren Bedingungen erzeugt werden.
- Optionale Filter wie `list_id` und Suchtext müssen sauber kombiniert werden.
- SQL-Parameter müssen verwendet werden.
- Suchtext darf nicht per String-Konkatenation direkt in SQL eingebaut werden.

Gewünscht ist eine robuste Zusammensetzung, zum Beispiel:

```sql
WHERE list_id = $listId
  AND (title LIKE $search OR description LIKE $search)
```

oder bei globaler Suche:

```sql
WHERE title LIKE $search OR description LIKE $search
```

Ungültige Konstruktionen wie isolierte Operatoren oder leere Bedingungen sind zu vermeiden.

### 12.3 Globale Suche

Eine globale Suche über alle Listen ist wünschenswert, aber nicht zwingend für die erste stabile CRUD-Version.

Die technische Struktur soll aber so vorbereitet sein, dass `list_id` optional berücksichtigt werden kann.

---

## 13. Smart Views

### 13.1 Grundprinzip

Smart Views sind **keine echten Aufgabenlisten**.

Smart Views sind gefilterte Ansichten über Aufgaben.

Echte Listen stammen aus der Tabelle:

```text
task_lists
```

Smart Views werden durch Abfragen auf der Tabelle:

```text
tasks
```

gebildet.

### 13.2 Standardliste vs. Smart View

Die Liste „Eingang“ ist eine echte Liste.

Smart Views wie „Heute“, „Überfällig“, „Alle Aufgaben“ und „Erledigt“ sind keine Datensätze in `task_lists`.

Diese Trennung ist verbindlich, weil sonst spätere Migration, Import/Export und TaskHost-Kompatibilität erschwert würden.

### 13.3 Smart Views für MVP und Folgeversionen

Für das MVP muss mindestens die Anzeige echter Listen zuverlässig funktionieren.

Als nächste fachliche Ausbaustufe werden folgende Smart Views vorgesehen:

| Smart View | Bedeutung |
|---|---|
| Alle Aufgaben | Alle Aufgaben aus allen echten Listen |
| Heute | Aufgaben mit Fälligkeit heute |
| Überfällig | Offene Aufgaben mit Fälligkeitsdatum vor heute |
| Erledigt | Erledigte Aufgaben |
| Woche | Aufgaben mit Fälligkeit innerhalb der aktuellen Woche |
| Favoriten | Aufgaben mit Stern/Favorit, sobald dieses Feld vorhanden ist |

### 13.4 Priorisierung

Für die erste stabile Version:

- Muss: echte Listen anzeigen und Aufgaben der ausgewählten Liste anzeigen.
- Sollte bald folgen: Alle Aufgaben, Heute, Überfällig, Erledigt.
- Kann später folgen: Woche, Favoriten.

Favoriten setzen voraus, dass das Datenmodell um ein Feld wie `is_starred` erweitert wird. Diese Erweiterung ist nicht Teil des aktuellen Minimaldatenmodells.

---

## 14. Backup

### 14.1 Zweck

Das Backup im MVP dient dazu, die lokale SQLite-Datenbankdatei zu sichern.

Es ist kein fachlicher Export und kein Synchronisationsmechanismus.

### 14.2 Verhalten

Der Benutzer kann eine Sicherung der Datenbank auslösen.

Die Anwendung kopiert die aktuelle SQLite-Datenbankdatei an einen vom Benutzer gewählten oder automatisch vorgeschlagenen Sicherungsort.

Der Dateiname soll nachvollziehbar sein, zum Beispiel:

```text
taskhost-backup-2026-05-13T094500.db
```

### 14.3 Anforderungen

- Backup darf die laufende Anwendung nicht beschädigen.
- Backup muss eine verständliche Erfolgsmeldung anzeigen.
- Bei Fehlern muss eine verständliche Fehlermeldung erscheinen.
- Der Zielpfad soll dem Benutzer angezeigt werden.
- Backup ist Teil des MVP.
- JSON-Export und JSON-Import sind nicht Teil des MVP.

### 14.4 Abgrenzung zu Export/Import

Für spätere Versionen ist ein fachlicher Export/Import vorgesehen, idealerweise in einem TaskHost-kompatiblen JSON-Format.

Diese spätere Funktion soll nicht mit dem einfachen Datenbank-Backup verwechselt werden.

---

## 15. Benutzeroberfläche

### 15.1 Grundsatz

Die UI soll funktional, übersichtlich und schnell bedienbar sein.

Schönheit ist nicht das wichtigste Ziel des MVP. Trotzdem soll die UI nicht chaotisch wirken und später in Richtung einer moderneren Aufgabenlisten-App weiterentwickelt werden können.

### 15.2 Menüleiste

Die Anwendung muss eine klassische Menüleiste besitzen.

Die Menüleiste ist eine feste MVP-Anforderung.

Vorgeschlagene Menüstruktur:

```text
Datei
- Datenbank sichern
- Beenden

Listen
- Neue Liste
- Liste umbenennen
- Liste löschen

Aufgaben
- Neue Aufgabe
- Aufgabe bearbeiten
- Aufgabe löschen
- Erledigt/offen umschalten

Ansicht
- Aktualisieren
- später: Smart Views / Detailbereich

Hilfe
- Über TaskHost Local
```

### 15.3 Toolbar

Die Anwendung muss eine gut sichtbare Toolbar besitzen.

Die Toolbar ist eine feste MVP-Anforderung.

Häufige Aktionen sollen sowohl über Menü als auch über Toolbar erreichbar sein.

Vorgeschlagene Toolbar-Aktionen:

```text
Neue Aufgabe
Aufgabe bearbeiten
Aufgabe löschen
Erledigt/offen
Neue Liste
Liste umbenennen
Datenbank sichern
Aktualisieren
```

Die genaue Symbolgestaltung kann später verbessert werden. Für das MVP reichen Text-Buttons oder einfache Icons.

### 15.4 Hauptlayout

Das Grundlayout besteht aus:

```text
linke Navigation
mittlere Aufgabenliste
optional später rechter Detailbereich
```

Die erste Version darf eine zweispaltige Oberfläche verwenden:

```text
links: Listen
rechts: Aufgaben
```

Die Zielrichtung ist jedoch ein dreiteiligeres Aufgabenlisten-Layout:

```text
links: Navigation / Listen / Smart Views
mitte: kompakte Aufgabenliste
rechts: Details zur ausgewählten Aufgabe
```

### 15.5 Aufgabenliste

Für das MVP darf die Aufgabenliste als `DataGridView` umgesetzt werden.

Langfristig soll die Aufgabenliste nicht wie eine überladene technische Tabelle wirken. Gewünscht ist eine kompakte, gut lesbare Aufgabenzeile mit:

- Erledigt-Status,
- Titel,
- Fälligkeitsdatum,
- Priorität,
- optional Favorit/Stern,
- kurzer Notizvorschau.

Details sollen nicht dauerhaft alle in der Aufgabenliste stehen. Dadurch bleibt die Liste übersichtlich.

### 15.6 Detailbereich

Der rechte Detailbereich ist für das erste stabile CRUD-MVP nicht zwingend, aber als Zielbild verbindlich vorgesehen.

Beim Auswählen einer Aufgabe soll später rechts ein Detailbereich angezeigt werden können mit:

- Titel,
- Beschreibung/Notiz,
- Fälligkeit,
- Priorität,
- Erledigt-Status,
- später Unteraufgaben,
- später Anhänge,
- später Verlauf/Kommentare, falls strategisch gewünscht.

Der Detailbereich darf die erste Fehlerkorrektur und die erste CRUD-Stabilisierung nicht blockieren.

### 15.7 Dialoge

Für das MVP werden separate Dialoge verwendet:

- `ListEditForm` für Listenname,
- `TaskEditForm` für Aufgabendaten.

Dialoge müssen Eingaben validieren und dürfen keine ungültigen Daten speichern.

---

## 16. Fehlerbehandlung

### 16.1 Grundsatz

Fehler müssen verständlich angezeigt werden.

Technische Detailinformationen dürfen für Debugging hilfreich sein, sollten aber den Benutzer nicht unnötig verwirren.

Für das MVP ist eine einfache MessageBox-Fehleranzeige zulässig.

### 16.2 Datenbankfehler

Bei Datenbankfehlern soll die Anwendung anzeigen:

- dass ein Datenbankproblem aufgetreten ist,
- bei welcher Aktion es auftrat,
- optional die technische Fehlermeldung,
- wenn sinnvoll den Datenbankpfad.

### 16.3 Validierungsfehler

Validierungsfehler sind keine technischen Abstürze.

Beispiele:

- leerer Listenname,
- leerer Aufgabentitel,
- ungültige Priorität,
- Löschung einer nicht leeren Liste.

Diese Fälle werden mit verständlichen Hinweisen behandelt.

### 16.4 Known Issues

Aktuelle bekannte Fehler werden nicht dauerhaft im Pflichtenheft gepflegt.

Sie werden in folgendem Dokument dokumentiert:

```text
docs/080_Known_Issues.md
```

Das Pflichtenheft verweist auf dieses Dokument, beschreibt aber nicht jeden temporären Bug im Detail.

---

## 17. Sicherheit, Datenschutz und Netzwerkfreiheit

### 17.1 Lokale Datenhaltung

Alle fachlichen Daten des MVP bleiben lokal auf dem Rechner des Benutzers.

Die Anwendung speichert Aufgaben, Listen und Notizen ausschließlich in der lokalen SQLite-Datenbank.

### 17.2 Keine Netzwerkkommunikation

Für das MVP gilt:

- keine Cloud-Anbindung,
- keine TaskHost-API-Anbindung,
- keine automatische Synchronisierung,
- keine Telemetrie,
- keine automatische Update-Prüfung,
- keine HTTP-Clients zur Laufzeit,
- keine Übertragung von Aufgabeninhalten an externe Dienste.

Die Anwendung muss ohne Internetverbindung nutzbar sein.

### 17.3 Öffentliche Repository-Sicherheit

Da das Repository öffentlich sein kann, dürfen folgende Daten nicht eingecheckt werden:

- lokale SQLite-Datenbanken mit echten Daten,
- Backups mit echten Aufgaben,
- Zugangsdaten,
- API-Schlüssel,
- persönliche Notizen,
- private Konfigurationsdateien.

Die `.gitignore` muss diese Risiken soweit praktikabel abdecken.

### 17.4 Datenschutz im MVP

Da das MVP lokal bleibt und keine Netzwerkkommunikation durchführt, ist der Datenschutzumfang bewusst klein.

Trotzdem sind Aufgaben und Notizen potenziell persönliche Daten. Deshalb sollen Backup-Pfade, lokale Datenbankdateien und spätere Exporte mit Sorgfalt behandelt werden.

---

## 18. TaskHost-Kompatibilität als Schutzplanke

### 18.1 Grundsatz

TaskHost Local wird im MVP eigenständig entwickelt.

Es gibt im MVP:

- keine direkte TaskHost-API-Anbindung,
- keinen Login,
- keine Token-Verwaltung,
- keine Synchronisation,
- keine gemeinsamen Listen,
- keine Benutzer- oder Rechteverwaltung.

Trotzdem soll die Umsetzung fachlich und begrifflich kompatibel zur TaskHost-Produktfamilie bleiben.

### 18.2 Begriffe

Folgende Begriffe sollen langfristig anschlussfähig bleiben:

| TaskHost Local | Strategische Bedeutung |
|---|---|
| TaskList | echte Aufgabenliste |
| TaskItem | Aufgabe |
| Description / Note | Beschreibung oder Notiz |
| DueDate | Fälligkeit |
| Priority | Priorität |
| Completed | Erledigt-Status |
| SubTask | spätere Unteraufgabe |
| Attachment | späterer Anhang |
| Reminder | spätere Erinnerung |
| SmartView | gefilterte Ansicht |

### 18.3 Datenmodell-Kompatibilität

Das Datenmodell muss nicht sofort identisch mit TaskHost Web/API sein.

Es soll aber spätere Migration, Import/Export oder API-Anbindung nicht unnötig erschweren.

Deshalb gilt:

- Aufgaben gehören zu Listen.
- Smart Views speichern keine eigenen Aufgaben.
- Fälligkeit, Erledigt-Status und Priorität sind eigenständige Felder.
- Zeitstempel werden konsistent gespeichert.
- spätere Felder wie `is_starred`, `reminder_at`, `sync_id` oder `remote_id` dürfen nicht durch heutige Entscheidungen blockiert werden.

### 18.4 Spätere Integrationsoptionen

Spätere Optionen sind:

- Export/Import in TaskHost-kompatiblem JSON,
- optionaler TaskHost-API-Adapter,
- Desktop-Client mit lokalem Offline-Speicher,
- spätere Synchronisation.

Diese Optionen sind ausdrücklich keine MVP-Anforderungen.

---

## 19. Nicht umzusetzende Funktionen im MVP

Folgende Funktionen sind nicht Teil des MVP:

- Cloud-Synchronisierung,
- Multi-Geräte-Synchronisierung,
- Benutzerkonten,
- Login,
- Rollen/Rechte,
- geteilte Listen,
- Kommentare,
- Einladungen,
- REST-API-Anbindung,
- TaskHost-Server-Anbindung,
- mobile App,
- Web-App,
- Push-Benachrichtigungen,
- aktive Erinnerungen,
- Hintergrunddienst,
- komplexe Wiederholungsregeln,
- Kalenderintegration,
- vollständige Tag-Verwaltung,
- Anhänge,
- Unteraufgaben,
- Papierkorb,
- Audit-Log,
- Mandantenfähigkeit,
- automatische Updates.

Einige dieser Funktionen können später sinnvoll sein. Sie sollen aber das erste arbeitsfähige lokale Ergebnis nicht verzögern.

---

## 20. Abnahmekriterien für das MVP

Das MVP gilt als fachlich arbeitsfähig, wenn die folgenden manuellen Prüfungen erfolgreich durchgeführt wurden.

### 20.1 Start und Datenbank

```text
[ ] Anwendung baut erfolgreich.
[ ] Anwendung startet ohne Fehlermeldung.
[ ] Datenbankordner wird automatisch angelegt.
[ ] SQLite-Datenbankdatei wird automatisch angelegt.
[ ] Tabellen werden automatisch angelegt.
[ ] Standardliste „Eingang“ wird automatisch angelegt.
[ ] Die Anwendung kann nach Neustart erneut auf die Daten zugreifen.
```

### 20.2 Listen

```text
[ ] Liste kann angelegt werden.
[ ] Liste kann umbenannt werden.
[ ] Leerer Listenname wird abgelehnt.
[ ] Leere Liste kann nach Sicherheitsabfrage gelöscht werden.
[ ] Liste mit Aufgaben wird im MVP nicht gelöscht.
[ ] Standardliste „Eingang“ wird nicht versehentlich entfernt.
```

### 20.3 Aufgaben

```text
[ ] Aufgabe kann angelegt werden.
[ ] Leerer Aufgabentitel wird abgelehnt.
[ ] Aufgabe wird der ausgewählten Liste zugeordnet.
[ ] Aufgabe kann bearbeitet werden.
[ ] Notiz/Beschreibung kann gespeichert werden.
[ ] Fälligkeitsdatum kann gespeichert werden.
[ ] Aufgabe kann Priorität speichern.
[ ] Aufgabe kann nach Sicherheitsabfrage gelöscht werden.
[ ] Aufgabe kann als erledigt markiert werden.
[ ] completed_at wird beim Erledigen gesetzt.
[ ] Aufgabe kann wieder geöffnet werden.
[ ] completed_at wird beim Wiederöffnen zurückgesetzt.
[ ] Aufgaben bleiben nach Neustart erhalten.
```

### 20.4 Suche

```text
[ ] Suche findet Aufgaben über Titel.
[ ] Suche findet Aufgaben über Beschreibung/Notiz.
[ ] Suche erzeugt keine SQL-Fehler.
[ ] Leere Suche zeigt wieder die normale Aufgabenliste.
```

### 20.5 Backup

```text
[ ] Backup kann ausgelöst werden.
[ ] Backup-Datei wird erstellt.
[ ] Backup-Dateiname ist nachvollziehbar.
[ ] Speicherort wird dem Benutzer angezeigt.
[ ] Fehler beim Backup werden verständlich angezeigt.
```

### 20.6 UI

```text
[ ] Menüleiste ist vorhanden.
[ ] Toolbar ist vorhanden.
[ ] Häufige Aktionen sind über Menü erreichbar.
[ ] Häufige Aktionen sind über Toolbar erreichbar.
[ ] Linke Listen-Navigation ist vorhanden.
[ ] Aufgabenliste ist vorhanden.
[ ] Aufgabe kann per Auswahl bearbeitet werden.
```

### 20.7 Datenschutz und Netzwerkfreiheit

```text
[ ] Anwendung funktioniert ohne Internetverbindung.
[ ] Anwendung führt im MVP keine erkennbare Netzwerkkommunikation durch.
[ ] Es gibt keine Telemetrie-Funktion.
[ ] Keine lokale Datenbankdatei ist im Git-Repository enthalten.
[ ] Keine Backup-Datei mit echten Daten ist im Git-Repository enthalten.
```

---

## 21. Traceability: Abdeckung wichtiger Lastenheft-Anforderungen

Diese Tabelle verbindet zentrale Anforderungen aus dem Lastenheft mit der geplanten Umsetzung im Pflichtenheft.

| Lastenheft-Thema | Umsetzung im Pflichtenheft |
|---|---|
| Lokale Windows-App | Technologiestack: C#, .NET 8, Windows Forms |
| Lokale Datenhaltung | SQLite unter `%AppData%\SASD\TaskHostLocal` |
| Keine Cloud / kein Sync | Abschnitt Netzwerkfreiheit und Nicht-MVP-Funktionen |
| Strukturierter Code | Schichtenmodell Forms → Services → Repositories → Database |
| Standardliste „Eingang“ | Datenbankinitialisierung und Listenverwaltung |
| Listen anlegen/ändern/löschen | Abschnitt Listenverwaltung |
| Sichere Löschregeln | Listen löschen nur leer, Aufgaben mit Sicherheitsabfrage |
| Aufgaben anlegen/bearbeiten/löschen | Abschnitt Aufgabenverwaltung |
| Erledigt/offen | `is_completed` und `completed_at` Regeln |
| Fälligkeitsdatum | Abschnitt Fälligkeit |
| Keine aktiven Erinnerungen | Abgrenzung Erinnerungen/Wiederholungen |
| Priorität | numerische Prioritätsstufen 0 bis 3 |
| Notizen | Beschreibung/Notiz als optionales Feld |
| Suche | Suche über Titel und Beschreibung |
| Backup | Kopie der SQLite-Datenbank |
| Menüleiste | verbindliche UI-Anforderung |
| Toolbar | verbindliche UI-Anforderung |
| Detailbereich als Zielbild | UI-Abschnitt Detailbereich |
| Smart Views | Trennung echte Listen / Smart Views |
| TaskHost-Kompatibilität | eigener Schutzplanken-Abschnitt |
| Keine Telemetrie | Sicherheit, Datenschutz und Netzwerkfreiheit |
| Manuelle Abnahme | Abnahmekriterien |

---

## 22. Offene Punkte für spätere Versionen

Folgende Punkte sind bewusst offen und sollen später separat geplant werden:

- Unteraufgaben,
- Favoriten/Stern mit `is_starred`,
- Tags,
- Anhänge,
- aktive Erinnerungen,
- Wiederholungen,
- Papierkorb,
- JSON-Export/Import,
- TaskHost-API-Adapter,
- Synchronisation,
- plattformübergreifender Desktop-Client,
- modernere UI-Technologie,
- automatisierte Tests,
- Installer,
- Signierung.

Diese Punkte dürfen nicht versehentlich in das MVP hineinrutschen, solange die erste lokale Nutzbarkeit noch nicht stabil erreicht ist.

---

## 23. Änderungsnotiz Version 0.2

Version 0.2 dieses Pflichtenhefts wurde nach kritischem Abgleich mit dem überarbeiteten Lastenheft erstellt.

Wesentliche Ergänzungen:

- ausdrücklicher Bezug auf Lastenheft v0.2,
- klarere Smart-View-Regeln,
- Trennung von „Eingang“ als echter Liste und Smart Views,
- stärkere TaskHost-Kompatibilität,
- verbindliche Menü- und Toolbar-Anforderungen,
- präzisere Datenmodellbeschreibung,
- sichere Löschregeln,
- klare Abgrenzung von Fälligkeit, Erinnerungen und Wiederholungen,
- Prioritätsstufen,
- Netzwerkfreiheit und keine Telemetrie,
- erweiterte Abnahmekriterien,
- Traceability-Tabelle.

---

## 24. Kurzfazit

Das MVP von TaskHost Local soll eine kleine, lokale und zuverlässig nutzbare Windows-Aufgabenverwaltung sein.

Die technische Umsetzung bleibt bewusst einfach:

```text
C# + Windows Forms + SQLite
```

Die fachliche Richtung bleibt aber langfristig anschlussfähig:

```text
TaskHost Local jetzt eigenständig,
später möglicher Desktop-Client der TaskHost-Produktfamilie.
```

Die wichtigste Regel lautet:

> Schnell nutzbar werden, aber keine kurzfristigen Entscheidungen treffen, die spätere Wartung, Debugging oder TaskHost-Kompatibilität unnötig erschweren.
