Use master
GO

CREATE DATABASE ContactManager
GO

Use ContactManager
GO

CREATE TABLE Contact
(
	Id            integer identity(1,1) primary key,
	Nom           varchar(30) not null,
	Telephone     varchar(20) not null,
	Courriel      varchar(40) not null,
	CodePostal    varchar(20) not null,
	DateNaissance datetime null
)
Go