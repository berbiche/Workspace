--LABO08
--NICOLAS BERBICHE
USE [AIR-TRANSAT]
GO

/*
DROP VIEW [AvionManiwaki]
DROP VIEW [AvionGranby]
DROP VIEW [VolDetails]
GO
*/

--Q01
CREATE VIEW [AvionManiwaki]
AS
SELECT *
FROM [AVION]
WHERE AVION.AEROPORT = 'Maniwaki'
GO

--Q02
SELECT * FROM [AvionManiwaki]
GO

--Q03
CREATE VIEW [AvionGranby]
AS
SELECT *
FROM [AVION]
WHERE AVION.AEROPORT = 'Granby'
WITH CHECK OPTION
GO

--Q04
SELECT * FROM [AvionGranby]
GO

--Q05
UPDATE [AvionManiwaki]
SET AEROPORT = 'Granby'
WHERE NO_AVION = 101
GO

--Q06
---- OUI
---- La mise à jour a fonctionné car la contrainte CHECK OPTION n'était pas sur la vue

--Q07
UPDATE [AVION]
SET AEROPORT = 'Maniwaki'
WHERE NO_AVION = 102
GO

--Q08
---- OUI
---- La mise à jour a fonctionné car l'UPDATE ne se faisait pas au travers la vue, mais directement sur la table

--Q09
INSERT INTO [AvionManiwaki] (CAPACITE,NO_AVION,MODELE,AEROPORT)
VALUES (700,120,'CRJ','Maniwaki')
GO

--Q10
---- OUI
---- La mise à jour a fonctionné car la contrainte CHECK OPTION n'était pas sur la vue

--Q11
INSERT INTO [AvionManiwaki] (CAPACITE,NO_AVION,MODELE,AEROPORT)
VALUES (800,130,'CRJ','Granby')
GO

--Q12
---- OUI
---- La mise à jour a fonctionné car la contrainte CHECK OPTION n'était pas sur la vue
---- ce qui permet d'insérer des valeurs qui ne respectent pas la clause WHERE

--Q13
INSERT INTO [AvionGranby] (CAPACITE,NO_AVION,MODELE,AEROPORT)
VALUES (900,140,'DASH','Montréal')
GO

--Q14
---- NON
---- La mise à jour n'a pas fonctionné car la contrainte CHECK OPTION empêche l'insertion
---- de valeur qui ne respecte pas la clause du WHERE

--Q15
DELETE [AvionManiwaki]
WHERE NO_AVION = 108
GO

--Q16
---- OUI
---- La vue n'est pas une jointure de plusieurs tables, par conséquent la commande fonctionnera

--Q17
CREATE VIEW [VolDetails] (NUMERO,MODELE,PILOTE,SALAIRE)
AS
SELECT v.NO_VOL, a.MODELE, p.NOM_PILOTE, p.SALAIRE
FROM [VOL] v 
	INNER JOIN [PILOTE] p
		ON v.NO_PILOTE = p.NO_PILOTE 
	INNER JOIN [AVION] a
		ON v.NO_AVION = a.NO_AVION
GO

--Q18
INSERT INTO [VolDetails]
VALUES (2000,'A330','Picher')
GO

--Q19
---- NON
---- L'ajout implique la modification de plusieurs tables et il est uniquement possible de modifier une table à la fois

--Q20
UPDATE [VolDetails]
SET SALAIRE = SALAIRE * 1.10
GO

--Q21
---- OUI
---- Une seule table est modifiée par l'opération