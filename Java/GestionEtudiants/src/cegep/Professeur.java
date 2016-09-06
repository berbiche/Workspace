package cegep;

public class Professeur {
	
	private String nom, prenom;
	private int nas;
	
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
	
	public int getNas() {
		return nas;
	}

	/**
	 * @param nom : Le nom de famille
	 * @param prenom : Le prénom
	 * @param nas : Le numéro d'assurance sociale
	 */
	public Professeur(String nom, String prenom, int nas) {
		super();
		this.nom = nom;
		this.prenom = prenom;
		this.nas = nas;
	}
	
	
	
}
