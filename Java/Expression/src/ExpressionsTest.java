
import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;
import java.io.BufferedReader;
import java.io.StringReader;

public class ExpressionsTest {

	private double delta = 0.0000001;
	
	@Before
	public void setUp() throws Exception {
	}

	@Test
	public void test01() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("(5+4)=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAPP(entree);
		
		// Assert
		assertEquals((double)9, resultat, delta);
	}

	@Test
	public void test02() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("(5*(4+3))=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAPP(entree);
		
		// Assert
		assertEquals((double)35, resultat, delta);
	}

	@Test
	public void test03() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("(5*(4^((6-3))+1))=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAPP(entree);
		
		// Assert
		assertEquals((double)325, resultat, delta);
	}

	
	@Test
	public void test04() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("5+4=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAPP(entree);
		
		// Assert
		assertEquals((double)9, resultat, delta);
	}

	@Test
	public void test05() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("5*(4+3)=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAPP(entree);
		
		// Assert
		assertEquals((double)35, resultat, delta);
	}

	@Test
	public void test06() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("5*(4^((6-3))+1)=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAPP(entree);
		
		// Assert
		assertEquals((double)325, resultat, delta);
	}

	@Test
	public void test07() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("5+4*3=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEASPAPO(entree);
		
		// Assert
		assertEquals((double)17, resultat, delta);
	}

	@Test
	public void test08() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("5*4+3=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEASPAPO(entree);
		
		// Assert
		assertEquals((double)23, resultat, delta);
	}

	@Test
	public void test09() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("2*5^2-8=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEASPAPO(entree);
		
		// Assert
		assertEquals((double)42, resultat, delta);
	}

	@Test
	public void test10() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("1-2*5^2+8=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEASPAPO(entree);
		
		// Assert
		assertEquals((double)-41, resultat, delta);
	}

	@Test
	public void test11() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("(1-2)*5^2+8=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAAPEPO(entree);
		
		// Assert
		assertEquals((double)-17, resultat, delta);
	}

	@Test
	public void test12() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("((1-2)*5)^2+8=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAAPEPO(entree);
		
		// Assert
		assertEquals((double)33, resultat, delta);
	}

	@Test
	public void test13() {
		// Setup
		BufferedReader entree = new BufferedReader(new StringReader("((1-2)*5)^(2+(8-6))=".replaceAll("(.)", "$1\n")));

		// Act
		double resultat = Expressions.evaluerEAAPEPO(entree);
		
		// Assert
		assertEquals((double)625, resultat, delta);
	}


}
