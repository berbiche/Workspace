package cegep;

import java.util.ArrayList;

public class CoursGroupe {
	
//	Variables privées
	private Cours cours;
	private int noGroupe;
	private String session, noLocal;
	private Professeur professeur;
    private ArrayList<Etudiant> listeEtudiant;
	private ArrayList<Note> notes;
	
//	Getters et Setters
	public String getNoLocal() {
		return noLocal;
	}
	public void setNoLocal(String noLocal) {
		this.noLocal = noLocal;
	}
	public Professeur getProfesseur() {
		return professeur;
	}
	public void setProfesseur(Professeur professeur) {
		this.professeur = professeur;
	}
	public Cours getCours() {
		return cours;
	}
	public int getNoGroupe() {
		return noGroupe;
	}
	public String getSession() {
		return session;
	}
	
	/**
	 * @param cours Instance de l'objet Cours
	 * @param noGroupe le numéro du groupe
	 * @param session le numéro de la session
	 */
	public CoursGroupe(Cours cours, int noGroupe, String session) {
        this.notes = new ArrayList<Note>();
        this.listeEtudiant = new ArrayList<Etudiant>();
		this.cours = cours;
		this.noGroupe = noGroupe;
		this.session = session;
	}

    /**
     * Inscrit l'étudiant au CoursGroupe
     * @param etudiant
     */
	public void inscrireEtudiant(Etudiant etudiant) {
        listeEtudiant.add(etudiant);
    }

    /**
     * Recherche un étudiant dans la liste d'étudiant
     * @param noDossier le numéro de dossier de l'étudiant
     * @return l'instance de l'étudiant trouvée ou NULL
     */
    public Etudiant getEtudiant(String noDossier) {
        for (Etudiant e: listeEtudiant) {
            if (e.getNoDossier().equals(noDossier))
                return e;
        }
        return null;
    }

    /**
     * Retire un étudiant du cours
     * @param noDossier le numéro de dossier de l'étudiant
     * @return le succès de l'opération
     */
    public boolean retirerEtudiant(String noDossier) {
        for (int i = 0; i < listeEtudiant.size(); i++) {
            if (listeEtudiant.get(i).getNoDossier().equals(noDossier)) {
                listeEtudiant.remove(i);
                return true;
            }
        }
        return false;
    }

    /**
     * Retire un étudiant du cours
     * @param etudiant
     * @return le succès de l'opération
     */
    public boolean retirerEtudiant(Etudiant etudiant) {
        return listeEtudiant.remove(etudiant);
    }

    /**
     * Retourne la note d'un étudiant
     * @param etudiant l'instance Etudiant
     * @return -1 si étudiant non trouvé
     */
	public int getNoteEtudiant(Etudiant etudiant) {
        for (Note n: notes) {
            if (n.getEtudiant().equals(etudiant))
                return n.getResultat();
        }
        return -1;
    }

    /**
     * Retourne la note d'un étudiant
     * @param noDossier le numéro de dossier de l'étudiant
     * @return -1 si étudiant non trouvé
     */
    public int getNoteEtudiant(String noDossier) {
        for (Note n: notes) {
            if (n.getEtudiant().getNoDossier().equals(noDossier))
                return n.getResultat();
        }
        return -1;
    }

    /**
     * Ajouter une note à un étudiant
     * @param etudiant instance de l'objet Etudiant
     * @param resultat le résultat de l'étudiant
     */
    public void ajouterNote(Etudiant etudiant, int resultat) {
        Note n = new Note(etudiant, this, resultat);
        notes.add(n);
        etudiant.ajouterNote(n);
    }

}
