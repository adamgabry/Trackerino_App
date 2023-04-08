#Trackerino

## Documentation
[Wireframe on Figma](https://www.figma.com/file/Qz5WsGPIsTrbT9KmVWHumM/Trackerino?node-id=42%3A440&t=Wj4498UCIiKvloKl-1)

#DAL

###Migrace
    cd Trackerino.DAL/

| Příkaz | Význam |
| ----------- | ----------- |
| dotnet ef migrations add [nazev]  | Vytvoření/přidání migrace  |
| dotnet ef migrations remove   | Odstranění poslední migrace |
| dotnet ef database update   | Uplatnění změn v migracích na databázi |


Používáme LocalDB pro počáteční migraci.
TODO: SQLite pro testování v Azure.

###Testy
    1. Po spuštění testu v UserTests se do databáze vytvoří jeden uživatel.
    2. přes SQL Server Object Explorer se můžete podívat, že tam je.

TODO: udělat testy aby fungovaly v Azure DevOps.