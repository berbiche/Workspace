package cegep;

public class Note {

    private Etudiant etudiant;
    private CoursGroupe coursGroupe;
    private int resultat;

    public Etudiant getEtudiant() {
        return this.etudiant;
    }

    public CoursGroupe getCoursGroupe() {
        return this.coursGroupe;
    }

    public int getResultat() {
        return this.resultat;
    }

    public void setResultat(int resultat) {
        this.resultat = resultat;
    }

    Note(Etudiant etudiant, CoursGroupe coursGroupe, int resultat) {
        this.etudiant = etudiant;
        this.coursGroupe = coursGroupe;
        this.resultat = resultat;
    }

    @Override
    public String toString() {
        return "Note: { " + resultat + " -- " + etudiant + " -- " + coursGroupe + " }";
    }

}
