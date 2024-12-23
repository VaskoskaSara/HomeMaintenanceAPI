BEGIN TRANSACTION;
GO

ALTER TABLE dbo.Users 
ADD PaymentType int NULL
GO;

CREATE TABLE dbo.PaymentTypes
	(
	Id int NOT NULL,
	PaymentType nvarchar(50) NOT NULL,
	IsDeleted bigint DEFAULT 0 NOT NULL 
	)  ON [PRIMARY]
GO;

ALTER TABLE dbo.PaymentTypes ADD CONSTRAINT
	PK_PaymentTypes PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO;
	
ALTER TABLE dbo.Users
ADD CONSTRAINT FK_Users_PaymentTypes FOREIGN KEY (PaymentType) REFERENCES dbo.PaymentTypes(Id);
GO;

INSERT INTO dbo.PaymentTypes (Id, PaymentType) VALUES (1, 'Hourly'), (2, 'Overall'), (3, 'excludeByContract');

COMMIT;
GO