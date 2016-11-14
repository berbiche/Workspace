package cegep;

import java.util.ArrayList;
import java.util.Comparator;

public class Etudiant extends Personne {

	private String noDossier;
    private ArrayList<CoursGroupe> cours;
    private ArrayList<Note> notes;
    static private ComparateurNumeroDossier comparateurNumeroDossier;

	public String getNoDossier() {
		return noDossier;
	}

	public ArrayList<Note> getNotes() {
        return notes;
    }

    public ArrayList<CoursGroupe> getCours() {
        return cours;
    }

    public static Comparator<Etudiant> getComparateurNumeroDossier() {
        return comparateurNumeroDossier;
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

    public boolean ajouterNote(CoursGroupe cg, int resultat) {
        if (cours.contains(cg)) {
            Note n = new Note(this, cg, resultat);
            notes.add(n);
            cg.ajouterNote(n);
        }
        return false;
    }

    public Note getNoteCours(CoursGroupe cg) {
        for (Note n: notes) {
            if (n.getCoursGroupe().equals(cg))
               return n;
        }
        return null;
    }

    public ArrayList<Note> getNotesSession(String session) {
        ArrayList<Note> listeNotes = new ArrayList<Note>();
        for (Note n: notes) {
            if (n.getCoursGroupe().getSession().equals(session))
                listeNotes.add(n);
        }
        return listeNotes;
    }

    public int getMoyenneCours(CoursGroupe cg) {
        if (notes.size() > 0) {
            int moyenne = 0, count = 0;
            for (Note n : notes) {
                if (n.getCoursGroupe() == cg) {
                    moyenne += n.getResultat();
                    count++;
                }
            }
            return moyenne / count;
        }
        return -1;
    }

    public int getMoyenneSession(String session) {
        if (notes.size() > 0) {
            int moyenne = 0, count = 0;
            for (Note n : notes) {
                if (n.getCoursGroupe().getSession().equals(session)) {
                    moyenne += n.getResultat();
                    count++;
                }
            }
            return moyenne / count;
        }
        return -1;
    }

    public int getMoyenne() {
        if (notes.size() > 0) {
            int moyenne = 0;
            for (Note n : notes) {
                moyenne += n.getResultat();
            }
            return moyenne / notes.size();
        }
        return -1;
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

    static private class ComparateurNumeroDossier implements Comparator<Etudiant> {
        @Override
        public int compare(Etudiant o1, Etudiant o2) {
            return o1.compareTo(o2);
        }

    }

    boolean ajouterNote(Note n) {
        return notes.add(n);
    }

    boolean ajouterCours(CoursGroupe cg) {
        return cours.add(cg);
    }

    boolean retirerCours(CoursGroupe cg) {
        return cours.remove(cg);
    }

    public boolean equals(Etudiant e) {
        return e.noDossier.equals(this.noDossier);
    }

    public int compareTo(Etudiant e) {
        return e.noDossier.compareTo(this.noDossier);
    }
	
	@Override
	public String toString() {
		return "Etudiant: " + noDossier + ", " + nomFamille + ", " + prenom;
	}
	
}
