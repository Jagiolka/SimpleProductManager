# SimpleProductManager

Bei dem SimpleProductManager handelt es sich um eine "Spielwiese" für C# .Net 7.0, bzw vor kurzem upgedated auf 8.0.

Die Anwendung ist aktuell nicht sonderlich hübsch (WPF Style), es sind (noch) keine Tests vorhanden und nicht alles funktioniert bugfrei.

Der SimpleProdcutManager besteht aus 2 Anwendungsteile:
- einer WPF-Anwendung
- einem ASP .Net Core Rest Service
  - mit Zugriff auf eine localdb Datenbank mit Hilfe von Entity Framework Core

Die localdb für die Anwendung kann einfach über das Powershell Skript angelegt werden.
