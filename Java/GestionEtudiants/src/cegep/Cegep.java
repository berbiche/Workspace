package cegep;

import java.util.ArrayList;

public class Cegep {
	
	private ArrayList<Etudiant> listeEtudiant;
	private ArrayList<Professeur> listeProfesseur;
	private ArrayList<Cours> listeCours;
	private ArrayList<Personne> listeBenevoles;

	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
		listeProfesseur = new ArrayList<Professeur>();
		listeCours = new ArrayList<Cours>();
		listeBenevoles = new ArrayList<Personne>();
	}
	
	/**
	 * Ajoute un �tudiant avec un num�ro de dossier;
	 * @param noDossier : Liste de num�ro de dossier (1 ou + dossier)
	 * @return l'instance de l'objet Etudiant cr��e
	 */
	public Etudiant ajoutEtudiant(String noDossier) {
		Etudiant e = new Etudiant(noDossier);
		listeEtudiant.add(e);
		return e;
	}
	
	/**
	 * Rechercher un �tudiant par son num�ro de dossier
	 * @param noDossier : Le num�ro de dossier de l'�tudiant
	 * @return null si aucun �tudiant poss�de ce num�ro de dossier sinon Etudiant
	 */
	public Etudiant getEtudiantByNoDossier(String noDossier) {
		for (Etudiant e: listeEtudiant) {
			if (e.getNumDossier().equals(noDossier))
				return e;
		}
		return null;
	}
	
	/**
	 * Rechercher tous les �tudiants avec un nom de famille donn�
	 * @param nom : Le nom de famille recherch�
	 * @return la liste d'�tudiant avec le nom donn�
	 */
	public ArrayList<Etudiant> getAllEtudiantsByNomFamille(String nom) {
		ArrayList<Etudiant> etudiants = new ArrayList<Etudiant>();
		for (Etudiant i: listeEtudiant) {
			if (i.getNom().equals(nom))
				etudiants.add(i);
		}
		return etudiants;
	}
	
	/**
	 * Ajouter un professeur � la liste des professeurs
	 * @param nom
	 * @param prenom
	 * @param nas
	 * @return
	 */
	public Professeur embaucherProfesseur(String nom, String prenom, int nas) {
		Professeur e = new Professeur(nom, prenom, nas);
		listeProfesseur.add(e);
		return e;
	}
	
	/**
	 * Ajouter un cours dans la liste de cours
	 * @param num : Le num�ro du cours
	 * @return l'instance de l'objet Cours cr��e
	 */
	public Cours ajoutCours(int num) {
		Cours e = new Cours(num);
		listeCours.add(e);
		return e;
	}
	
	/**
	 * Chercher un cours par son num�ro de dossier
	 * @param numero : le num�ro de 
	 * @return Retourne l'instance du Cours correspondant au numero sinon NULL
	 */
	public Cours getCoursByNumero(int numero) {
		for (Cours e : listeCours)
			if (e.getNumero() == numero)
				return e;
		return null;
	}
	
	/**
	 * Ajouter un benevole � la liste de b�n�voles
	 * @param benevole : Un objet Professeur ou Etudiant
	 */
	public void ajouterBenevole(Personne benevole) {
		listeBenevoles.add(benevole);
	}
	
	/**
	 * Rechercher une personne par son nom de famille
	 * @param nom : Le nom de famille de la personne
	 * @return La liste de personne avec ce nom de famille
	 */
	public ArrayList<Personne> rechercheBenevole(String nom) {
		ArrayList<Personne> trouve = new ArrayList<Personne>();
		for (Personne e : listeBenevoles) {
			if (e.nom.equals(nom))
				trouve.add(e);
		}
		return trouve;
	}

}
