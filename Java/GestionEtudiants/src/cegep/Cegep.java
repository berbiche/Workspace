package cegep;

import java.util.ArrayList;
import java.util.List;

public class Cegep {
	
	private List<Etudiant> listeEtudiant;
	private List<Cours> listeCours;
	private List<Personne> benevoles;
	private int compteur;

	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
		listeCours = new ArrayList<Cours>();
		benevoles = new ArrayList<Personne>();
		compteur = 0;
	}
	
	/**
	 * Ajoute un �tudiant avec un num�ro de dossier;
	 * @param noDossier : Liste de num�ro de dossier (1 ou + dossier)
	 * @return l'instance de l'objet Etudiant cr��e
	 */
	public Etudiant ajoutEtudiant() {
		Etudiant e = new Etudiant(Integer.toString(compteur++));
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
	public List<Etudiant> getAllEtudiantsByNomFamille(String nom) {
		List<Etudiant> etudiants = new ArrayList<Etudiant>();
		for (Etudiant i: listeEtudiant) {
			if (i.getNom().equals(nom))
				etudiants.add(i);
		}
		return etudiants;
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
	
	public void inscrireBenevole(Personne benevole) {
		benevoles.add(benevole);
	}

}
