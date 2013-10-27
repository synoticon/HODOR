--@author: Aaron
--Get rid of obsolete tables only if they still exist
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Modul_Zu_Subrelease')
BEGIN
	DROP TABLE gr1.Modul_Zu_Subrelease;
END
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Rolle_Zu_Rechte')
BEGIN
	DROP TABLE gr1.Rolle_Zu_Rechte;
END
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Rechte')
BEGIN
	DROP TABLE gr1.Rechte;
END

--non-obsolete tables to delete
DROP TABLE gr1.Download_History;
GO
DROP TABLE gr1.Lizenz_Zeitlich;
GO
DROP TABLE gr1.Lizenz_Versionsorientiert;
GO
DROP TABLE gr1.Benutzer_Zu_Lizenz;
GO
DROP TABLE gr1.Lizenz;
GO
DROP TABLE gr1.Benutzer;
GO
DROP TABLE gr1.Build;
GO
DROP TABLE gr1.Subrelease;
GO
DROP TABLE gr1.Release;
GO
DROP TABLE gr1.Programm;
GO
DROP TABLE gr1.Rolle;