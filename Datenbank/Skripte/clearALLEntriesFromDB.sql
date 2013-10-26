--@author: Aaron
DELETE FROM gr1.SupportTicket;
GO
DELETE FROM gr1.Download_History;
GO
DELETE FROM gr1.Lizenz_Zeitlich;
GO
DELETE FROM gr1.Lizenz_Versionsorientiert;
GO
DELETE FROM gr1.Benutzer_Zu_Lizenz;
GO
DELETE FROM gr1.Lizenz;
GO
DELETE FROM gr1.Benutzer WHERE NutzerNr != 'admin';
GO
DELETE FROM gr1.Build;
GO
DELETE FROM gr1.Subrelease;
GO
DELETE FROM gr1.Release;
GO
DELETE FROM gr1.Programm;
GO
DELETE FROM gr1.Rolle WHERE Rollenname NOT IN ('Administrator', 'Supporter', 'Member');