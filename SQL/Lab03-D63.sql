--LAB03
--Nicolas Berbiche

--Ctrl + k, Ctrl + u
--Ctrl + k, Ctrl + c
--USE MASTER
--DROP DATABASE Lab3
--SELECT * FROM Employes
--DELETE Employes
--DROP PROC PeuplerEMP
--DROP PROC AjouterEMP
--DROP PROC RechercherEMP
--DROP PROC ModifierEMP
--DROP PROC SupprimerEMP

--Q01
CREATE DATABASE [Lab3]
GO

USE [Lab3]
GO

--Q02
CREATE TABLE Employes
(
	no_emp         INT PRIMARY KEY,
	prenom_emp     VARCHAR(20) NOT NULL,
	nom_emp        VARCHAR(20) NOT NULL,
	fonction       VARCHAR(20),
	salaire        MONEY,
	date_embauche  DATE,
	date_naissance DATE,
	pays           VARCHAR(20)
)
GO

--Q03
CREATE PROC PeuplerEMP
  @x INT = 20,
  @y INT = 1000
AS
BEGIN
IF (@x < 1 OR @y < 1)
BEGIN
  RAISERROR('X et Y doivent être supérieur à 0',@x,@y)
  RETURN
END
WHILE (@x > 0)
BEGIN
  DECLARE @date_em DATE = DATEADD(DAY,-@x,GETDATE()),
          @date_na DATE = DATEADD(DAY,-(2*@x),GETDATE())
	INSERT INTO Employes(no_emp,prenom_emp,nom_emp,fonction,salaire,date_embauche,date_naissance,pays)
    VALUES(@y,CONCAT('prenom_',@y),CONCAT('nom_',@y),CONCAT('fonction_',@y),10*@y,@date_em,@date_na,CONCAT('pays_',@x))
  SET @x -= 1
  SET @y += 1
END
END
GO

--Q04
--x = 100 et y = 5
EXEC PeuplerEMP 100,5
--x = 200 et y = 1000
EXEC PeuplerEMP 200
--x = 20 et y = 30
EXEC PeuplerEMP @y=30
--x = DEFAULT et y = DEFAULT
EXEC PeuplerEMP
GO

--Q05
EXEC sp_helptext PeuplerEMP
GO

--Q06
EXEC sp_recompile PeuplerEMP
GO

--Q07
DROP PROC PeuplerEMP
GO

--Q08
CREATE PROC RechercherEMP
  @id  INT = NULL,
  @nom VARCHAR(20) = NULL
AS
BEGIN
IF (@id IS NULL AND @nom IS NULL)
BEGIN
  RAISERROR('Au moins un paramètre doit être non nulle',16,1)
  RETURN
END
SELECT * FROM Employes WHERE no_emp = @id OR nom_emp = @nom
END
GO

--Q09
CREATE PROC AjouterEMP
  @id       INT,
  @prenom   VARCHAR(20),
  @nom      VARCHAR(20),
	@fonction VARCHAR(20) = NULL,
	@salaire  MONEY = NULL,
	@date_em  DATE = NULL,
	@date_na  DATE = NULL,
	@pays     VARCHAR(20) = NULL
AS
BEGIN
INSERT INTO Employes VALUES(@id,@prenom,@nom,@fonction,@salaire,@date_em,@date_na,@pays)
END
GO

--Q10
CREATE PROC ModifierEMP
  @id       INT,
  @fonction VARCHAR(20) = NULL,
  @salaire  MONEY = NULL,
  @pays     VARCHAR(20) = NULL
AS
BEGIN
  IF (@salaire NOT BETWEEN 20000 AND 120000)
  BEGIN
    RAISERROR('Le salaire doit être entre %i et %i',16,1,20000,120000)
    RETURN
  END
  UPDATE Employes
  SET fonction=@fonction,salaire=@salaire,pays=@pays
  WHERE no_emp = @id
END
GO

--Q11
CREATE PROC SupprimerEMP
  @id INT
AS DELETE FROM Employes WHERE no_emp = @id
GO