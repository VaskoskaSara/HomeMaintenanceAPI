﻿CREATE TABLE NotificationReviews (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Message NVARCHAR(255),
    UserId UNIQUEIDENTIFIER NOT NULL,
	EmployeeId UNIQUEIDENTIFIER NOT NULL,
	PaymentId VARCHAR(MAX) NULL,
    CreatedAt DATETIME,
    Sent BIT DEFAULT 0 
);