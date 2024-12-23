CREATE OR ALTER PROCEDURE InsertEmployeeDisabledDates
    @Id UNIQUEIDENTIFIER,
    @DisabledDates VARCHAR(MAX)
AS  
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.UserAvailability WHERE UserId = @Id)
    BEGIN
        UPDATE UserAvailability
        SET DisabledDates = @DisabledDates
        WHERE UserId = @Id;
    END

    ELSE
    
    BEGIN
        INSERT INTO dbo.UserAvailability (UserId, DisabledDates)
        VALUES (@Id, @DisabledDates);
    END;
END; 
GO;