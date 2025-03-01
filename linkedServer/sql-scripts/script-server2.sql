
CREATE DATABASE myDatabase;
GO

USE myDatabase;
GO

CREATE TABLE BaseProjects (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    Name NVARCHAR(255) NOT NULL,    
    StartDate DATETIME2 NOT NULL,      
    Status NVARCHAR(255) NOT NULL
);
GO

INSERT INTO BaseProjects (Id, Name, StartDate, Status) 
VALUES ('a79609df-cddd-4dd5-92b3-34ef0751b483', 'Projekt Alpha', '2025-01-11 10:00:00', 'In Progress');
GO