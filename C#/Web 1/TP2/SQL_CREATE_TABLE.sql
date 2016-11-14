USE MASTER
GO

CREATE DATABASE [Toys4Us]
GO

USE [Toys4Us]
GO

CREATE TABLE [aspnet_User]
(
	Id         UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
	Name       VARCHAR(40),
	Email      VARCHAR(255) UNIQUE NOT NULL,
	[Password] NVARCHAR(32) NOT NULL,
)
GO

CREATE TABLE [toys4us_Age_Categories]
(
	Id        INTEGER IDENTITY(1,1) PRIMARY KEY,
	Name      VARCHAR(80),
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
	Name        VARCHAR(80) NOT NULL
)
GO

CREATE TABLE [toys4us_Brands]
(
	Id          INTEGER IDENTITY(1,1) PRIMARY KEY,
	Name        VARCHAR(100) NOT NULL,
	Description VARCHAR(2000),
	Website     VARCHAR(1000),
	Address     VARCHAR(400),
	Phone       NVARCHAR(17)
)
GO

CREATE TABLE [toys4us_Toys]
(
	Id           INTEGER IDENTITY(1000,1) PRIMARY KEY,
	Name         VARCHAR(200) NOT NULL,
	Description  VARCHAR(400),
	Date_Added   DATE DEFAULT GETDATE() NOT NULL,
	Gender       CHAR(1) NOT NULL,
	Brand        INTEGER REFERENCES [toys4us_Brands](Id)
	Age_Id       INTEGER REFERENCES [toys4us_Age_Categories](Id),
	Category_Id  INTEGER REFERENCES [toys4us_Categories](Id),
	Price        MONEY CHECK (Price >= 0),
	CONSTRAINT CK_toys4us_Gender CHECK(Gender IN ('M','F','O'))
)
GO

















