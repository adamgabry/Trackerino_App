# Documentation
[Wireframe on Figma](https://www.figma.com/file/Qz5WsGPIsTrbT9KmVWHumM/Trackerino?node-id=42%3A440&t=Wj4498UCIiKvloKl-1)

***DAL***

#Migrace
Při přidání migrace:
dotnet ef migrations add [nazev]
Odstraneni:
dotnet ef migrations remove

#Nové migraci / zmenach v DBcontextu
v Trackerino.DAL/
dotnet ef database update

1. Používáme LocalDB pro počáteční migraci

#Testy
    1. Po spuštění testu v UserTests se do databáze vytvoří jeden uživatel.
    2. přes SQL Server Object Explorer se můžete podívat, že tam je.

TODO: udělat testy aby fungovaly v Azure DevOps.