package application;
import static org.junit.Assert.*;

import java.util.List;

import org.junit.Before;
import org.junit.Test;

import dictionnaire.Dictionnaire;


public class TestsDictionnaire {
	
	Dictionnaire d;
	int nbMots;

	@Before
	public void setUp() throws Exception {
		d = new Dictionnaire();
	}

	private void initialiserDico1() {
		d.ajouter("m4", "m4d1");
		d.ajouter("m2", "m2d1");
		d.ajouter("m1", "m1d1");
		d.ajouter("m3", "m3d1");
		nbMots = 4;
	}
	
	@Test
	public void testDictionnaire() {
		assertEquals(0, d.nbMots());
		assertEquals(0, d.mots().size());
	}

	@Test
	public void testAjouter() {
		// Act
		d.ajouter("m1", "m1d1");
		
		// Assert
		assertEquals(1, d.nbMots());
		assertEquals(1, d.mots().size());
		assertEquals("m1", d.mots().get(0));
		assertTrue(d.contient("m1"));
		assertEquals(1, d.definitions("m1").size());
		assertEquals("m1d1", d.definitions("m1").get(0));
		assertFalse(d.contient("m3"));
	}

	@Test
	public void testAjouter2Mots() {
		// Act
		d.ajouter("m1", "m1d1");
		d.ajouter("m2", "m2d1");
		
		// Assert
		assertEquals(2, d.nbMots());
		assertEquals(2, d.mots().size());
		assertTrue(d.contient("m1"));
		assertTrue(d.contient("m2"));
		assertFalse(d.contient("m3"));
	}


	@Test (expected = NullPointerException.class )
	public void testAjouterNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.ajouter(null, "m1d2");
		
	}

	@Test (expected = Dictionnaire.DictionnaireException.class )
	public void testAjouterMotExistant() {
		// Setup
		initialiserDico1();
		
		// Act
		d.ajouter("m1", "m1d2");
		
	}

	@Test (expected = NullPointerException .class)
	public void testAjouterMotNul() {
		// Setup
		initialiserDico1();
		
		// Act
		d.ajouter(null, "m1d2");
		
	}

	@Test
	public void testRetirerMotExistant() {
		// Setup
		initialiserDico1();
		
		// Act
		boolean existait = d.retirer("m2");
		
		// Assert
		assertEquals(nbMots - 1, d.nbMots());
		assertEquals(nbMots - 1, d.mots().size());
		assertTrue(existait);
		assertTrue(d.contient("m1"));
		assertFalse(d.contient("m2"));
		assertTrue(d.contient("m3"));
		assertTrue(d.contient("m4"));
	}


	@Test
	public void testRetirerMotInexistant() {
		// Setup
		initialiserDico1();
		
		// Act
		boolean existait = d.retirer("inexistant");
		
		// Assert
		assertEquals(nbMots, d.nbMots());
		assertEquals(nbMots, d.mots().size());
		assertFalse(existait);
		assertTrue(d.contient("m1"));
		assertTrue(d.contient("m2"));
		assertTrue(d.contient("m3"));
		assertTrue(d.contient("m4"));
	}

	@Test (expected = NullPointerException.class )
	public void testRetirerNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.retirer(null);
		
	}

	@Test
	public void testAjouterMotRetire() {
		// Setup
		initialiserDico1();
		d.retirer("m2");
		
		// Act
		d.ajouter("m2", "nouveau");
		
		// Assert
		assertEquals(nbMots, d.nbMots());
		assertEquals(nbMots, d.mots().size());
		assertTrue(d.contient("m2"));
	}


	@Test
	public void testAjouterDefinition() {
		// Setup
		initialiserDico1();
		
		// Act
		d.ajouterDefinition("m2", "m2d2");
		
		// Assert
		assertEquals(nbMots, d.mots().size());
		assertEquals(2, d.definitions("m2").size());
		assertTrue(d.definitions("m2").contains("m2d2"));
	}

	@Test (expected = Dictionnaire.DictionnaireException.class)
	public void testAjouterDefinitionMotInexistant() {
		// Setup
		initialiserDico1();
		
		// Act
		d.ajouterDefinition("inexistant", "rien");
	}

	@Test (expected = NullPointerException.class )
	public void testAjouterDefinitionMotNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.ajouterDefinition(null, "m1d2");
		
	}

	@Test
	public void testRetirerDefinition() {
		// Setup
		initialiserDico1();
		d.ajouterDefinition("m2", "m2d2");
		d.ajouterDefinition("m2", "m2d3");
		
		// Act
		String defRetiree = d.retirerDefinition("m2", 1);
		
		// Assert
		assertEquals("m2d2", defRetiree);
		assertEquals(2, d.definitions("m2").size());
		assertTrue(d.definitions("m2").contains("m2d1"));
		assertFalse(d.definitions("m2").contains("m2d2"));
		assertTrue(d.definitions("m2").contains("m2d3"));
	}

	@Test (expected = Dictionnaire.DictionnaireException.class)
	public void testRetirerDefinitionMotInexistant() {
		// Setup
		initialiserDico1();
		
		// Act
		d.retirerDefinition("inexistant", 0);
	}

	@Test (expected = NullPointerException.class )
	public void testRetirerDefinitionMotNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.retirerDefinition(null, 0);
		
	}
	@Test (expected = IndexOutOfBoundsException.class)
	public void testRetirerDefinitionIndexTropPetit() {
		// Setup
		initialiserDico1();
		
		// Act
		d.retirerDefinition("m2", -1);
	}


	@Test (expected = IndexOutOfBoundsException.class)
	public void testRetirerDefinitionIndexTropGrand() {
		// Setup
		initialiserDico1();
		d.ajouterDefinition("m2", "m2d2");
		
		// Act
		d.retirerDefinition("m2", 2);
	}

	@Test
	public void testModifierDefinition() {
		// Setup
		initialiserDico1();
		d.ajouterDefinition("m2", "m2d2");
		d.ajouterDefinition("m2", "m2d3");
		
		// Act
		String ancienne = d.modifierDefinition("m2", 1, "nouvelle");
		
		// Assert
		assertEquals(3, d.definitions("m2").size());
		assertEquals("m2d2", ancienne);
		assertTrue(d.definitions("m2").contains("m2d1"));
		assertTrue(d.definitions("m2").contains("m2d3"));
		assertEquals(1, d.definitions("m2").indexOf("nouvelle"));
	}

	@Test (expected = Dictionnaire.DictionnaireException.class)
	public void testModifierDefinitionMotInexistant() {
		// Setup
		initialiserDico1();
		
		// Act
		d.modifierDefinition("inexistant", 0, "rien");
	}

	@Test (expected = NullPointerException.class )
	public void testModifierDefinitionMotNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.modifierDefinition(null, 0, "m1d2");
		
	}

	@Test (expected = IndexOutOfBoundsException.class)
	public void testModifierDefinitionIndexTropPetit() {
		// Setup
		initialiserDico1();
		
		// Act
		d.modifierDefinition("m2", -1, "nouvelle");
	}


	@Test (expected = IndexOutOfBoundsException.class)
	public void testModifierDefinitionIndexTropGrand() {
		// Setup
		initialiserDico1();
		
		// Act
		d.modifierDefinition("m2", 1, "nouvelle");
	}

	@Test 
	public void testDefinitionsMotInexistantEstNul() {
		// Setup
		initialiserDico1();
		
		// Act
		List<String> defs = d.definitions("inexistant");
		
		// Assert
		assertNull(defs);
	}

	@Test (expected = UnsupportedOperationException.class)
	public void testListeDefinitionsNonModifiable() {
		// Setup
		initialiserDico1();
		d.ajouterDefinition("m1", "m1d2");
		
		// Act
		List<String> defs = d.definitions("m1");
		defs.remove(0);
	}

	@Test
	public void testListeDefinitions() {
		// Setup
		initialiserDico1();
		d.ajouterDefinition("m1", "m1d2");
		
		// Act
		List<String> defs = d.definitions("m1");
		
		// Assert
		assertEquals("m1d1", defs.get(0));
		assertEquals("m1d2", defs.get(1));
	}

	@Test (expected = NullPointerException.class )
	public void testListeDefinitionsMotNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.definitions(null);
		
	}

	@Test 
	public void testMotsEnOrdreAlphabetique() {
		// Setup
		initialiserDico1();
		
		// Act
		List<String> mots = d.mots();
		
		// Assert
		assertEquals("m1", mots.get(0));
		assertEquals("m2", mots.get(1));
		assertEquals("m3", mots.get(2));
		assertEquals("m4", mots.get(3));
	}

	@Test 
	public void testMotsVide() {
		// Setup
		Dictionnaire d = new Dictionnaire();;
		
		// Act
		List<String> mots = d.mots();
		
		// Assert
		assertEquals(0, mots.size());
	}

	@Test (expected = NullPointerException.class )
	public void testContientNull() {
		// Setup
		initialiserDico1();
		
		// Act
		d.contient(null);
		
	}

}
