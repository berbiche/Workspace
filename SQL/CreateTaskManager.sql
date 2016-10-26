Use Master
GO

CREATE Database TaskManager
Go

Use TaskManager
GO

Create Table Task
(
	Id integer Identity(1,1) Primary Key,
	[Description] nvarchar(200) Not Null,
	Client nvarchar(20) Not Null,
	[Priority] integer Not Null Check ([Priority] > 0 And [Priority] < 4),
	Creation datetime2 Not Null,
	Due datetime2 Not Null,
	ClosedDate datetime2 Null,
	Done Bit Not Null DEFAULT 0,
	Constraint InvalidDate Check (Due > Creation),
	Constraint IsDone Check (ClosedDate is Null AND Done = 0 or ClosedDate is not Null AND Done = 1)
)
Go

ALTER TABLE Task
	ADD CONSTRAINT DefaultDate
		DEFAULT GETDATE() FOR Creation

--DROP TABLE Task