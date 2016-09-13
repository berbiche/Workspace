package cegep;

public abstract class Personne {
	
	protected String nom, prenom;
	
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
	
	protected Personne(String prenom, String nom) {
		this.prenom = prenom;
		this.nom = nom;
	}
	
	public String payerStationnement() {
		return nom + " " + prenom + " a payé 300$ de stationnement";
	}

	@Override
	public String toString() {
		return "Personne [nom=" + nom + ", prenom=" + prenom + "]";
	}
	
	
}
