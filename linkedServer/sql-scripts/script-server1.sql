
CREATE DATABASE myDatabase;
GO

USE myDatabase;
GO

CREATE TABLE Projects (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,  
    Description NVARCHAR(255) NOT NULL,   
    Reason NVARCHAR(255) NOT NULL
);
GO

INSERT INTO Projects (Id, Description, Reason) 
VALUES ('a79609df-cddd-4dd5-92b3-34ef0751b483', 'Neues Softwaremodul entwickeln', 'Kundennachfrage');

EXEC sp_addlinkedserver 
    @server = 'LinkedServer', 
    @srvproduct = '', 
    @provider = 'MSOLEDBSQL', 
    @datasrc = 'sql2,1433'; 
    
EXEC sp_addlinkedsrvlogin 
    @rmtsrvname = 'LinkedServer', 
    @useself = 'FALSE', 
    @locallogin = NULL, 
    @rmtuser = 'sa', 
    @rmtpassword = 'Password1';
GO

CREATE SYNONYM BaseProjects FOR
 LinkedServer.myDatabase.dbo.BaseProjects;
GO