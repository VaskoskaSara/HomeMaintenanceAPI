BEGIN TRANSACTION
GO

CREATE TABLE dbo.Positions
	(
	Id UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
	PositionName nvarchar(100) NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Positions ADD CONSTRAINT
	PK_Positions PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE TABLE dbo.UserRoles
	(
	Id uniqueidentifier NOT NULL,
	RoleName nvarchar(50) NOT NULL,
	IsDeleted bigint NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.UserRoles ADD CONSTRAINT
	PK_UserRoles PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


CREATE TABLE dbo.Users
	(
	Id uniqueidentifier NOT NULL,
	FullName nvarchar(100) NOT NULL,
	PhoneNumber bigint NULL,
	Email nvarchar(100) NOT NULL,
	Password nvarchar(30) NOT NULL,
	City nvarchar(50) NOT NULL,
	UserRole int NOT NULL,
	IsDeleted bigint NOT NULL,
	CreatedAt datetime NULL,
	Experience int NULL,
	Price bigint NULL,
	Image nvarchar(4000) NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Users ADD CONSTRAINT
	PK_Users PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE dbo.Users ADD CONSTRAINT
	FK_Users_UserRoles FOREIGN KEY
	(
	Id
	) REFERENCES dbo.UserRoles
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

CREATE TABLE dbo.PositionUsers
	(
	Id uniqueidentifier NOT NULL,
	PositionId uniqueidentifier NOT NULL,
	UserId uniqueidentifier NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.PositionUsers ADD CONSTRAINT
	PK_PositionUsers PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE dbo.PositionUsers ADD CONSTRAINT
	FK_PositionUsers_Positions FOREIGN KEY
	(
	PositionId
	) REFERENCES dbo.Positions
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

ALTER TABLE dbo.PositionUsers ADD CONSTRAINT
	FK_PositionUsers_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

CREATE TABLE dbo.Reviews
	(
	Id uniqueidentifier DEFAULT NEWID() NOT NULL,
	Comment nvarchar(4000) NULL,
	EmployeeId uniqueidentifier NOT NULL,
	PaymentId varchar(max),
	UserId uniqueidentifier NOT NULL,
	Rating decimal(18, 0) NOT NULL,
	CreatedAt datetime NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.Reviews ADD CONSTRAINT
	PK_Reviews PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE dbo.Reviews ADD CONSTRAINT
	FK_Reviews_Users FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.Users
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

CREATE TABLE dbo.ReviewImages
	(
	Id uniqueidentifier NOT NULL,
	Image nvarchar(4000) NOT NULL,
	ReviewId uniqueidentifier NOT NULL
	)  ON [PRIMARY]

ALTER TABLE dbo.ReviewImages ADD CONSTRAINT
	PK_ReviewImages PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE dbo.ReviewImages ADD CONSTRAINT
	FK_ReviewImages_Reviews FOREIGN KEY
	(
	ReviewId
	) REFERENCES dbo.Reviews
	ON UPDATE  NO ACTION 
	ON DELETE  NO ACTION 
	
COMMIT
GO
