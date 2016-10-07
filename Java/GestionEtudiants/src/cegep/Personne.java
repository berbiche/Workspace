package cegep;

import java.util.Comparator;

public abstract class Personne {
	
	protected String nomFamille, prenom;
    static private ComparateurNoms comparateurNoms;
	
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

	public static Comparator<Personne> getComparateurNoms() {
        return comparateurNoms;
    }
	
	protected Personne(String nomFamille, String prenom) {
		this.nomFamille = nomFamille;
		this.prenom = prenom;
	}

	@Override
	public String toString() {
		return "Personne: " + nomFamille + ", " + prenom;
	}

	static private class ComparateurNoms implements Comparator<Personne> {

        @Override
        public int compare(Personne o1, Personne o2) {
            return o1.nomFamille.compareTo(o2.nomFamille);
        }

    }
	
}
