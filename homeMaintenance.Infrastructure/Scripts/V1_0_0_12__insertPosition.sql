BEGIN TRANSACTION;
GO

CREATE OR ALTER PROCEDURE InsertPosition 
@PositionName		  NVARCHAR(100),
@NewId UNIQUEIDENTIFIER OUTPUT
AS 
BEGIN 
SET NOCOUNT ON 

INSERT INTO dbo.Positions(Id, PositionName) 
VALUES 
(NEWID(), @PositionName);


SELECT @NewId = Id FROM dbo.Positions WHERE PositionName = @PositionName;

END; GO;
GO;

COMMIT;
GO;