--@author: Aaron
/*
**	Generisches Skript für SQL-Server Datenbanken, dass einem zu einer bestimmten ausgewählten Datenbank vom SQL-Server "vermisste" Indexe anzeigt.
**	Diese werden vom SQL-Server während einer Laufzeit in einer Statistik geführt. Darunter können auch Indexe sein, die zwischenzeitlich bereits angelegt wurden
**	oder auch welche die nur extrem selten einen geringen Effekt gehabt hätten. Hier geben die Daten aber bereits Anhaltspunkte, wie nötig die vorgeschlagenen Indizes wirklich sind.
**	Genauere Beschreibung der einzelnen Spaltenwerte sollten von den entsprechenden Microsoft Seiten eingeholt werden.
*/
SELECT *
FROM sys.dm_db_missing_index_details mid
JOIN sys.dm_db_missing_index_groups mig ON mid.index_handle = mig.index_handle
JOIN sys.dm_db_missing_index_group_stats migs ON mig.index_group_handle = migs.group_handle
WHERE mid.database_id = DB_ID();
