package cegep;

import java.util.ArrayList;
import java.util.List;

public class Cegep {
	
	private List<Etudiant> listeEtudiant;
	private List<Cours> listeCours;

	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
		listeCours = new ArrayList<Cours>();
	}
	
	/**
	 * Ajoute un étudiant avec un numéro de dossier;
	 * @param noDossier : Liste de numéro de dossier (1 ou + dossier)
	 */
	public void ajoutEtudiant(Etudiant... étudiants) {
		for (Etudiant e : étudiants)
			listeEtudiant.add(e);
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
	 * Ajoute un ou plusieurs cours à la liste de cours
	 * @param lCours : 1 ou plusieurs cours
	 */
	public void ajoutCours(Cours... lCours) {
		for (Cours e : lCours)
			listeCours.add(e);
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

}
