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
GO

EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'Ad Hoc Distributed Queries', 1;
RECONFIGURE;
GO

CREATE VIEW BaseProjects AS
SELECT Id, Name, StartDate, Status
FROM OPENROWSET('MSOLEDBSQL', 
                'Server=sql2,1433;UID=sa;PWD=Password1', 
                'SELECT Id, Name, StartDate, Status FROM myDatabase.dbo.BaseProjects');
GO