USE MASTER
GO

CREATE DATABASE [Toys4Us]
GO

USE [Toys4Us]
GO

CREATE TABLE [aspnet_User]
(
	Id         UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	[Name]     VARCHAR(40),
	Email      VARCHAR(255) UNIQUE NOT NULL,
	[Password] NCHAR(32) NOT NULL,
)
GO

/*
CREATE TABLE [toys4us_Age_Categories]
(
	Id        INTEGER IDENTITY(1,1) PRIMARY KEY,
	Name      NVARCHAR(80),
	Age_Start INTEGER NOT NULL,
	Age_End   INTEGER NOT NULL,
	CONSTRAINT UQ_toys4usAgeCategories_Age UNIQUE(Age_Start, Age_End),
	CONSTRAINT CK_toys4usAgeCategories_AgeStart
		CHECK(Age_Start >= 0 AND Age_Start < Age_End),
	CONSTRAINT CK_toys4usAgeCategories_AgeEnd
		CHECK(Age_End =< 18)
)
GO

CREATE TABLE [toys4us_Categories]
(
	Id          INTEGER IDENTITY(1,1) PRIMARY KEY,
	Name        NVARCHAR(80) NOT NULL
)
GO
*/

CREATE TABLE [toys4us_Brands]
(
	Id          INTEGER IDENTITY(1,1) PRIMARY KEY,
	Name        NVARCHAR(100) NOT NULL,
	Description NVARCHAR(2000),
	Website     NVARCHAR(1000),
	Address     NVARCHAR(400),
	Phone       VARCHAR(17)
)
GO

CREATE TABLE [toys4us_Toys]
(
	Id           INTEGER IDENTITY(1000,1) PRIMARY KEY,
	Name         NVARCHAR(200) NOT NULL,
	Description  NVARCHAR(400),
	Date_Added   DATE DEFAULT GETDATE() NOT NULL,
	Gender       CHAR(1) NOT NULL,
	Brand        INTEGER NULL,
--Age_Id       INTEGER REFERENCES [toys4us_Age_Categories](Id),
--Category_Id  INTEGER REFERENCES [toys4us_Categories](Id),
	Price        SMALLMONEY CHECK (Price >= 0),
	CONSTRAINT CK_toys4us_Gender CHECK(Gender IN ('M','F','O'))
)
GO

ALTER TABLE [toys4us_Toys]
	ADD FOREIGN KEY (Brand) REFERENCES [toys4us_Brands](Id) ON UPDATE CASCADE ON DELETE SET NULL
GO