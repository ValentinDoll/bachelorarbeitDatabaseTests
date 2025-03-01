CREATE DATABASE PublisherDB;
GO

USE PublisherDB;
GO

CREATE TABLE BaseProjects (
    Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    ProjectName NVARCHAR(255) NOT NULL,
    StartDate DATE NOT NULL,
    Status NVARCHAR(255) NOT NULL);

INSERT INTO BaseProjects (Id, ProjectName, StartDate, Status)
VALUES ('a79609df-cddd-4dd5-92b3-34ef0751b483', 'Project Alpha 3', '2025-01-01', 'InProgress'),
       ('a79609df-cddd-4dd5-92b3-34ef0751b484', 'Project Beta', '2025-02-01', 'Started');