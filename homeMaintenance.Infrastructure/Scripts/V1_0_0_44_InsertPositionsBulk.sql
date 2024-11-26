BEGIN TRANSACTION;
GO

CREATE TYPE dbo.PositionNameTableType AS TABLE
(
    PositionName NVARCHAR(100)
);
GO

CREATE OR ALTER PROCEDURE InsertPositionsBulk
@PositionNames dbo.PositionNameTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Positions (Id, PositionName)
    SELECT NEWID(), PositionName
    FROM @PositionNames;
END
GO


COMMIT;
GO;
