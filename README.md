# SimpleProductManager

### Beschreibung
Bei dem SimpleProductManager handelt es sich um eine "Spielwiese" auf C# .Net 9.0.

Die Anwendung ist aktuell nicht sonderlich hübsch (WPF Style), es sind auch keine Tests vorhanden.

Der SimpleProdcutManager besteht aus 2 Anwendungsteilen:
- __Service:__ ASP .Net Core Rest Schnitstellen zur Datenbereitstellung mittels Entity Framework Core. Datenbankstrukturbereitstellung durch Migration.
- __GUI:__ Anwenderoberfläche mit WPF

##### Datenbank mit Docker
Zur Bereitstellung der Datenbank nutze ich Docker.
Dazu folgende Zeilen in der Docker BASH verwenden:

pull image:
``` BASH 
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

start docker from image (as example: docker terminal):
``` BASH
docker run -dit -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=.saP4ssword' -p 1433:1433 --name sqlServerContainer mcr.microsoft.com/mssql/server:2022-latest
```

ConnectionString:
```
Server=localhost,1433;Database=SimpleProductDatabase;User Id=sa;Password=.saP4ssword;TrustServerCertificate=True;
```
(da es sich um eine Entwicklungsumgebung handelt => TrustServerCertificate=True;)

##### Migration
Migration-Initialisierung im "Microsoft Visual Studio" "Package Manager Console".
Mit folgendem Befehl wird der Projekt-Ordner "Migrations" angelegt, und die Dateien \<DatenbankName\>ContextModelSnapshot.cs und \<Datum\>_\<DatenbankName\>.cs erstellt. 
Dieser Schritt ist nur nötig sollten noch keine Migrations-Dateien existieren.
```
Add-Migration SimpleProductDatabase
```

'Update-Database' erstellt die Datenbankstruktur aus den Migrationsdaten in der Datenbank aus den Appsettings.
```
Update-Database -verbose
```






