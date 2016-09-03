package cegep;

import java.util.ArrayList;
import java.util.List;

public class Cegep {
	
	private List<Etudiant> listeEtudiant;
	
	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
	}
	
	/**
	 * Ajoute un étudiant avec un numéro de dossier;
	 * @param noDossier : Numéro de dossier de l'étudiant
	 */
	public void ajoutEtudiant(String noDossier) {
		listeEtudiant.add(new Etudiant(noDossier));
	}
	
	/**
	 * Ajoute un étudiant avec toutes les informations
	 * @param nom : Le nom de famille
	 * @param prenom : Le prénom
	 * @param noDossier : Numéro de dossier de l'étudiant
	 */
	public void ajoutEtudiant(String nom, String prenom, String noDossier) {
		listeEtudiant.add(new Etudiant(nom, prenom, noDossier));
	}
	
	/**
	 * Rechercher un étudiant par son numéro de dossier
	 * @param noDossier : Le numéro de dossier de l'étudiant
	 * @return null si aucun étudiant possède ce numéro de dossier sinon Etudiant
	 */
	public Etudiant getEtudiantByNoDossier(String noDossier) {
		for (Etudiant i: listeEtudiant) {
			if (i.getNumDossier() == noDossier)
				return i;
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
			if (i.getNom() == nom)
				etudiants.add(i);
		}
		return etudiants;
	}

}
