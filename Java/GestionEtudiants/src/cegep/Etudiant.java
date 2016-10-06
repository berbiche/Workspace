package cegep;

import java.util.ArrayList;

public class Etudiant extends Personne {
	
	private String noDossier;
    private ArrayList<Note> notes;

	public String getNoDossier() {
		return noDossier;
	}

	public ArrayList<Note> getNotes() {
        return notes;
    }

	Etudiant(String noDossier) {
		this("", "", noDossier);
	}

	Etudiant(String nomFamille, String prenom, String noDossier) {
		super(nomFamille, prenom);
        notes = new ArrayList<Note>();
		this.noDossier = noDossier;
	}

    void ajouterNote(Note n) {
        notes.add(n);
    }
	
	@Override
	public String toString() {
		return "Etudiant: " + noDossier + ", " + nomFamille + ", " + prenom;
	}
	
}
