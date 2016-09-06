package cegep;

import java.util.ArrayList;

public class CoursGroupe {
	
//	Variables priv�es
	private Cours leCours;
	private int numeroGroupe;
	private String session, local;
	private Professeur prof;
	private ArrayList<Etudiant> listeEtudiant;
	
//	Getters et Setters
	public String getLocal() {
		return local;
	}
	public void setLocal(String local) {
		this.local = local;
	}
	public Professeur getProf() {
		return prof;
	}
	public void setProf(Professeur prof) {
		this.prof = prof;
	}
	public Cours getLeCours() {
		return leCours;
	}
	public int getNumeroGroupe() {
		return numeroGroupe;
	}
	public String getSession() {
		return session;
	}
	
	/**
	 * @param leCours : Instance de l'objet Cours
	 * @param numeroGroupe : le num�ro du groupe
	 * @param session : le num�ro de la session
	 */
	public CoursGroupe(Cours leCours, int numeroGroupe, String session) {
		super();
		listeEtudiant = new ArrayList<Etudiant>();
		this.leCours = leCours;
		this.numeroGroupe = numeroGroupe;
		this.session = session;
	}
	
	/**
	 * Ajouter un �tudiant par son instance
	 * @param e : Instance de l'objet �tudiant
	 */
	public void ajoutEtudiant(Etudiant e) {
		listeEtudiant.add(e);
	}
	
	/**
	 * Retirer un �tudiant par son num�ro de dossier
	 * @param noDossier : Le num�ro de dossier de l'�tudiant
	 */
	public void retirerEtudiant(String noDossier) {
		for (int i = 0; i < listeEtudiant.size(); i++) {
			if (listeEtudiant.get(i).getNumDossier().equals(noDossier)) {
				listeEtudiant.remove(i);
				break;
			}
		}
	}
	
	/**
	 * Retirer un �tudiant par son instance dans la liste d'�tudiant
	 * @param e
	 */
	public void retirerEtudiant(Etudiant e) {
		listeEtudiant.remove(e);
	}
	
}
