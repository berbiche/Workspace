package cegep;

public class Personne {
	
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
	
	/**
	 * Constructeur de la classe "Parente"
	 * @param nom : Le nom de famille
	 * @param prenom : Le prénom
	 */
	protected Personne(String nom, String prenom) {
		this.nom = nom;
		this.prenom = prenom;
	}

}
