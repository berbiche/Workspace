--Lab03
--Nicolas Berbiche

--Q01
USE Rona
GO

--Q02
SELECT *
FROM Clients
WHERE No_client NOT IN (SELECT No_client FROM Commande)


--Q03
SELECT No_client, Ville,
			 No_commande = (SELECT top 1 no_commande FROM Commande WHERE No_client = 10), 
			 Montant = (SELECT top 1 Montant FROM Commande WHERE No_client = 10)
FROM Clients
WHERE No_Client = 20


--Q04
SELECT No_client, Nom
FROM Clients
WHERE No_client IN (SELECT No_client FROM Commande)

--Q05.1
SELECT No_client, Nom,
			 No_commande = (SELECT top 1 No_commande FROM Commande WHERE No_client = 20), 
			 Montant = (SELECT top 1 Montant FROM Commande WHERE No_client = 20 )
FROM Clients
WHERE No_Client = 20

union 

SELECT No_client, Nom, 
			 No_commande = (SELECT top 1 No_commande FROM Commande WHERE No_client = 30), 
			 Montant = (SELECT top 1 Montant FROM Commande WHERE No_client = 30)
FROM Clients
WHERE No_Client = 30

--Q05.2
SELECT T1.No_client, Nom, No_commande, Montant
FROM Clients 'T1', Commande 'T2'
WHERE T1.No_Client = 30 AND T2.No_client = 30 OR
			T1.No_Client = 20 AND T2.No_client = 20


--Q06.1
SELECT DISTINCT Nom, No_commande, Ville, Montant
FROM Clients 'T1', Commande 'T2'
WHERE T1.Ville = 'Laval' AND
			T2.No_client = (SELECT T1.No_Client WHERE T1.Ville = 'Laval') OR
			T1.Ville = 'Montréal' AND
			T2.No_client = (SELECT T1.No_Client WHERE T1.Ville = 'Montréal')


--Q06.2
SELECT DISTINCT Nom, No_commande, Ville, Montant
FROM Clients 'T1', Commande 'T2'
WHERE T1.Ville = 'Montréal' AND
			T2.No_client = (SELECT T1.No_Client WHERE T2.Ville = 'Montréal')
			
UNION

SELECT DISTINCT Nom, No_commande, Ville, Montant
FROM Clients 'T1', Commande 'T2'
WHERE T1.Ville = 'Laval' AND
			T2.No_client = (SELECT T1.No_Client WHERE T2.Ville = 'Laval')

--Q07.1
SELECT DISTINCT Nom, No_commande, Ville, Montant
FROM Clients 'T1', Commande 'T2'
WHERE T1.Ville != 'Brossard' AND
			T2.No_client = (SELECT T1.No_Client WHERE T1.Ville != 'Brossard')

--Q07.2
SELECT DISTINCT Nom, No_commande, Ville, Montant
FROM Clients 'T1', Commande 'T2'
WHERE T1.Ville <> 'Brossard' AND
			T2.No_client = (SELECT T1.No_Client WHERE T1.Ville <> 'Brossard')

--Q08.1
SELECT * 
FROM Commande
WHERE Montant >= 1000 AND Montant <= 9000

--Q08.2
SELECT * 
FROM Commande
WHERE Montant >= 1000

INTERSECT

SELECT *
FROM Commande
WHERE Montant <= 9000

--Q09.1
SELECT * 
FROM Commande
WHERE Montant < 1000 OR Montant > 9000

--Q09.2
SELECT *
FROM Commande
WHERE Montant > 9000 OR
			IN (SELECT Montant WHERE Montant < 1000)