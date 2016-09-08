package cegep;

public class Professeur extends Personne {
	
	private int nas;
	
	public int getNas() {
		return nas;
	}

	/**
	 * @param nom : Le nom de famille
	 * @param prenom : Le prénom
	 * @param nas : Le numéro d'assurance sociale
	 */
	Professeur(String nom, String prenom, int nas) {
		super(nom, prenom);
		this.nas = nas;
	}
	
	
	
}
