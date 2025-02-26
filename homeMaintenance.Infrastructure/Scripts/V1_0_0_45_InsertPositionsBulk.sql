BEGIN TRANSACTION;
GO

CREATE OR ALTER PROCEDURE InsertPositionsBulk
@PositionNames dbo.PositionNameTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Positions (Id, PositionName)
    SELECT NEWID(), PositionName
    FROM @PositionNames;
END; GO;
GO


COMMIT;
GO;
