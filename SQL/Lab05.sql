--LAB5
--Nicolas Berbiche
USE Rona
GO

/*
ALTER TABLE Clients
ALTER COLUMN No_client INTEGER NOT NULL
GO

ALTER TABLE Clients WITH CHECK
ADD PRIMARY KEY (No_client)
GO

ALTER TABLE Commande
ALTER COLUMN No_commande INTEGER NOT NULL
GO

ALTER TABLE Commande
ADD	FOREIGN KEY (No_client) REFERENCES Clients(No_client) ON UPDATE CASCADE ON DELETE CASCADE
	PRIMARY KEY (No_commande)
GO
*/

SELECT * FROM Commande
SELECT * FROM Clients

--Q01
SELECT cl.Ville, SUM(co.Montant) 'Montant total des commandes'
FROM Clients cl INNER JOIN Commande co
ON cl.No_client = co.No_client
GROUP BY cl.Ville

--Q02
SELECT cl.Ville, MAX(co.Montant) 'Montant maximum'
FROM Clients cl INNER JOIN Commande co
ON cl.No_client = co.No_client
GROUP BY cl.Ville

--Q03
SELECT cl.Ville, MIN(co.Montant) 'Montant minimum'
FROM Clients cl INNER JOIN Commande co
ON cl.No_client = co.No_client
GROUP BY cl.Ville

--Q04
SELECT cl.Ville, AVG(co.Montant) 'Montant moyen'
FROM Clients cl INNER JOIN Commande co
ON cl.No_client = co.No_client
GROUP BY cl.Ville

--Q05
SELECT cl.Nom
FROM Clients cl
WHERE 

--Q06
SELECT *
FROM Clients
WHERE No_Client NOT IN (SELECT No_client FROM Commande)

--Q07
SELECT COUNT(co.No_commande)
FROM Commande co, Clients cl
WHERE co.No_client IN (SELECT cl.No_Client FROM Clients WHERE cl.Ville = 'Laval') 

--Q08
SELECT *
FROM Commande
WHERE No_client NOT IN (SELECT No_Client FROM Clients)

--Q09
SELECT cl.Nom 'Client.nom', co.no_commande 'Commande.no_commande'
FROM Clients cl FULL OUTER JOIN Commande co
ON 
