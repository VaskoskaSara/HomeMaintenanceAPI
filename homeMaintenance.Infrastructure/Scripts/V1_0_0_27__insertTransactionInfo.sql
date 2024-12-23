BEGIN TRANSACTION;
GO

CREATE OR ALTER PROCEDURE InsertTransaction 
@UserId                     UNIQUEIDENTIFIER, 
@EmployeeId                 UNIQUEIDENTIFIER, 
@PaymentId                  VARCHAR(MAX), 
@Amount                     BIGINT,
@StartDateTime              DATETIME, 
@EndDateTime                DATETIME
AS 
BEGIN 
SET NOCOUNT ON 

INSERT INTO dbo.UserPayment (UserId, EmployeeId, PaymentId, Amount, StartDateTime, EndDateTime) VALUES 
(@UserId, @EmployeeId, @PaymentId, @Amount, @StartDateTime, @EndDateTime);

END; GO;
GO;

COMMIT;
GO;