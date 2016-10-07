package cegep;

import java.util.ArrayList;

public class CoursGroupe {
	
//	Variables privées
	private Cours cours;
    private int moyenne;
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
        if (this.professeur != null)
            this.professeur.retirerCours(this);
		this.professeur = professeur;
        professeur.ajouterCours(this);
	}
	public Cours getCours() {
		return cours;
	}
	public int getMoyenne() {
        return moyenne;
    }
	public int getNoGroupe() {
		return noGroupe;
	}
	public String getSession() {
		return session;
	}
	public ArrayList<Etudiant> getListeEtudiant() {
        return listeEtudiant;
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
        etudiant.ajouterCours(this);
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
            Etudiant e = listeEtudiant.get(i);
            if (e.getNoDossier().equals(noDossier)) {
                listeEtudiant.remove(i);
                e.retirerCours(this);
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
        etudiant.retirerCours(this);
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
     * Retourne la note d'un étudiant
     * @param nom le nom de famille de l'étudiant
     * @param prenom le prénom de l'étudiant
     * @return -1 si étudiant non trouvé
     */
    public int getNoteEtudiant(String nom, String prenom) {
        for (Note n: notes) {
            if (n.getEtudiant().getNomFamille().equals(nom) && n.getEtudiant().getPrenom().equals(prenom))
                return n.getResultat();
        }
        return -1;
    }

    boolean ajouterNote(Note n) {
        return notes.add(n);
    }

    /**
     * Ajouter une note à un étudiant
     * @param etudiant instance de l'objet Etudiant
     * @param resultat le résultat de l'étudiant
     */
    public boolean ajouterNote(Etudiant etudiant, int resultat) {
        if (listeEtudiant.contains(etudiant)) {
            Note n = new Note(etudiant, this, resultat);
            notes.add(n);
            etudiant.ajouterNote(n);
            moyenne += ((resultat - moyenne) / notes.size());
            return true;
        }
        return false;
    }

    @Override
    public String toString() {
        return "CoursGroupe: " + cours.getNoCours() + ", " + noGroupe + ", "
                + session + ", " + noLocal + ", " + professeur.getNomFamille()
                + ", " + professeur.getPrenom() + ", " + listeEtudiant.size();
    }

}
