package principal;

import java.util.*;
import cegep.*;

import static org.junit.Assert.*;

import org.junit.Before;
import org.junit.Test;

public class CegepTest {

	Etudiant e1, e2, e3;
	Cours c;
	CoursGroupe cg;
	Cegep cegep;

	@Before
	public void setUp() throws Exception {
		cegep = new Cegep();
		e1 = cegep.admettreEtudiant("Tremblay", "Robert");
		e2 = cegep.admettreEtudiant("Jacques", "Jean");
		e3 = cegep.admettreEtudiant("Tremblay", "William");
		c = cegep.ajouterCours("420-D61", "PO et Structures de donn�es", 6);
		cg = cegep.ajouterCoursGroupe(c, 1, "A14");
	}

	public static void main(String[] args) {

	}

	@Test
	public void testEtudiantStringStringString() {
		// Setup
		String nomFamille = "Tremblay";
		String prenom = "Robert";
		
		// Act
		Etudiant e = cegep.admettreEtudiant(nomFamille, prenom);
		
		// Assert
		assertEquals(nomFamille, e.getNomFamille());
		assertEquals(prenom, e.getPrenom());
	}

	@Test
	public void testProfesseurStringStringString() {
		// Setup
		String nas = "131111123";
		String nomFamille = "Tremblay";
		String prenom = "Robert";
		
		// Act
		Professeur e = cegep.embaucherProfesseur(nas, nomFamille, prenom);
		
		// Assert
		assertEquals(nas, e.getNAS());
		assertEquals(nomFamille, e.getNomFamille());
		assertEquals(prenom, e.getPrenom());
	}

	@Test
	public void testCoursStringStringInt() {
		// Setup
		String noCours = "420-D61";
		String nom = "PO et Structures de donn�es";
		int nbHeures = 6;
		
		// Act
		Cours e = cegep.ajouterCours(noCours, nom, nbHeures);
		
		// Assert
		assertEquals(noCours, e.getNoCours());
		assertEquals(nom, e.getNom());
		assertEquals(nbHeures, e.getNbHeures());
	}

	@Test
	public void testCoursGroupe() {
		// Setup

		// Act
		CoursGroupe cg1 = cegep.ajouterCoursGroupe(c, 1, "A14");

		// Assert
		assertSame(c, cg1.getCours());
		assertEquals(1, cg1.getNoGroupe());
		assertEquals("A14", cg1.getSession());
	}

	@Test
	public void testAssignerProfesseur() {
		// Setup
		Professeur p = cegep.embaucherProfesseur("123456789", "Blo", "Joe");

		// Act
		cg.setProfesseur(p);

		// Assert
		assertSame(p, cg.getProfesseur());
	}

	@Test
	public void testInscrireEtudiants() {
		// Setup

		// Act
		cg.inscrireEtudiant(e1);
		cg.inscrireEtudiant(e2);
		cg.inscrireEtudiant(e3);

		// Assert
		assertSame(e1, cg.getEtudiant(e1.getNoDossier()));
		assertSame(e2, cg.getEtudiant(e2.getNoDossier()));
		assertSame(e3, cg.getEtudiant(e3.getNoDossier()));
	}

	@Test
	public void testRetirerEtudiantEtudiant() {
		// Setup
		cg.inscrireEtudiant(e1);
		cg.inscrireEtudiant(e2);
		cg.inscrireEtudiant(e3);

		// Act
		boolean resultat = cg.retirerEtudiant(e2);

		// Assert
		assertSame(e1, cg.getEtudiant(e1.getNoDossier()));
		assertSame(null, cg.getEtudiant(e2.getNoDossier()));
		assertEquals(true, resultat);
		assertSame(e3, cg.getEtudiant(e3.getNoDossier()));
	}

	@Test
	public void testRetirerEtudiantString() {
		// Setup
		cg.inscrireEtudiant(e1);
		cg.inscrireEtudiant(e2);
		cg.inscrireEtudiant(e3);

		// Act
		boolean resultat = cg.retirerEtudiant(e2.getNoDossier());
		
		// Assert
		assertSame(e1, cg.getEtudiant(e1.getNoDossier()));
		assertSame(null, cg.getEtudiant(e2.getNoDossier()));
		assertEquals(true, resultat);
		assertSame(e3, cg.getEtudiant(e3.getNoDossier()));
	}
	
	@Test
	public void testAdmettreEtudiant() {
		// Setup
		String nomFamille = "Tremblay";
		String prenom = "Robert";
		Cegep c = new Cegep();

		// Act
		Etudiant resultat = c.admettreEtudiant(nomFamille,	prenom);

		// Assert
		assertEquals(nomFamille, resultat.getNomFamille());
		assertEquals(prenom, resultat.getPrenom());
	}

	@Test
	public void testGetEtudiant() {
		// Setup
		Cegep c = new Cegep();
		Etudiant e1 = c.admettreEtudiant("Tremblay", "Robert");
		Etudiant e2 = c.admettreEtudiant("Jacques", "Jean");
		Etudiant e3 = c.admettreEtudiant("Tremblay", "William");
		
		// Act
		Etudiant gE1 = c.getEtudiant(e1.getNoDossier());
		Etudiant gE2 = c.getEtudiant(e2.getNoDossier());
		Etudiant gE3 = c.getEtudiant(e3.getNoDossier());
		
		// Assert
		assertEquals(e1.getPrenom(), gE1.getPrenom());
		assertEquals(e2.getPrenom(), gE2.getPrenom());
		assertEquals(e3.getPrenom(), gE3.getPrenom());
	}

	@Test
	public void testRechercherEtudiantPlusieurs() {
		// Act
		ArrayList<Etudiant> liste = cegep.rechercherEtudiant(e1.getNomFamille());
		
		// Assert
		assertEquals(2, liste.size());
		assertSame(e1, liste.get(0));
		assertSame(e3, liste.get(1));
	}

	@Test
	public void testRechercherEtudiantAucun() {
		// Act
		ArrayList<Etudiant> liste = cegep.rechercherEtudiant("Absent");
		
		// Assert
		assertEquals(0, liste.size());
	}

	@Test
	public void testEmbaucherProfesseur() {
		// Setup
		String nas = "123456789";
		String nomFamille = "Tremblay";
		String prenom = "Robert";
		Cegep c = new Cegep();

		// Act
		Professeur resultat = c.embaucherProfesseur(nas, nomFamille, prenom);

		// Assert
		Professeur p = c.getProfesseur(nas);
		assertEquals(nas, p.getNAS());
		assertEquals(nomFamille, p.getNomFamille());
		assertEquals(prenom, p.getPrenom());
		assertSame(p, resultat);
	}

	@Test
	public void testGetProfesseur() {
		// Setup
		Cegep c = new Cegep();
		c.embaucherProfesseur("131111133", "Tremblay", "Robert");
		c.embaucherProfesseur("132222233", "Jacques", "Jean");
		c.embaucherProfesseur("133333333", "Jones", "William");
		
		// Act
		Professeur e11 = c.getProfesseur("131111133");
		Professeur e22 = c.getProfesseur("132222233");
		Professeur e33 = c.getProfesseur("133333333");
		
		// Assert
		assertEquals("Tremblay", e11.getNomFamille());
		assertEquals("Jacques", e22.getNomFamille());
		assertEquals("Jones", e33.getNomFamille());
	}

	@Test
	public void testAjouterCours() {
		// Setup
		String noCours = "420-D61";
		String nom = "PO";
		int nbHeures = 6;
		Cegep c = new Cegep();

		// Act
		Cours resultat = c.ajouterCours(noCours, nom, nbHeures);

		// Assert
		Cours co = c.getCours(noCours);
		assertEquals(noCours, co.getNoCours());
		assertEquals(nom, co.getNom());
		assertEquals(nbHeures, co.getNbHeures());
		assertSame(co, resultat);
	}

	@Test
	public void testGetCours() {
		// Setup
		Cegep c = new Cegep();
		c.ajouterCours("420-D61", "PO1", 6);
		c.ajouterCours("420-D62", "PO2", 7);
		c.ajouterCours("420-D63", "PO3", 8);
		
		// Act
		Cours c11 = c.getCours("420-D61");
		Cours c22 = c.getCours("420-D62");
		Cours c33 = c.getCours("420-D63");
		
		// Assert
		assertEquals("420-D61", c11.getNoCours());
		assertEquals("420-D62", c22.getNoCours());
		assertEquals("420-D63", c33.getNoCours());
	}

	@Test
	public void testAjouterCoursGroupe() {
		// Setup
		Cegep c = new Cegep();
		Cours co = c.ajouterCours("420-D61", "PO", 6);

		// Act
		CoursGroupe resultat = c.ajouterCoursGroupe(co, 1, "A14");

		// Assert
		CoursGroupe cg = c.getCoursGroupe(co.getNoCours(), 1, "A14");
		assertSame(co, cg.getCours());
		assertEquals(1, cg.getNoGroupe());
		assertEquals("A14", cg.getSession());
		assertSame(cg, resultat);
	}

	@Test
	public void testGetCoursGroupe() {
		// Setup
		Cegep c = new Cegep();
		c.ajouterCours("420-D61", "PO1", 6);
		c.ajouterCours("420-D62", "PO2", 7);
		c.ajouterCoursGroupe(c.getCours("420-D61"), 1, "A14");
		c.ajouterCoursGroupe(c.getCours("420-D61"), 2, "A14");
		c.ajouterCoursGroupe(c.getCours("420-D62"), 1, "A14");
		
		// Act
		CoursGroupe c11 = c.getCoursGroupe("420-D61", 1, "A14");
		CoursGroupe c22 = c.getCoursGroupe("420-D61", 2, "A14");
		CoursGroupe c33 = c.getCoursGroupe("420-D62", 1, "A14");
		
		// Assert
		assertEquals("420-D61", c11.getCours().getNoCours());
		assertEquals(1, c11.getNoGroupe());
		assertEquals("420-D61", c22.getCours().getNoCours());
		assertEquals(2, c22.getNoGroupe());
		assertEquals("420-D62", c33.getCours().getNoCours());
		}

}
