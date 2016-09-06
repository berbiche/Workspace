package cegep;
import java.util.List;

public class Principal {

	public static void main(String[] args) {
		
//		Q01
		System.out.println("Création d'un cegep");
		Cegep cegep = new Cegep();
		
//		Q02
		System.out.println("Création de l'étudiant Robert Tremblay 1311111");
		Etudiant e1 = new Etudiant("Tremblay", "Robert", "1311111");
		System.out.println(e1);
		
//		Q03
		System.out.println("Création de l'étudiant Jean Jacques 1422222");
		Etudiant e2 = new Etudiant("Jacques", "Jean", "1422222");
		System.out.println(e2);
		
//		Q04
		System.out.println("Création de l'étudiant William Tremblay 1433333");
		Etudiant e3 = new Etudiant("Tremblay", "William", "1433333");
		System.out.println(e3);
		
//		Q05
		System.out.println("Admission des étudiants");
		cegep.ajoutEtudiant(e1, e2, e3);
		
//		Q06
		Etudiant e4 = cegep.getEtudiantByNoDossier("1433333");
		if (e4 != null)
			System.out.println("Get Etudiant by Dossier: 1433333 => "+e4.getPrenom()+" "+e4.getNom());
		
//		Q07
		Etudiant e5 = cegep.getEtudiantByNoDossier("1355555");
		if (e5 != null)
			System.out.println("Get Etudiant by Dossier: 1355555 => "+e5.getPrenom()+" "+e5.getNom());
		
//		Q08
		List<Etudiant> tremblay = cegep.getAllEtudiantsByNomFamille("Tremblay");
		if (tremblay.size() == 0) {
			System.out.println("Aucun étudiant nommé Tremblay");
		} else {
			System.out.println(tremblay.size()+" étudiant(s) nommé(s) Tremblay");
			for (Etudiant e : tremblay)
				System.out.println(e);
		}
		
//		Q09
		List<Etudiant> savard = cegep.getAllEtudiantsByNomFamille("Savard");
		if (savard.size() == 0) {
			System.out.println("Aucun étudiant nommé Savard");
		} else {
			System.out.println(savard.size()+" étudiant(s) nommé(s) Savard");
			for (Etudiant e : savard)
				System.out.println(e);
		}
	}

}
