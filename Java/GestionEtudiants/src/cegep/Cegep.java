package cegep;

import java.util.ArrayList;

public class Cegep {
	
	private ArrayList<Etudiant> listeEtudiant;
	private ArrayList<Professeur> listeProfesseur;
	private ArrayList<Cours> listeCours;
	private ArrayList<CoursGroupe> listeCoursGroupe;
	private ArrayList<Personne> listeBenevoles;
    private int numDossierEtudiant;

	public Cegep() {
		listeEtudiant = new ArrayList<Etudiant>();
		listeProfesseur = new ArrayList<Professeur>();
		listeCours = new ArrayList<Cours>();
		listeBenevoles = new ArrayList<Personne>();
        listeCoursGroupe = new ArrayList<CoursGroupe>();
        numDossierEtudiant = 1001;
	}
	
	/**
	 * Ajoute un étudiant avec un numéro de dossier;
	 * @param noDossier le numéro de dossier de l'étudiant
	 * @return l'instance de l'objet Etudiant créée
	 */
	public Etudiant admettreEtudiant(String noDossier) {
		Etudiant e = new Etudiant(noDossier);
		listeEtudiant.add(e);
		return e;
	}

    /**
     * Ajoute un étudiant avec un numéro de dossier
     * @param nomFamille le nom de famille
     * @param prenom le prénom
     * @return l'instance de l'objet Etudiant créée
     */
	public Etudiant admettreEtudiant(String nomFamille, String prenom) {
        Etudiant e = new Etudiant(nomFamille, prenom, (numDossierEtudiant++)+"");
        listeEtudiant.add(e);
        return e;
    }
	
	/**
	 * Rechercher un étudiant par son numéro de dossier
	 * @param noDossier Le numéro de dossier de l'étudiant
	 * @return null si aucun étudiant possède ce numéro de dossier sinon Etudiant
	 */
	public Etudiant getEtudiant(String noDossier) {
		for (Etudiant e: listeEtudiant) {
			if (e.getNoDossier().equals(noDossier))
				return e;
		}
		return null;
	}

    /**
     * Rechercher un étudiant par son nom de famille et son prénom
     * @param nomFamille le nom de famille de l'étudiant
     * @param prenom le prénom de l'étudiant
     * @return null si aucun étudiant possède ce numéro de dossier sinon Etudiant
     */
	public Etudiant getEtudiant(String nomFamille, String prenom) {
        for (Etudiant e: listeEtudiant) {
            if (e.getNomFamille().equals(nomFamille) && e.getPrenom().equals(prenom))
                return e;
        }
        return null;
    }

	/**
	 * Rechercher tous les étudiants avec un nom de famille donné
	 * @param nomFamille Le nom de famille recherché
	 * @return la liste d'étudiant avec le nom donné
	 */
	public ArrayList<Etudiant> rechercherEtudiant(String nomFamille) {
		ArrayList<Etudiant> etudiants = new ArrayList<Etudiant>();
		for (Etudiant i: listeEtudiant) {
			if (i.getNomFamille().equals(nomFamille))
				etudiants.add(i);
		}
		return etudiants;
	}

	/**
	 * Ajouter un professeur à la liste des professeurs
	 * @param nomFamille
	 * @param prenom
	 * @param NAS
	 * @return l'instance de l'objet Professeur créée
	 */
	public Professeur embaucherProfesseur(String NAS, String nomFamille, String prenom) {
		Professeur e = new Professeur(nomFamille, prenom, NAS);
		listeProfesseur.add(e);
		return e;
	}

    /**
     * Rechercher un professeur par son NAS
     * @param NAS le NAS du professeur
     * @return null si aucun professeur possède ce NAS
     */
	public Professeur getProfesseur(String NAS) {
        for (Professeur p: listeProfesseur) {
            if (p.getNAS().equals(NAS))
                return p;
        }
        return null;
    }

    /**
     * Rechercher un professeur par son nom de famille et son prénom
     * @param nomFamille le nom de famille du professeur
     * @param prenom le prénom du professeur
     * @return null si aucun professeur possède ce nom de famille et prénom
     */
    public Professeur getProfesseur(String nomFamille, String prenom) {
        for (Professeur p: listeProfesseur) {
            if (p.getNomFamille().equals(nomFamille) && p.getPrenom().equals(prenom))
                return p;
        }
        return null;
    }

	/**
	 * Ajouter un cours dans la liste de cours
	 * @param numero Le numéro du cours
	 * @return l'instance de l'objet Cours créée
	 */
	public Cours ajouterCours(String numero) {
		Cours e = new Cours(numero);
		listeCours.add(e);
		return e;
	}

    /**
     * Ajouter un cours dans la liste de cours
     * @param numero
     * @param nom
     * @param nbHeures
     * @return l'instance de l'objet Cours créée
     */
	public Cours ajouterCours(String numero, String nom, int nbHeures) {
        Cours e = new Cours(numero, nom, nbHeures);
        listeCours.add(e);
        return e;
    }
	
	/**
	 * Chercher un cours par son numéro
	 * @param numero le numéro de cours
	 * @return l'instance du Cours correspondante au numéro sinon NULL
	 */
	public Cours getCours(String numero) {
		for (Cours e : listeCours)
			if (e.getNoCours().equals(numero))
				return e;
		return null;
	}

    /**
     * Ajouter un coursgroupe dans la liste de coursgroupe
     * @param cours
     * @param noGroupe
     * @param session
     * @return l'instance de l'objet CoursGroupe créée
     */
	public CoursGroupe ajouterCoursGroupe(Cours cours, int noGroupe, String session) {
        CoursGroupe cg = new CoursGroupe(cours, noGroupe, session);
        listeCoursGroupe.add(cg);
        return cg;
    }

    /**
     * Chercher un CoursGroupe par son nom, numéro de groupe et la session
     * @param numero
     * @param noGroupe
     * @param session
     * @return
     */
    public CoursGroupe getCoursGroupe(String numero, int noGroupe, String session) {
		for (CoursGroupe cg: listeCoursGroupe) {
            if (cg.getNoGroupe() == noGroupe
                && cg.getCours().getNoCours().equals(numero)
                && cg.getSession().equals(session)) {
                return cg;
            }
        }
        return null;
    }
	
	/**
	 * Ajouter un benevole à la liste de bénévoles
	 * @param benevole un objet Personne
	 */
	public void ajouterBenevole(Personne benevole) {
		listeBenevoles.add(benevole);
	}
	
	/**
	 * Rechercher une personne par son nom de famille
	 * @param nomFamille le nom de famille de la personne
	 * @return la liste de personnes avec le nom de famille
	 */
	public ArrayList<Personne> rechercherBenevole(String nomFamille) {
		ArrayList<Personne> trouve = new ArrayList<Personne>();
		for (Personne e : listeBenevoles) {
			if (e.nomFamille.equals(nomFamille))
				trouve.add(e);
		}
		return trouve;
	}

}
