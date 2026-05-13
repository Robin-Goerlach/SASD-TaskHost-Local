# TaskHost Local – Dokumentations-Checkliste

**Dokumentstatus:** Arbeitsfassung  
**Stand:** 2026-05-13  

## 1. Zweck

Diese Checkliste hilft dabei, die Dokumentation kontrolliert aufzubauen, ohne zu früh zu viele Details festzuschreiben oder wichtige Entscheidungen zu vergessen.

## 2. Grundsatz

Wir dokumentieren zuerst die Richtung, aber nicht oberflächlich. Die wichtigsten Entscheidungen müssen festgehalten werden, damit spätere Entwicklungsschritte nicht in eine falsche Richtung laufen.

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

## 4. ADRs

- [x] `ADR-001-Use-CSharp-WinForms-SQLite.md`
- [x] `ADR-002-Develop-Standalone-Before-TaskHost-Integration.md`
- [x] `ADR-003-TaskHost-Local-As-Future-Desktop-Client.md`

## 5. Noch zu ergänzen

- [ ] README überarbeiten
- [ ] README-Screenshot ergänzen
- [ ] Lizenzentscheidung treffen
- [ ] LICENSE-Datei ergänzen, falls Open Source gewünscht
- [ ] GitHub Issues aus Known Issues ableiten
- [ ] Build-/Run-Anleitung mit realem Pfad prüfen
- [ ] Screenshot-Ordner ergänzen
- [ ] Code-Kommentierungsstandard ergänzen
- [ ] Teststrategie ergänzen
- [ ] Release-Notizen für v0.1.0 ergänzen

## 6. Technische Checkliste

- [x] Repository angelegt
- [x] erster Commit erstellt
- [x] Dateirechte normalisiert
- [x] Build erfolgreich
- [ ] Laufzeitfehler beim Laden der Aufgaben beheben
- [ ] Datenbankinitialisierung manuell prüfen
- [ ] leere Aufgabenliste prüfen
- [ ] CRUD-Funktionen prüfen
- [ ] Backupfunktion prüfen

## 7. Fachliche Checkliste

- [x] Projektname festgelegt: TaskHost Local / SASD-TaskHost-Local
- [x] Verhältnis zu TaskHost festgelegt
- [x] V1 ohne Cloud/Sync festgelegt
- [x] Windows Forms als pragmatische V1-UI festgelegt
- [x] SQLite als lokale Datenhaltung festgelegt
- [x] Menü und Toolbar als gewünschte UI-Elemente festgehalten
- [x] rechter Detailbereich als Zielbild festgehalten
- [ ] genaue Smart-View-Regeln festlegen
- [ ] genaue Favoriten-Logik festlegen
- [ ] Unteraufgaben für spätere Version spezifizieren
- [ ] Import-/Exportformat definieren

## 8. UI-Checkliste

- [x] Menüleiste beibehalten
- [x] Toolbar beibehalten
- [x] linke Navigation vorgesehen
- [x] mittlere Aufgabenliste vorgesehen
- [x] rechter Detailbereich als Zielbild vorgesehen
- [x] Suche unten vorgesehen
- [x] Status/Datenbankpfad vorgesehen
- [ ] Tabellenansicht später durch Aufgabenzeilen verbessern
- [ ] Detailbereich technisch umsetzen
- [ ] Favorit/Stern in Aufgabenzeile ergänzen
- [ ] Fälligkeitsdatum unter Titel anzeigen
- [ ] erledigte Aufgaben visuell unterscheiden

## 9. Sicherheits-/Datenschutzcheckliste

- [x] keine Cloud in V1
- [x] keine Telemetrie in V1
- [x] lokale Datenhaltung
- [ ] `.gitignore` auf SQLite-Dateien prüfen
- [ ] `.gitignore` auf Backup-Dateien prüfen
- [ ] keine echten Daten in Screenshots verwenden
- [ ] README-Hinweis zu lokalen Daten ergänzen

## 10. Empfohlene nächste Arbeitsschritte

1. Dokumentationsdateien ins Repository übernehmen.
2. Commit erstellen: `Add initial project documentation`.
3. GitHub Issue für SQLite-Fehler anlegen.
4. SQLite-Fehler beheben.
5. README später mit kurzer Projektbeschreibung und Screenshot ergänzen.
6. Danach UI Richtung Detailpanel weiterentwickeln.

