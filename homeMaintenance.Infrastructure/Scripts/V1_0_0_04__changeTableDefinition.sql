BEGIN TRANSACTION;
GO

DROP TABLE dbo.PositionUsers;

ALTER TABLE dbo.Users
DROP CONSTRAINT FK_Users_UserRoles;

ALTER TABLE dbo.UserRoles 
DROP CONSTRAINT PK_UserRoles;

ALTER TABLE dbo.UserRoles 
DROP COLUMN Id;

ALTER TABLE dbo.UserRoles 
ADD Id INT NOT NULL;

ALTER TABLE dbo.UserRoles 
ADD CONSTRAINT PK_UserRoles PRIMARY KEY (Id);

ALTER TABLE dbo.Users
ADD CONSTRAINT FK_Users_UserRoles FOREIGN KEY (UserRole) REFERENCES dbo.UserRoles(Id);

ALTER TABLE dbo.Users
ADD CONSTRAINT DF_Id DEFAULT NEWID() for Id;

COMMIT;
GO;