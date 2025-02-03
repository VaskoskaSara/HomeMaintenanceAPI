﻿CREATE OR ALTER PROCEDURE GetDisabledDatesByEmployeeId
    @Id UNIQUEIDENTIFIER
AS  
BEGIN
    SET NOCOUNT ON;

    SELECT DisabledDates FROM UserAvaliability WHERE UserId = @Id
END; GO;