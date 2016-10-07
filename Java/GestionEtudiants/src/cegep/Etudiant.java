package cegep;

import java.util.ArrayList;

public class Etudiant extends Personne {

	private String noDossier;
    private ArrayList<CoursGroupe> cours;
    private ArrayList<Note> notes;
    private int MOYENNE;

	public String getNoDossier() {
		return noDossier;
	}

	public ArrayList<Note> getNotes() {
        return notes;
    }

    public ArrayList<CoursGroupe> getCours() {
        return cours;
    }

	Etudiant(String noDossier) {
		this("", "", noDossier);
	}

	Etudiant(String nomFamille, String prenom, String noDossier) {
		super(nomFamille, prenom);
        this.noDossier = noDossier;
        notes = new ArrayList<Note>();
        cours = new ArrayList<CoursGroupe>();
	}

    boolean ajouterNote(Note n) {
        return notes.add(n);
    }

    public boolean ajouterNote(CoursGroupe cg, int resultat) {
        if (cours.contains(cg)) {
            Note n = new Note(this, cg, resultat);
            notes.add(n);
            cg.ajouterNote(n);
        }
        return false;
    }

    boolean ajouterCours(CoursGroupe cg) {
        return cours.add(cg);
    }

    boolean retirerCours(CoursGroupe cg) {
        return cours.remove(cg);
    }

    public int getMoyenneCours(CoursGroupe cg) {
        int moyenne = 0;
        int count = 0;
        for (Note n: notes) {
            if (n.getCoursGroupe() == cg) {
                moyenne += n.getResultat();
                count++;
            }
        }
        return moyenne / count;
    }

    /**
     * Rechercher le CoursGroupe
     * @param noCours le numéro du cours
     * @return null si le cours n'est pas trouvé
     */
    public CoursGroupe getCoursGroupe(String noCours, String session) {
        for (CoursGroupe cg: cours) {
            if (cg.getCours().getNoCours().equals(noCours) && cg.getSession().equals(session))
                return cg;
        }
        return null;
    }

    /**
     * Retourne la liste des cours suivi pour une session
     * @param session la session
     * @return
     */
    public ArrayList<CoursGroupe> getCoursSession(String session) {
        ArrayList<CoursGroupe> lesCours = new ArrayList<CoursGroupe>();
        for (CoursGroupe cg: cours) {
            if (cg.getSession().equals(session))
                lesCours.add(cg);
        }
        return lesCours;
    }

    public boolean equals(Object o) {
        return ((Etudiant) o).noDossier == this.noDossier;
    }
	
	@Override
	public String toString() {
		return "Etudiant: " + noDossier + ", " + nomFamille + ", " + prenom;
	}
	
}
