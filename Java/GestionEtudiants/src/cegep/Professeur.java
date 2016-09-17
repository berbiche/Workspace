package cegep;

public class Professeur extends Personne {
	
	private String NAS;
	
	public String getNas() {
		return nas;
	}
	
	Professeur(String NAS) {
		super("", "");
		this.NAS= NAS;
	}

	/**
	 * @param nom
	 * @param prenom
	 * @param NAS
	 */
	Professeur(String nom, String prenom, String NAS) {
		super(nom, prenom);
		this.NAS = NAS;
	}

	@Override
	public String toString() {
		return "Professeur [NAS=" + NAS + ", nom=" + nom + ", prenom=" + prenom + "]";
	}

	@Override
	public String payerStationnement() {
		return nom + " " + prenom + " a payé 250$ de stationnement";
	}
	
}
