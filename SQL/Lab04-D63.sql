--Nicolas Berbiche
--Lab04

USE SkyNet
GO

--Q01
CREATE FUNCTION ListeCommandes(@c_num INT)
RETURNS TABLE
AS
RETURN (SELECT * FROM Commandes WHERE no_client=@c_num)
GO

--Q02
SELECT *
FROM ListeCommandes(40)
GO

--Q03
CREATE FUNCTION NbreCommandes(@c_num INT)
RETURNS INT
AS
BEGIN
  RETURN (SELECT COUNT(*) FROM Commandes WHERE no_client=@c_num)
END
GO

--Q04
SELECT dbo.NbreCommandes(40)
GO

--Q05
CREATE FUNCTION MontantTotalCommandes(@c_num INT)
RETURNS INT
AS
BEGIN
  RETURN
  (SELECT SUM(Dco.prix_unitaire*Dco.quantite)
   FROM Commandes Co
    JOIN Details_commandes Dco
      ON Co.no_commande=Dco.no_commande
   WHERE no_client=@c_num)
END
GO

--Q06
SELECT dbo.MontantTotalCommandes(40)
GO

--Q07
CREATE FUNCTION MontantMoyenCommandes(@c_num INT)
RETURNS INT
AS
BEGIN
  RETURN
  (SELECT AVG(Dco.prix_unitaire*Dco.quantite)
   FROM Commandes Co
    JOIN Details_commandes Dco
      ON Co.no_commande=Dco.no_commande
   WHERE no_client=@c_num)
END
GO

--Q08
SELECT dbo.MontantMoyenCommandes(40)
GO

--Q09
sp_helptext 'dbo.NbreCommandes'
GO

--Q10
CREATE PROC ExecNbreCommandes
  @c_num INT,
  @nbCommandes INT OUTPUT
AS
BEGIN
  SET @NbCommandes = dbo.NbreCommandes(@c_num)
END
GO

--Q11
DECLARE @nbCommandes INT
EXEC ExecNbreCommandes
  40,
  @nbCommandes OUTPUT
GO

--Q12
ALTER PROC ExecNbreCommandes
  @c_num INT = 40,
  @nbCommandes INT OUTPUT
AS
BEGIN
  SET @nbCommandes = dbo.NbreCommandes(@c_num)
END
GO

--Q13
DECLARE @nbCommandes INT
EXEC ExecNbreCommandes 
  DEFAULT,
  @nbCommandes OUTPUT
GO

--Q14
--Uniquement possible pour une procédure?
sp_depends 'dbo.NbreCommandes'
GO

--Q15
DROP FUNCTION dbo.NbreCommandes
GO

--Q16
sp_recompile ExecNbreCommandes
GO

--Q17
DROP PROC ExecNbreCommandes
GO

--Q18
--algorithme aucunement optimisé
CREATE PROC PREMIER
AS
BEGIN
  DECLARE @i INT = 2, @nombre INT = 100
  DECLARE @Primes TABLE(nombre INT PRIMARY KEY, prime BIT)
  WHILE (@i < @nombre)
  BEGIN
    INSERT INTO @Primes VALUES (@i, 1)
    SET @i += 1
  END
  DECLARE @j INT = 2
  SET @i = 2 + @j
  WHILE (@i < @nombre)
  BEGIN
    UPDATE @Primes SET prime = 0 WHERE nombre = @i
    SET @i += @j
  END
END

--Q19


--Q20