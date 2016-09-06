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
	 * Ajoute un �tudiant avec un num�ro de dossier;
	 * @param noDossier : Liste de num�ro de dossier (1 ou + dossier)
	 */
	public void ajoutEtudiant(Etudiant... �tudiants) {
		for (Etudiant e : �tudiants)
			listeEtudiant.add(e);
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
	 * Ajoute un ou plusieurs cours � la liste de cours
	 * @param lCours : 1 ou plusieurs cours
	 */
	public void ajoutCours(Cours... lCours) {
		for (Cours e : lCours)
			listeCours.add(e);
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

}
