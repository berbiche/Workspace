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
        numDossierEtudiant = 0;
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
	 * @return null si aucun étudiant posséde ce numéro de dossier sinon Etudiant
	 */
	public Etudiant getEtudiant(String noDossier) {
		for (Etudiant e: listeEtudiant) {
			if (e.getNoDossier().equals(noDossier))
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

	public Professeur getProfesseur(String NAS) {
        for (Professeur p: listeProfesseur) {
            if (p.getNAS().equals(NAS))
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
			if (e.getNoCours() == numero)
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
     * Chercher un CoursGroupe par son numéro
     * @param noGroupe le numéro du CoursGroupe
     * @return l'instance du CoursGroupe correspondante au numéro sinon NULL
     */
    public CoursGroupe getCoursGroupe(int noGroupe) {
        for (CoursGroupe cg: listeCoursGroupe) {
            if (cg.getNoGroupe() == noGroupe)
                return cg;
        }
        return null;
    }

    public CoursGroupe getCoursGroupe(String numero, int noGroupe, String session) {
        return getCoursGroupe(noGroupe);
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
	public ArrayList<Personne> rechercheBenevole(String nomFamille) {
		ArrayList<Personne> trouve = new ArrayList<Personne>();
		for (Personne e : listeBenevoles) {
			if (e.nomFamille.equals(nomFamille))
				trouve.add(e);
		}
		return trouve;
	}

}
