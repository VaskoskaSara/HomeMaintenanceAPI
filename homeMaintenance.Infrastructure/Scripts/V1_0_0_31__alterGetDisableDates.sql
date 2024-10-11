BEGIN TRANSACTION;
GO

-- EXEC sp_rename 'UserAvailability', 'UserAvaliability';
-- GO;

ALTER PROCEDURE GetDisabledDatesByEmployeeId
    @Id UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

    SELECT DisabledDates FROM dbo.UserAvaliability WHERE UserId = @Id
END
GO;

ALTER PROCEDURE InsertEmployeeDisabledDates
    @Id UNIQUEIDENTIFIER,
    @DisabledDates VARCHAR(MAX)
AS  
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.UserAvaliability WHERE UserId = @Id)
    BEGIN
        UPDATE dbo.UserAvaliability
        SET DisabledDates = @DisabledDates
        WHERE UserId = @Id;
    END
    ELSE
    BEGIN
        INSERT INTO dbo.UserAvaliability (UserId, DisabledDates)
        VALUES (@Id, @DisabledDates);
    END
END;
GO;

COMMIT;
GO;