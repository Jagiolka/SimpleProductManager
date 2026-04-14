[Datenbank anlegen mit EF Migration =>](databaseMigration.md)

## 1. Datenbank-Server mit Docker
Zur Bereitstellung der Datenbank nutze ich Docker über WSL unter Windows 11.

#### Docker - Image
Um einen MS SQL Server nutzen zu können, muss dieser erst als Docker-Image geladen werden. Daraufhin wird anhand des Images ein Container gestartet und der Server wird aufrufbar.
Ich nutze zum Starten die Befehlszeilen des Desktop-Docker-Terminal:
<BR />

#### Docker - Image pullen
``` BASH 
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

#### Docker - Container bereitzustellen
``` BASH
docker run -dit -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=.saP4ssword' -p 1433:1433 --name sqlServerContainer mcr.microsoft.com/mssql/server:2022-latest
```
<BR />

__ConnectionString: (im Projekt)__
```
Server=localhost,1433;Database=SimpleProductDatabase;User Id=sa;Password=.saP4ssword;TrustServerCertificate=True;
```
_(da es sich um eine Entwicklungsumgebung handelt => TrustServerCertificate=True;)_

<BR />
<p align="center">
  <table align="center" >
    <tr>
      <td align="center" style="border: none;">
        <img src="/docs/Images/Screenshot_ssms_login.jpg" alt="Anmeldung" width="400px" />
        <br /> Anmeldedaten im SSMS: 
        <br /> localhost mit Port aus der docker-Konfiguration 1433
      </td>
      <td align="center" style="border: none;">
        <img src="/docs/Images/Screenshot_ssms_databaseEmpty.jpg" alt="Bilderbeispiel" width="400px" />
        <br /> Datenbankserver ist noch leer
        <br /> Datenbank und Tabellen können mit der EF Migration angelegt werden
      </td>
    </tr>
  </table>
</p>

