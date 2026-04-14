# SimpleProductManager

### Beschreibung
Bei dem SimpleProductManager handelt es sich um eine "Spielwiese" auf C# .Net 9.0.

Der SimpleProductManager besteht aus 2 Anwendungsteilen:
- __REST Service:__
  - ASP .Net Core Rest Schnitstellen
  - Datenbereitstellung mittels Entity Framework Core
  - Datenbankstrukturbereitstellung durch EF-Migration
    - erstellt Datenbank und Tabellen
    - "DatabaseSeeder" erstellt auf Wunsch einen Testdatensatz (appsettings.json => "SeedTestData": true)
- __GUI:__
  - WPF Anwenderoberfläche
  - Produktübersicht
    - Filter Textbox
    - Tabelleneintrag mit Bearbeitungs- und Löschungs-Button
    - einfaches ErrorLog-Control
    - Hinzufügen/Löschen/Bearbeitung von Produkten und Produktkategorien


### ProductManager
<table align="center" >
  <tr>
    <td align="center">
      <img src="/docs/Images/spm01_overview.jpg" alt="SimpleProductManager" width="450px" />
    </td>
    <td>
      <img src="/docs/Images/spm02_overview_filter.jpg" alt="SimpleProductManager with filter" width="450px" />
    </td>
    <td>
      <img src="/docs/Images/spm01_overview_errorMsg.jpg" alt="SimpleProductManager with error" width="450px" />
    </td>
  </tr>  
</table>

### ProductEditor
<table align="center" >
  <tr>
    <td align="center">
      <img src="/docs/Images/spm_productEditor01_Dialog.jpg" alt="ProductEditor dialog" width="450px" />
    </td>
    <td>
      <img src="/docs/Images/spm_productEditor02_categoryControl.jpg" alt="ProductEditor dialog category control" width="450px" />
    </td>
    <td>
      <img src="/docs/Images/spm02_overview_errorMsg.jpg" alt="ProductEditor dialog with error" width="450px" />
    </td>    
  </tr>  
</table>

### Swagger - Services
<img src="/docs/Images/swagger_SimpleProductManager.Services.jpg" alt="ProductEditor dialog with error" width="450px" />



### Hilfen zur Ausführung:
- [Datenbank-Server bereitstellen mit Docker](/docs/databaseServerWithDocker.md)
- [Datenbank-Migration](/docs/databaseMigration.md)

