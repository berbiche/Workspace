package cegep;

import java.util.ArrayList;
import java.util.List;

public class Cegep {
	
	private List<Etudiant> listeEtudiant;
	private List<Professeur> listeProfesseur;
	private List<Cours> listeCours;
	private List<Personne> listeBenevoles;

	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
		listeProfesseur = new ArrayList<Professeur>();
		listeCours = new ArrayList<Cours>();
		listeBenevoles = new ArrayList<Personne>();
	}
	
	/**
	 * Ajoute un étudiant avec un numéro de dossier;
	 * @param noDossier : Liste de numéro de dossier (1 ou + dossier)
	 * @return l'instance de l'objet Etudiant créée
	 */
	public Etudiant ajoutEtudiant(String noDossier) {
		Etudiant e = new Etudiant(noDossier);
		listeEtudiant.add(e);
		return e;
	}
	
	/**
	 * Rechercher un étudiant par son numéro de dossier
	 * @param noDossier : Le numéro de dossier de l'étudiant
	 * @return null si aucun étudiant possède ce numéro de dossier sinon Etudiant
	 */
	public Etudiant getEtudiantByNoDossier(String noDossier) {
		for (Etudiant e: listeEtudiant) {
			if (e.getNumDossier().equals(noDossier))
				return e;
		}
		return null;
	}
	
	/**
	 * Rechercher tous les étudiants avec un nom de famille donné
	 * @param nom : Le nom de famille recherché
	 * @return la liste d'étudiant avec le nom donné
	 */
	public List<Etudiant> getAllEtudiantsByNomFamille(String nom) {
		List<Etudiant> etudiants = new ArrayList<Etudiant>();
		for (Etudiant i: listeEtudiant) {
			if (i.getNom().equals(nom))
				etudiants.add(i);
		}
		return etudiants;
	}
	
	/**
	 * Ajouter un professeur à la liste des professeurs
	 * @param prenom
	 * @param nom
	 * @param nas
	 * @return
	 */
	public Professeur embaucherProfesseur(String prenom, String nom, int nas) {
		Professeur e = new Professeur(prenom, nom, nas);
		listeProfesseur.add(e);
		return e;
	}
	
	/**
	 * Ajouter un cours dans la liste de cours
	 * @param num : Le numéro du cours
	 * @return l'instance de l'objet Cours créée
	 */
	public Cours ajoutCours(int num) {
		Cours e = new Cours(num);
		listeCours.add(e);
		return e;
	}
	
	/**
	 * Chercher un cours par son numéro de dossier
	 * @param numero : le numéro de 
	 * @return Retourne l'instance du Cours correspondant au numero sinon NULL
	 */
	public Cours getCoursByNumero(int numero) {
		for (Cours e : listeCours)
			if (e.getNumero() == numero)
				return e;
		return null;
	}
	
	/**
	 * Ajouter un benevole à la liste de bénévoles
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
