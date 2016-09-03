package cegep;

public class Etudiant {
	
	private String nom, prenom, noDossier;
	
	public String getNom() {
		return nom;
	}

	public void setNom(String nom) {
		this.nom = nom;
	}

	public String getPrenom() {
		return prenom;
	}

	public void setPrenom(String prenom) {
		this.prenom = prenom;
	}

	public String getNumDossier() {
		return noDossier;
	}
	
	public Etudiant(String noDossier) {
		this("", "", noDossier);
	}

	public Etudiant(String nom, String prenom, String noDossier) {
		this.nom = nom;
		this.prenom = prenom;
		this.noDossier = noDossier;
	}
	
}
