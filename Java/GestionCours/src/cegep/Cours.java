package cegep;

public class Cours {
	
	private String nom, noCours;
	private int nbHeures;
	
	public String getNom() {
		return nom;
	}
	public void setNom(String nom) {
		this.nom = nom;
	}
	public int getNbHeures() {
		return nbHeures;
	}
	public void setNbHeures(int nbHeures) {
		this.nbHeures = nbHeures;
	}
	public String getNoCours() {
		return noCours;
	}
	
	Cours(String noCours) {
		this(noCours, "", 0);
	}

	Cours(String noCours, String nom, int nbHeures) {
		super();
		this.noCours = noCours;
		this.nom = nom;
		this.nbHeures = nbHeures;
	}

	@Override
	public String toString() {
		return "Cours: " + noCours + ", " + nom + ", " + nbHeures;
	}
	
}
