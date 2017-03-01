package application;

import java.util.Comparator;

public class Etudiant {

	private String noDossier;
	private String nomFamille, prenom;
    static private ComparateurNumeroDossier comparateurNumeroDossier;

	public String getNoDossier() {
		return noDossier;
	}

	public String getNomFamille() { return nomFamille; }

	public String getPrenom() { return prenom; }

    public static Comparator<Etudiant> getComparateurNumeroDossier() {
        return comparateurNumeroDossier;
    }

	Etudiant(String noDossier) {
		this("", "", noDossier);
	}

	Etudiant(String nomFamille, String prenom, String noDossier) {
		this.nomFamille = nomFamille;
		this.prenom = prenom;
        this.noDossier = noDossier;
	}

    static private class ComparateurNumeroDossier implements Comparator<Etudiant> {
        @Override
        public int compare(Etudiant o1, Etudiant o2) {
            return o1.compareTo(o2);
        }

    }

    public int compareTo(Etudiant e) {
        return e.noDossier.compareTo(this.noDossier);
    }

	@Override
	public boolean equals(Object o) {
		return ((Etudiant) o).noDossier.equals(this.noDossier);
	}

	@Override
	public String toString() {
		return "Etudiant: " + noDossier + ", " + nomFamille + ", " + prenom;
	}
	
}
