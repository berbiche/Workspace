--Nicolas Berbiche
--Lab08-D63

USE MASTER
GO

--DROP DATABASE BANQUE

--Q01
CREATE DATABASE BANQUE
GO

USE BANQUE
GO

--Q02
CREATE TABLE Client
(numero_client INT PRIMARY KEY,
 nom           VARCHAR(20),
 prenom        VARCHAR(20),
 adresse       VARCHAR(20),
 ville         VARCHAR(20))
GO

--Q03
CREATE TABLE Compte
(numero_compte  INT PRIMARY KEY,
 numero_client  INT,
 date_ouverture DATE DEFAULT GETDATE(),
 solde          MONEY)
GO

--Q04
CREATE TRIGGER TRG_SOLDE
ON Compte FOR UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM inserted WHERE solde < 0)
	BEGIN
		RAISERROR('Impossible d''avoir un solde négatif',16,1)
		ROLLBACK
		RETURN
	END
END
GO

--Q05
INSERT INTO Client VALUES(1,'Bourget','Pierre','23, rue des Plaines','Montréal')
GO

--Q06
INSERT INTO Client VALUES(2,'Marois','Julie','40, rue des Pins','Laval')
GO

--Q07
INSERT INTO Compte VALUES(1000,1,DEFAULT,0)
GO

--Q08
INSERT INTO Compte VALUES(2000,1,DEFAULT,0)
GO

--Q09
CREATE PROC Depot(@numero_compte INT, @montant MONEY)
AS
UPDATE Compte SET solde+=@montant WHERE numero_compte=@numero_compte
GO

--Q10
CREATE PROC Retrait(@numero_compte INT, @montant MONEY)
AS
UPDATE Compte SET solde-=@montant WHERE numero_compte=@numero_compte
GO

--Q11
EXEC Depot 1000, 300
GO

--Q12
EXEC Depot 2000, 200
GO

--Q13
CREATE PROC Transfert(@compteSrc INT, @compteDest INT, @montant MONEY)
AS
BEGIN
	--On peut pas transférer un montant négatif
	IF @montant < 0 RETURN;
	BEGIN TRY
		BEGIN TRANSACTION
			UPDATE Compte SET solde-=@montant WHERE numero_compte=@compteSrc
			UPDATE Compte SET solde+=@montant WHERE numero_compte=@compteDest
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		RAISERROR('Transfert de compte impossible',16,1)
	END CATCH
END
GO

--Q14
SELECT * FROM Compte
GO

EXEC Transfert 1000,2000,200
GO

--Q15
--Vrai

--Q16
--Faux

--Q17
SELECT * FROM Compte
GO

EXEC Transfert 1000,2000,150
GO

--Q18
--FAUX

--Q19
--Faux

--Q20
SELECT * FROM Compte
GO

/*
 * Le trigger rollback la transaction car Pierre aura moins de 0$,
 * ainsi Pierre n'a pas été débité.
 */