--Nicolas Berbiche
--Lab07-D63

USE MASTER
GO

--Q00
--DROP DATABASE Lab
--GO

--Q01
CREATE DATABASE Lab
GO

--Q02
USE Lab
GO

--Q03
CREATE TABLE Commande
(no_commande INT NOT NULL,
 no_client   INT,
 [date]      DATETIME,
 montant     MONEY)
GO

ALTER TABLE Commande
  ADD CONSTRAINT PK_NoCommande PRIMARY KEY(no_commande)
GO

--Q04
CREATE TABLE JournalCommande
(no_commande INT,
 no_client   INT,
 date_cmd    DATETIME,
 montant     MONEY,
 date_trs    DATETIME,
 code_trs    VARCHAR(6),
 code_usr    VARCHAR(64))
GO

ALTER TABLE JournalCommande
  ADD CONSTRAINT CK_Code_Transaction
    CHECK (code_trs IN('INSERT','UPDATE','DELETE'))
GO

--Q05
CREATE TRIGGER INS_JOURNAL_CMD
ON Commande
AFTER INSERT
AS
BEGIN
  SET NOCOUNT ON;
  INSERT INTO JournalCommande
  SELECT *, GETDATE(), 'INSERT', SUSER_ID()
  FROM inserted
END
GO

--Q06
CREATE TRIGGER UPD_JOURNAL_CMD
ON Commande
AFTER UPDATE
AS
BEGIN
  SET NOCOUNT ON;
  INSERT INTO JournalCommande
  SELECT *, GETDATE(), 'UPDATE', SUSER_ID()
  FROM inserted
END
GO

--Q07
CREATE TRIGGER DEL_JOURNAL_CMD
ON Commande
AFTER DELETE
AS
BEGIN
  SET NOCOUNT ON;
  INSERT INTO JournalCommande
  SELECT *, GETDATE(), 'DELETE', SUSER_ID()
  FROM deleted
END
GO

--Q08
INSERT INTO Commande VALUES
  (1000,10,'23-JAN-2017',120.40),
  (1001,20,'12-MAR-2017',1200),
  (1002,10,'18-APR-2017',89.43),
  (1003,30,'28-APR-2017',4300)
GO

--Q09
--Trigger INS_JOURNAL_CMD

--Q10
SELECT * FROM JournalCommande
GO

--Q11
UPDATE Commande
SET montant = 100
WHERE no_commande = 1002
GO

--Q12
--Trigger UPD_JOURNAL_CMD

--Q13
SELECT * FROM JournalCommande
GO

--Q14
DELETE FROM Commande WHERE no_commande = 1001
GO

--Q15
--Trigger DEL_JOURNAL_CMD

--Q16
SELECT * FROM JournalCommande
GO

--Q17
DROP TRIGGER INS_JOURNAL_CMD
DROP TRIGGER UPD_JOURNAL_CMD
DROP TRIGGER DEL_JOURNAL_CMD
GO

--Q18
CREATE TRIGGER JOURNAL_CMD
ON Commande
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
  IF EXISTS(SELECT TOP 1 * FROM inserted) AND EXISTS(SELECT TOP 1 * FROM deleted)
  BEGIN
    SET NOCOUNT ON;
    INSERT INTO JournalCommande
    SELECT *, GETDATE(), 'UPDATE', SUSER_ID()
    FROM inserted
  END
  ELSE
  IF EXISTS(SELECT TOP 1 * FROM deleted)
  BEGIN
    SET NOCOUNT ON;
    INSERT INTO JournalCommande
    SELECT *, GETDATE(), 'DELETE', SUSER_ID()
    FROM deleted
  END
  ELSE
  BEGIN
    SET NOCOUNT ON;
    INSERT INTO JournalCommande
    SELECT *, GETDATE(), 'INSERT', SUSER_ID()
    FROM inserted
  END
END
GO

--Q19
INSERT INTO Commande VALUES(1004,20,'24-APR-2017',100)
GO

UPDATE Commande
SET montant = 200
WHERE no_commande = 1004
GO

DELETE FROM Commande WHERE no_commande = 1003
GO

SELECT * FROM JournalCommande
GO

--Q20
CREATE TRIGGER AVANT_INS_CMD
ON Commande
INSTEAD OF INSERT
AS
BEGIN
  SET NOCOUNT ON;
  IF EXISTS(SELECT * FROM inserted WHERE [date] > GETDATE())
  BEGIN
		RAISERROR('La date de commande est invalide', 16, 1);
    RETURN;
  END
  INSERT INTO Commande
  SELECT * FROM inserted
END
GO

--DROP TRIGGER AVANT_INS_CMD

--Q21
--Ne devrait pas ins�rer dans la table
INSERT INTO Commande VALUES
  (1005,40,'10-APR-2017',100),
  (1006,40,'29-APR-2017',100)
GO

--Devrait ins�rer dans la table
INSERT INTO Commande VALUES
  (1007,40,'10-APR-2017',100),
  (1008,20,'24-APR-2017',100)
GO

SELECT * FROM Commande
GO