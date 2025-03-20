# SimpleProductManager

### Beschreibung
Bei dem SimpleProductManager handelt es sich um eine "Spielwiese" auf C# .Net 9.0.

Der SimpleProdcutManager besteht aus 2 Anwendungsteilen:
- __Service:__ ASP .Net Core Rest Schnitstellen zur Datenbereitstellung mittels Entity Framework Core. Datenbankstrukturbereitstellung durch Migration.
- __GUI:__ Anwenderoberfläche mit WPF

### 1. Datenbank-Server mit Docker
Zur Bereitstellung der Datenbank nutze ich Docker über WSL unter Windows 11.

#### Docker Image
Um einen MS SQL Server nutzen zu können, müssen wir erst ein Docker Image beziehen.
Dazu folgende Zeilen in der Docker BASH ausführen (ich nutze dazu das Docker.Desktop Terminal):

__pull image:__
``` BASH 
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Um aus dem Image ein Container bereitzustellen (damit ein aufrufbaren SQL Server):
``` BASH
docker run -dit -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=.saP4ssword' -p 1433:1433 --name sqlServerContainer mcr.microsoft.com/mssql/server:2022-latest
```

__ConnectionString:__
```
Server=localhost,1433;Database=SimpleProductDatabase;User Id=sa;Password=.saP4ssword;TrustServerCertificate=True;
```
(da es sich um eine Entwicklungsumgebung handelt => TrustServerCertificate=True;)

### 2. Migration
Migration-Befehle gebe ich im "Package Manager Console" des "Microsoft Visual Studio" ein.

#### 2.1 Migration Initialisierung
__!__ _Dieser Schritt ist nur nötig sollten noch keine Migrations-Dateien im Projekt existieren._


Mit folgendem Befehl wird der Projekt-Ordner "Migrations" angelegt, und die Dateien \<DatenbankName\>ContextModelSnapshot.cs und \<Datum\>_\<DatenbankName\>.cs erstellt. 
Als Beispiel aus dem Projekt: 
_20250312204530_Init_SimpleProductDatabase.cs_
_SimpleProductDatabaseContextModelSnapshot.cs_
```
Add-Migration SimpleProductDatabase
```

#### 2.2 Datenbank erstellen anhand der Migrations-Dateien
'Update-Database' erstellt/updated die Datenbankstruktur aus den Migrationsdaten in der Datenbank aus den Appsettings.
__Wichtig:__ In der "Package Manager Console" erst das Default-Projekt auswählen. (hier _SimpleProductManager.Services_)
```
Update-Database -verbose
```
