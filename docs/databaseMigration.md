### 2. Migration
Migration-Befehle gebe ich im "Package Manager Console" des "Microsoft Visual Studio" ein.


#### 2.2 Datenbank erstellen anhand der Migrations-Dateien
'Update-Database' erstellt/updated die Datenbankstruktur aus den Migrationsdaten in der Datenbank aus den Appsettings.
__Wichtig:__ In der "Package Manager Console" erst das Default-Projekt auswählen. (hier _SimpleProductManager.Services_)
```
Update-Database -verbose
```