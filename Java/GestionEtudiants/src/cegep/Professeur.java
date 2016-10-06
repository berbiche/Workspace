package cegep;

public class Professeur extends Personne {
	
	private String NAS;
	
	public String getNAS() {
		return NAS;
	}
	
	Professeur(String NAS) {
		super("", "");
		this.NAS= NAS;
	}

	Professeur(String nomFamille, String prenom, String NAS) {
		super(nomFamille, prenom);
		this.NAS = NAS;
	}

	@Override
	public String toString() {
		return "Professeur: " + NAS + ", " + nomFamille + ", " + prenom;
	}
	
}
