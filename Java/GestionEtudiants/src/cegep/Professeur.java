package cegep;

import java.lang.reflect.Array;
import java.util.ArrayList;
import java.util.Iterator;

public class Professeur extends Personne {
	
	private String NAS;
	private ArrayList<CoursGroupe> cours;
	
	public String getNAS() {
		return NAS;
	}

	public ArrayList<CoursGroupe> getCours() {
        return cours;
    }
	
	Professeur(String NAS) {
        this("", "", NAS);
	}

	Professeur(String nomFamille, String prenom, String NAS) {
		super(nomFamille, prenom);
		this.NAS = NAS;
        this.cours = new ArrayList<CoursGroupe>();
	}

	void ajouterCours(CoursGroupe cg) {
        cours.add(cg);
    }

    void retirerCours(CoursGroupe cg) {
        cours.remove(cg);
    }

    public ArrayList<CoursGroupe> getCoursSession(String session) {
        ArrayList<CoursGroupe> lesCours = new ArrayList<CoursGroupe>();
        for (CoursGroupe cg: cours) {
            if (cg.getSession().equals(session))
                lesCours.add(cg);
        }
        return lesCours;
    }

    public boolean equals(Professeur p) {
        return p.nomFamille.equals(this.nomFamille) && p.prenom.equals(this.prenom);
    }

    public int compareTo(Professeur p) {
        return p.NAS.compareTo(this.NAS);
    }

	@Override
	public String toString() {
		return "Professeur: " + NAS + ", " + nomFamille + ", " + prenom;
	}
	
}
