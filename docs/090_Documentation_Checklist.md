# TaskHost Local – Dokumentations-Checkliste

**Dokumentstatus:** Arbeitsfassung v0.2  
**Stand:** 2026-05-13

## 1. Zweck

Diese Checkliste hilft dabei, die Dokumentation kontrolliert aufzubauen, ohne zu früh zu viele Details festzuschreiben oder wichtige Entscheidungen zu vergessen.

## 2. Grundsatz

Wir dokumentieren zuerst die Richtung, aber nicht oberflächlich.

Die wichtigsten Entscheidungen müssen festgehalten werden, damit spätere Entwicklungsschritte nicht in eine falsche Richtung laufen.

## 3. Initiale Dokumente

- [x] `000_Project_Overview.md` – Projektüberblick
- [x] `010_Strategic_Positioning.md` – strategische Einordnung
- [x] `020_Lastenheft_MVP.md` – fachliche Anforderungen
- [x] `030_Pflichtenheft_MVP.md` – Umsetzung des MVP
- [x] `040_UI_Concept.md` – UI-Richtung
- [x] `050_Technical_Design.md` – technische Architektur
- [x] `060_Data_Model.md` – Datenmodell
- [x] `070_Roadmap.md` – Entwicklungsroadmap
- [x] `080_Known_Issues.md` – bekannte Fehler
- [x] `090_Documentation_Checklist.md` – diese Checkliste
- [x] `100_Manual_Test_Plan.md` – manueller Testplan

## 4. ADRs

- [x] `ADR-001-Use-CSharp-WinForms-SQLite.md`
- [x] `ADR-002-Develop-Standalone-Before-TaskHost-Integration.md`
- [x] `ADR-003-TaskHost-Local-As-Future-Desktop-Client.md`
- [x] `ADR-004-No-Network-Communication-In-MVP.md`
- [x] `ADR-005-Simple-SQLite-Schema-Before-Formal-Migrations.md`

## 5. Bereits erledigt

- [x] Repository angelegt
- [x] erster Commit erstellt
- [x] Dateirechte normalisiert
- [x] Build erfolgreich
- [x] initiale Dokumentation erstellt
- [x] Lastenheft v0.2 überarbeitet
- [x] Pflichtenheft v0.2 überarbeitet
- [x] restliche Dokumentation auf v0.2 synchronisiert
- [x] Strategie- und Code-Chat als getrennte Arbeitsbereiche festgelegt

## 6. Noch offen – technische Arbeit

- [ ] SQLite-Laufzeitfehler beim Laden der Aufgaben beheben
- [ ] Datenbankinitialisierung manuell prüfen
- [ ] Standardliste „Eingang“ prüfen
- [ ] leere Aufgabenliste prüfen
- [ ] Listen-CRUD prüfen
- [ ] Aufgaben-CRUD prüfen
- [ ] Backupfunktion prüfen
- [ ] Suche prüfen
- [ ] keine Netzwerkkommunikation prüfen

## 7. Noch offen – Repository und GitHub

- [ ] GitHub Issues aus Known Issues ableiten
- [ ] GitHub Issues aus Roadmap ableiten
- [ ] README-Screenshot ergänzen
- [ ] Screenshot-Ordner ergänzen
- [ ] Lizenzentscheidung treffen
- [ ] LICENSE-Datei ergänzen, falls Open Source gewünscht
- [ ] Release-Tag erst nach lauffähigem Stand setzen

## 8. Fachliche Checkliste

- [x] Projektname festgelegt: TaskHost Local / SASD-TaskHost-Local
- [x] Verhältnis zu TaskHost festgelegt
- [x] V1 ohne Cloud/Sync festgelegt
- [x] Windows Forms als pragmatische V1-UI festgelegt
- [x] SQLite als lokale Datenhaltung festgelegt
- [x] Menü und Toolbar als Muss-Anforderungen festgehalten
- [x] rechter Detailbereich als Zielbild festgehalten
- [x] Standardliste „Eingang“ als echte Liste festgelegt
- [x] Smart Views als gefilterte Ansichten festgelegt
- [x] „Alle Aufgaben“ und „Überfällig“ als wichtige Smart Views ergänzt
- [x] Prioritätswerte 0–3 festgelegt
- [x] Fälligkeit von aktiven Erinnerungen getrennt
- [ ] Favoriten-Logik technisch einführen
- [ ] Unteraufgaben für spätere Version spezifizieren
- [ ] Import-/Exportformat definieren

## 9. UI-Checkliste

- [x] Menüleiste beibehalten
- [x] Toolbar beibehalten
- [x] linke Navigation vorgesehen
- [x] mittlere Aufgabenliste vorgesehen
- [x] rechter Detailbereich als Zielbild vorgesehen
- [x] Suche unten vorgesehen
- [x] Status/Datenbankpfad vorgesehen
- [x] DataGridView als zulässige V1-Zwischenlösung beschrieben
- [ ] Tabellenansicht später durch Aufgabenzeilen verbessern
- [ ] Detailbereich technisch umsetzen
- [ ] Favorit/Stern in Aufgabenzeile ergänzen
- [ ] Fälligkeitsdatum unter Titel anzeigen
- [ ] erledigte Aufgaben visuell unterscheiden

## 10. Sicherheits-/Datenschutzcheckliste

- [x] keine Cloud in V1
- [x] keine Telemetrie in V1
- [x] keine Netzwerkkommunikation in V1
- [x] lokale Datenhaltung
- [x] öffentliche Repository-Nutzung mit Schutzregeln dokumentiert
- [ ] `.gitignore` auf SQLite-Dateien prüfen
- [ ] `.gitignore` auf Backup-Dateien prüfen
- [ ] keine echten Daten in Screenshots verwenden
- [ ] README-Hinweis zu lokalen Daten prüfen

## 11. Test- und Abnahmecheckliste

- [x] manueller Testplan dokumentiert
- [ ] App startet ohne Fehlerdialog
- [ ] Standardliste wird angezeigt
- [ ] Liste kann angelegt werden
- [ ] Liste kann umbenannt werden
- [ ] leere Liste kann gelöscht werden
- [ ] Liste mit Aufgaben wird nicht gelöscht
- [ ] Aufgabe kann angelegt werden
- [ ] Aufgabe kann bearbeitet werden
- [ ] Aufgabe kann gelöscht werden
- [ ] Aufgabe kann erledigt/offen gesetzt werden
- [ ] Suche funktioniert
- [ ] Backup funktioniert
- [ ] Daten bleiben nach Neustart erhalten

## 12. Später ergänzen

- [ ] automatisierte Unit-Tests
- [ ] Integrationstests mit temporärer SQLite-Datei
- [ ] Testdaten mit fiktiven Aufgaben
- [ ] Installations-/Deployment-Hinweise
- [ ] Release Notes für erste stabile Version
- [ ] ggf. englische Kurzfassung wichtiger Dokumente

## 13. Empfohlene nächste Arbeitsschritte

1. Dokumentationspatch einspielen.
2. Diff prüfen.
3. Commit erstellen, z. B. `Synchronize remaining project documentation`.
4. SQLite-Fehler im Code-Chat beheben.
5. App manuell nach `100_Manual_Test_Plan.md` prüfen.
6. README-Screenshot mit fiktiven Daten ergänzen.
