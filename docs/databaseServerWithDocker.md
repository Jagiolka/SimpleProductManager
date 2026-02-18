## 1. Datenbank-Server mit Docker
Zur Bereitstellung der Datenbank nutze ich Docker über WSL unter Windows 11.

#### Docker Image
Um einen MS SQL Server nutzen zu können, muss dieser erst als Docker-Image geladen werden. Daraufhin wird anhand des Images ein Container gestartet und der Server wird aufrufbar.
Ich nutze zum Starten die Befehlszeilen des Desktop-Docker-Terminal:

__pull image__
``` BASH 
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

__Container bereitzustellen__:
``` BASH
docker run -dit -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=.saP4ssword' -p 1433:1433 --name sqlServerContainer mcr.microsoft.com/mssql/server:2022-latest
```

__ConnectionString:__ (im Projekt)
```
Server=localhost,1433;Database=SimpleProductDatabase;User Id=sa;Password=.saP4ssword;TrustServerCertificate=True;
```
_(da es sich um eine Entwicklungsumgebung handelt => TrustServerCertificate=True;)_

Anmeldedaten im SSMS
<img src="/Images/Screenshot_ssms_login.jpg" alt="Anmeldung" width="400px" />

Aktuell ist die Datenbank antürlich noch leer
<img src="/Images/Screenshot_ssms_databaseEmpty.jpg" alt="Bilderbeispiel" width="400px" />


<img src="/Images/Screenshot_ssms_databaseMigrated.jpg" alt="Bilderbeispiel" width="400px" />



[Datenbank anlegen mit EF Migration](databaseMigration.md)