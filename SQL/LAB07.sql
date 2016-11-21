--LAB07
--Nicolas Berbiche

USE [AIR-TRANSAT];
GO

--Q01
CREATE TABLE MAINTENANCE
(NO_AVION  INTEGER,
 [DATE]    DATE,
 ENTRETIEN VARCHAR(50));
GO

--Q02
ALTER TABLE MAINTENANCE
	ALTER COLUMN NO_AVION INTEGER NOT NULL
GO

ALTER TABLE MAINTENANCE
	ALTER COLUMN [DATE] DATE NOT NULL;
GO

ALTER TABLE MAINTENANCE
	ADD PRIMARY KEY (NO_AVION,[DATE]);
GO

--Q03
DECLARE @COMPTEUR INT = 1;
DECLARE @N1       INTEGER;
DECLARE @S1       VARCHAR(50) = 'Maintenance du moteur';
DECLARE @S2	      VARCHAR(50) = 'Maintenance du train d''atterrissage';
DECLARE @S3       VARCHAR(50);
DECLARE @DATE     DATE = GETDATE();

WHILE (@COMPTEUR <= 10)
BEGIN
	SET @N1 = (SELECT TOP 1 NO_AVION FROM AVION ORDER BY NEWID());
	IF RAND() > 0.5
		SET @S3 = @S2;
	ELSE
		SET @S3 = @S1;
	INSERT INTO MAINTENANCE VALUES (@N1, DATEADD(DD,@COMPTEUR,@DATE),@S3);
	SET @COMPTEUR = @COMPTEUR + 1;
END

--Q04
SELECT v.NO_VOL, v.V_DEPART
FROM VOL v JOIN PILOTE p
ON (p.NO_PILOTE = 2 or p.NO_PILOTE = 4) AND v.NO_PILOTE = p.NO_PILOTE

--Q05
SELECT v.NO_VOL
FROM VOL v JOIN AVION a
ON v.NO_AVION = a.NO_AVION AND a.AEROPORT != 'Granby'

--Q06
SELECT p.NO_PILOTE, p.NOM_PILOTE
FROM PILOTE p JOIN VOl v
ON p.NO_PILOTE = v.NO_PILOTE JOIN AVION a
ON a.NO_AVION = v.NO_AVION AND a.CAPACITE > 300

--Q07
SELECT p.NOM_PILOTE
FROM PILOTE p JOIN VOL v
ON p.VILLE = 'Sherbrooke' AND v.V_DEPART = 'Granby' AND p.NO_PILOTE = v.NO_PILOTE JOIN AVION a
ON a.MODELE = 'A350' AND a.NO_AVION = v.NO_AVION

--Q08
SELECT v.NO_VOL
FROM VOL v JOIN PILOTE p
ON p.VILLE = 'Sherbrooke' AND p.NO_PILOTE = v.NO_PILOTE AND (v.V_DEPART = 'Granby' OR v.V_DESTINATION = 'Granby') JOIN AVION a
ON a.AEROPORT = 'Sherbrooke' AND v.NO_AVION = a.NO_AVION

--Q09
SELECT p.NO_PILOTE, p.NOM_PILOTE
FROM PILOTE p
WHERE p.NO_PILOTE != 7 AND p.VILLE = (SELECT VILLE FROM PILOTE WHERE NO_PILOTE = 7)

--Q10
SELECT DISTINCT p.NO_PILOTE
FROM PILOTE p JOIN VOL v
ON p.NO_PILOTE != 7 AND p.NO_PILOTE = v.NO_PILOTE

--Q11
SELECT a.NO_AVION
FROM AVION a
WHERE a.AEROPORT = (SELECT AEROPORT FROM AVION WHERE NO_AVION = 104)

--Q12
SELECT p.NO_PILOTE, p.NOM_PILOTE
FROM PILOTE p
WHERE p.VILLE = (SELECT VILLE FROM PILOTE WHERE NO_PILOTE = 7)
AND p.SALAIRE > (SELECT SALAIRE FROM PILOTE WHERE NO_PILOTE = 7)

--Q13
SELECT p.NO_PILOTE, p.NOM_PILOTE
FROM PILOTE p JOIN VOL v
ON p.NO_PILOTE = v.NO_PILOTE AND v.V_DEPART = p.VILLE

--Q14
--Donn�es du PDF erron�es
SELECT p.NOM_PILOTE, COUNT(p.NOM_PILOTE) 'Compteur'
FROM PILOTE p JOIN VOL v
ON p.NO_PILOTE = v.NO_PILOTE
GROUP BY p.NOM_PILOTE

--Q15
SELECT a.NO_AVION, COUNT(a.NO_AVION) 'Nombre de maintenances'
FROM AVION a JOIN MAINTENANCE m
ON a.NO_AVION = m.NO_AVION
GROUP BY a.NO_AVION

--Q16
SELECT a.NO_AVION, m.ENTRETIEN
FROM AVION a JOIN MAINTENANCE m
ON a.NO_AVION = m.NO_AVION

--Q17
SELECT a.NO_AVION, m.ENTRETIEN
FROM AVION a LEFT JOIN MAINTENANCE m
ON a.NO_AVION = m.NO_AVION

--Q18
--Cl� primaire sur NO_AVION donc ne peut pas �tre nul
SELECT a.NO_AVION, m.ENTRETIEN
FROM AVION a RIGHT JOIN MAINTENANCE m
ON a.NO_AVION = m.NO_AVION

--Q19
SELECT a.NO_AVION, m.ENTRETIEN
FROM AVION a FULL JOIN MAINTENANCE m
ON a.NO_AVION = m.NO_AVION

--Q20
SELECT a.NO_AVION, m.ENTRETIEN
FROM AVION a CROSS JOIN MAINTENANCE m