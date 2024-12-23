CREATE TABLE UserAvaliability (
    Id INT PRIMARY KEY IDENTITY(1,1), 
    UserId UNIQUEIDENTIFIER NOT NULL,               
    DisabledDates VARCHAR(MAX),
    
   CONSTRAINT FK_UserAvaliability_UserId FOREIGN KEY (UserId) REFERENCES dbo.Users(Id)
);