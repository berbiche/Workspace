package cegep;

public class Personne {
	
	protected String nomFamille, prenom;
	
	public String getNomFamille() {
		return nomFamille;
	}

	public void setNomFamille(String nomFamille) {
		this.nomFamille = nomFamille;
	}
	
	public String getPrenom() {
		return prenom;
	}
	
	public void setPrenom(String prenom) {
		this.prenom = prenom;
	}
	
	protected Personne(String nomFamille, String prenom) {
		this.nomFamille = nomFamille;
		this.prenom = prenom;
	}

	@Override
	public String toString() {
		return "Personne: " + nomFamille + ", " + prenom;
	}
	
}
