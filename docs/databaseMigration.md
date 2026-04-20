### 2. Migration
Migration-Befehle gebe ich im "Package Manager Console" des "Microsoft Visual Studio" ein.

__Wichtig:__ In der "Package Manager Console" erst das Default-Projekt auswählen. (hier _SimpleProductManager.Services_)
<img src="/docs/Images/vs_packageManagerConsole_migration_update.jpg" alt="vs packageManagerConsole migration update" width="1024px" />


#### Datenbank und Tabelle erstellen
Im Projekt __SimpleProductManager.Services__ befindet sich der Ordner __Migrations__ an diesem Ort befinden sich die Dateien für die Migration. <BR />
Die Migration erstellt Datenkbank und Tabellen anhand der Dateieninformationen. <BR />
Anhand des __ConnectionStrings__ aus der __appsettings.json__ weiß die Migration welcher Server migriert werden soll. <BR />

Mit dem Befehl 'Update-Database' in der Package Manager Console kann die Migration simple gestartet werden:
```
Update-Database -verbose
```

#### DatabaseSeeder
Um die Tabelle nicht immer manuell befüllen zu müssen habe ich den DatabaseSeeder hinzugefügt. <BR />
Sollte in der __appsettings.json__ die node "DatabaseSeeding" den Wert "SeedTestData" auf __true__ sein und die Tabellen keine Daten beinhalten, werden die Tabellen mit festgelegte Testdaten gefüllt. <BR />
```
"DatabaseSeeding": {
    "SeedTestData": true
  }
```
