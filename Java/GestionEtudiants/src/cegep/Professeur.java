package cegep;

public class Professeur extends Personne {
	
	private int nas;
	
	public int getNas() {
		return nas;
	}

	/**
	 * @param nom : Le nom de famille
	 * @param prenom : Le pr�nom
	 * @param nas : Le num�ro d'assurance sociale
	 */
	Professeur(String nom, String prenom, int nas) {
		super(nom, prenom);
		this.nas = nas;
	}
	
	
	
}
