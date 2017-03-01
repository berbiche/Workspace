package application;
import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;

import dictionnaire.ArbreBinaire;

public class TestsArbreBinaire {
	
	ArbreBinaire<String, Etudiant> a = new ArbreBinaire<>();
	Etudiant e20 = new Etudiant("20", "n20", "p20");
	Etudiant e10 = new Etudiant("10", "n10", "p10");
	Etudiant e15 = new Etudiant("15", "n15", "p15");
	Etudiant e5 = new Etudiant("05", "n5", "p5");
	Etudiant e30 = new Etudiant("30", "n30", "p30");
	
	@Before
	public void setUp() throws Exception {
		a = new ArbreBinaire<>();
	}

	private void initArbre1() {
		a.put("20", e20);
		a.put("10", e10);
		a.put("15", e15);
		a.put("05", e5);
		a.put("30", e30);
	}

	
	@Test
	public void testArbreVide() {
		assertFalse( a.containsKey("01"));
		assertNull(a.get("01"));
		assertNull(a.remove("01"));
		assertEquals(0, a.size());
	}
	
	@Test
	public void testPut1Element() {
		// Setup
		Etudiant e = new Etudiant("01", "aa", "a");
		
		// Act
		Etudiant r = a.put("01", e);
		
		// Assert
		assertNull(r);
		assertTrue(a.containsKey("01"));
		assertSame(e, a.get("01"));
		assertEquals(1, a.size());
		
	}

	@Test
	public void testRemove1Element() {
		// Setup
		Etudiant e1 = new Etudiant("01", "aa", "a");
		a.put("01", e1);
		Etudiant e2 = new Etudiant("nouveau");
		
		// Act
		Etudiant r = a.remove("01");
		a.put("10", e2);
		
		// Assert
		assertFalse(a.containsKey("01"));
		assertNull(a.get("01"));
		assertSame(e1, r);
		assertSame(e2, a.get("10"));
		assertEquals(1, a.size());
		
	}

	@Test (expected = NullPointerException.class )
	public void testPutElementNull() {
		// Setup
		initArbre1();
		
		// Act;
		a.put(null, new Etudiant("02"));
	}

	@Test 
	public void testPutRacineExistante() {
		// Setup
		initArbre1();
		
		// Act;
		Etudiant r = a.put("20", new Etudiant("nouveau"));
		
		// Assert
		assertSame(e20, r);
		assertEquals("nouveau", a.get("20").getNoDossier());
		assertEquals(5, a.size());
	}
	
	@Test 
	public void testPutInterneExistant() {
		// Setup
		initArbre1();
		
		// Act;
		Etudiant r = a.put("10", new Etudiant("nouveau"));
		
		// Assert
		assertSame(e10, r);
		assertEquals("nouveau", a.get("10").getNoDossier());
		assertEquals(5, a.size());
	}
	
	@Test 
	public void testPutFeuilleExistante() {
		// Setup
		initArbre1();
		
		// Act;
		Etudiant r = a.put("15", new Etudiant("nouveau"));
		
		// Assert
		assertSame(e15, r);
		assertEquals("nouveau", a.get("15").getNoDossier());
		assertEquals(5, a.size());
	}
	
	@Test
	public void testRemoveRacine() {
		// Setup
		initArbre1();
		Etudiant e = new Etudiant("nouveau");
		
		// Act
		Etudiant r = a.remove("20");
		a.put("25", e);
		
		// Assert
		assertFalse(a.containsKey("20"));
		assertNull(a.get("20"));
		assertSame(e30, a.get("30"));
		assertEquals(5, a.size());
		assertSame(e20, r);
		assertSame(e, a.get("25"));
	}

	@Test
	public void testRemoveInterne() {
		// Setup
		initArbre1();
		Etudiant e1 = new Etudiant("02");
		Etudiant e2 = new Etudiant("17");
		
		
		// Act
		Etudiant r = a.remove("10");
		a.put("02", e1);
		a.put("17", e2);
		
		// Assert
		assertFalse(a.containsKey("10"));
		assertNull(a.get("10"));
		assertSame(e15, a.get("15"));
		assertEquals(6, a.size());
		assertSame(e10, r);
		assertSame(e1, a.get("02"));
		assertSame(e2, a.get("17"));
	}

	@Test
	public void testRemoveInexistant() {
		// Setup
		initArbre1();
		
		// Act
		Etudiant r = a.remove("12");
		
		// Assert
		assertNull(r);
		assertEquals(5, a.size());
	}


	@Test
	public void testRemoveFeuille() {
		// Setup
		initArbre1();
		
		// Act
		Etudiant r = a.remove("15");
		Etudiant e1 = new Etudiant("07");
		Etudiant e2 = new Etudiant("14");
		a.put("07",e1);
		a.put("14", e2);
		
		// Assert
		assertFalse(a.containsKey("15"));
		assertNull(a.get("15"));
		assertSame(e1, a.get("07"));
		assertSame(e2, a.get("14"));
		assertEquals(6, a.size());
		assertSame(e15, r);
	}

	@Test
	public void testPutRacineSupprimee() {
		// Setup
		initArbre1();
		a.remove("20");
		Etudiant e = new Etudiant("nouveau");
		
		// Act
		Etudiant r = a.put("20", e);
		
		// Assert
		assertTrue(a.containsKey("20"));
		assertNull(r);
		assertSame(e, a.get("20"));
		assertSame(e10, a.get("10"));
		assertSame(e30, a.get("30"));
		assertEquals(5, a.size());
	}

	@Test
	public void testPutInterneSupprime() {
		// Setup
		initArbre1();
		a.remove("10");
		Etudiant e = new Etudiant("nouveau");
		
		// Act
		Etudiant r = a.put("10", e);
		
		// Assert
		assertTrue(a.containsKey("10"));
		assertNull(r);
		assertSame(e, a.get("10"));
		assertSame(e5, a.get("05"));
		assertSame(e15, a.get("15"));
		assertEquals(5, a.size());
	}

	@Test
	public void testPutFeuilleSupprimee() {
		// Setup
		initArbre1();
		a.remove("05");
		Etudiant e = new Etudiant("nouveau");
		
		// Act
		Etudiant r = a.put("05", e);
		
		// Assert
		assertTrue(a.containsKey("05"));
		assertNull(r);
		assertSame(e, a.get("05"));
		assertEquals(5, a.size());
	}

	@Test
	public void testRemovePasDeFilsGauche() {
		// Setup
		initArbre1();
		a.remove("05");
		a.put("12", new Etudiant("12"));
		a.put("19", new Etudiant("19"));
		
		// Act
		Etudiant r = a.remove("10");
		
		// Assert
		assertFalse(a.containsKey("10"));
		assertNull(a.get("10"));
		assertTrue(a.containsKey("15"));
		assertTrue(a.containsKey("12"));
		assertTrue(a.containsKey("19"));
		assertEquals(5, a.size());
		assertSame(e10, r);
	}

	@Test
	public void testRemovePasDeFilsDroit() {
		// Setup
		initArbre1();
		a.remove("15");
		a.put("01", new Etudiant("01"));
		a.put("07", new Etudiant("07"));
		
		// Act
		Etudiant r = a.remove("10");
		
		// Assert
		assertFalse(a.containsKey("10"));
		assertNull(a.get("10"));
		assertTrue(a.containsKey("01"));
		assertTrue(a.containsKey("05"));
		assertTrue(a.containsKey("07"));
		assertEquals(5, a.size());
		assertSame(e10, r);
	}

	@Test
	public void testRemoveAvecDeuxFilsPGDAUnFils() {
		// Setup
		initArbre1();
		a.put("01", new Etudiant("01"));
		a.put("07", new Etudiant("07"));
		a.put("06", new Etudiant("06"));
		a.put("12", new Etudiant("12"));
		a.put("14", new Etudiant("14"));
		a.put("19", new Etudiant("19"));
		
		// Act
		Etudiant r = a.remove("10");
		
		// Assert
		assertFalse(a.containsKey("10"));
		assertNull(a.get("10"));
		assertTrue(a.containsKey("01"));
		assertTrue(a.containsKey("05"));
		assertTrue(a.containsKey("06"));
		assertTrue(a.containsKey("07"));
		assertTrue(a.containsKey("12"));
		assertTrue(a.containsKey("14"));
		assertTrue(a.containsKey("15"));
		assertTrue(a.containsKey("19"));
		assertEquals(10, a.size());
		assertSame(e10, r);
	}

	@Test
	public void testRemoveAvecDeuxFilsFeuilles() {
		// Setup
		initArbre1();
		Etudiant e4 = new Etudiant("04");
		Etudiant e16 = new Etudiant("16");
		
		// Act
		Etudiant r = a.remove("10");
		a.put("04", e4);
		a.put("16", e16);
		
		// Assert
		assertFalse(a.containsKey("10"));
		assertNull(a.get("10"));
		assertTrue(a.containsKey("05"));
		assertTrue(a.containsKey("15"));
		assertTrue(a.containsKey("20"));
		assertTrue(a.containsKey("30"));
		assertEquals(6, a.size());
		assertSame(e10, r);
		
	}
	
	@Test
	public void testRemoveAvecDeuxFils() {
		// Setup
		initArbre1();
		a.put("01", new Etudiant("01"));
		a.put("07", new Etudiant("07"));
		a.put("12", new Etudiant("12"));
		a.put("19", new Etudiant("19"));
		Etudiant e6 = new Etudiant("06");
		Etudiant e11 = new Etudiant("11");
		
		// Act
		Etudiant r = a.remove("10");
		a.put("06", e6);
		a.put("11", e11);
		
		// Assert
		assertFalse(a.containsKey("10"));
		assertNull(a.get("10"));
		assertTrue(a.containsKey("01"));
		assertTrue(a.containsKey("05"));
		assertTrue(a.containsKey("07"));
		assertTrue(a.containsKey("15"));
		assertTrue(a.containsKey("12"));
		assertTrue(a.containsKey("19"));
		assertTrue(a.containsKey("20"));
		assertTrue(a.containsKey("30"));
		assertEquals(10, a.size());
		assertSame(e10, r);
		assertSame(e6, a.get("06"));
		assertSame(e11, a.get("11"));
	}

	@Test
	public void testIterateur() {
		// Setup
		initArbre1();
		a.put("01", new Etudiant("01"));
		a.put("07", new Etudiant("07"));
		a.put("12", new Etudiant("12"));
		a.put("19", new Etudiant("19"));
		
		// Assert
		int nbElements = 0;
		String precedent = "0";
		for (String suivant: a) {
			assertTrue(suivant.compareTo(precedent)>0);
			precedent = suivant;
			nbElements++;
		}
		assertEquals(a.size(), nbElements);
	}
}
