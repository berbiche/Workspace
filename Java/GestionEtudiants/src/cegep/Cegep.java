package cegep;

import java.util.ArrayList;
import java.util.List;

public class Cegep {
	
	private List<Etudiant> listeEtudiant;
	
	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
	}
	
	/**
	 * Ajoute un �tudiant avec un num�ro de dossier;
	 * @param noDossier : Num�ro de dossier de l'�tudiant
	 */
	public void ajoutEtudiant(String noDossier) {
		listeEtudiant.add(new Etudiant(noDossier));
	}
	
	/**
	 * Ajoute un �tudiant avec toutes les informations
	 * @param nom : Le nom de famille
	 * @param prenom : Le pr�nom
	 * @param noDossier : Num�ro de dossier de l'�tudiant
	 */
	public void ajoutEtudiant(String nom, String prenom, String noDossier) {
		listeEtudiant.add(new Etudiant(nom, prenom, noDossier));
	}
	
	/**
	 * Rechercher un �tudiant par son num�ro de dossier
	 * @param noDossier : Le num�ro de dossier de l'�tudiant
	 * @return null si aucun �tudiant poss�de ce num�ro de dossier sinon Etudiant
	 */
	public Etudiant getEtudiantByNoDossier(String noDossier) {
		for (Etudiant i: listeEtudiant) {
			if (i.getNumDossier() == noDossier)
				return i;
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
			if (i.getNom() == nom)
				etudiants.add(i);
		}
		return etudiants;
	}

}
