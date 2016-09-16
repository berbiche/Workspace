package cegep;

public class Professeur extends Personne {
	
	private int nas;
	
	public int getNas() {
		return nas;
	}
	
	Professeur(String nas) {
		super("", "");
		this.nas = nas;
	}

	/**
	 * @param nom
	 * @param prenom
	 * @param nas
	 */
	Professeur(String nom, String prenom, int nas) {
		super(nom, prenom);
		this.nas = nas;
	}

	@Override
	public String toString() {
		return "Professeur [nas=" + nas + ", nom=" + nom + ", prenom=" + prenom + "]";
	}

	@Override
	public String payerStationnement() {
		return nom + " " + prenom + " a payé 250$ de stationnement";
	}
	
	
	
}
