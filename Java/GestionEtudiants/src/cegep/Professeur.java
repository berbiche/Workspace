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
	protected Professeur(String nom, String prenom, int nas) {
		super(nom, prenom);
		this.nas = nas;
	}

	@Override
	public String toString() {
		return "Professeur [nas=" + nas + ", nom=" + nom + ", prenom=" + prenom + "]";
	}

	@Override
	public String payerStationnement() {
		return nom + " " + prenom + " a pay� 250$ de stationnement";
	}
	
	
	
}
