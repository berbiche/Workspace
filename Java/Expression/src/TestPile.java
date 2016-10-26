import java.util.*;
import static org.junit.Assert.*;
import org.junit.Before;
import org.junit.Test;


public class TestPile {
	
	Pile<Integer> p;

	@Before
	public void setUp() throws Exception {
		p = new Pile<Integer>();
	}
	
	@Test 
	public void testPile() {
		// Assert
		assertEquals(0, p.size());
	}

	@Test
	public void testPeekNouvellePileRetourneNull() {
		// Act
		Integer resultat = p.peek();
		
		// Assert
		assertNull(resultat);
	}

	@Test (expected = NoSuchElementException.class)
	public void testPopNouvellePileLanceException() {
		// Act
		p.pop();
	}

	@Test 
	public void testPush1Element() {
		// Setup
		Integer element = new Integer(5);
		
		// Act
		p.push(element);
		
		// Assert
		assertEquals(1, p.size());
		assertSame(element, p.peek());
	}

	@Test
	public void testPush2Elements() {
		// Setup
		Integer element = new Integer(6);

		// Act
		p.push(5);
		p.push(element);
		
		// Assert
		assertEquals(2, p.size());
		assertSame(element, p.peek());
	}

	@Test (expected = NullPointerException.class)
	public void testPushNullLanceException() {
		// Setup
		p.push(5);
		
		// Act
		p.push(null);
		
	}

	@Test 
	public void testPop1ElementPileDe1Element() {
		// Setup
		Integer element = new Integer(5);
		p.push(element);

		// Act
		Integer resultat = p.pop();
		
		// Assert
		assertSame(element, resultat);
		assertEquals(0, p.size());
	}

	@Test 
	public void testPop2ElementPileDe2Elements() {
		// Setup
		Integer element1 = new Integer(5);
		Integer element2 = new Integer(6);
		p.push(element1);
		p.push(element2);

		// Act
		Integer resultat2 = p.pop();
		Integer resultat1 = p.pop();
		
		// Assert
		assertSame(element1, resultat1);
		assertSame(element2, resultat2);
		assertEquals(0, p.size());
	}

	@Test 
	public void testPop2ElementPileDe3Elements() {
		// Setup
		Integer element1 = new Integer(5);
		Integer element2 = new Integer(6);
		p.push(4);
		p.push(element1);
		p.push(element2);

		// Act
		Integer resultat2 = p.pop();
		Integer resultat1 = p.pop();
		
		// Assert
		assertSame(element1, resultat1);
		assertSame(element2, resultat2);
		assertEquals(1, p.size());
	}

	@Test 
	public void testPeekApresPop() {
		// Setup
		Integer element = new Integer(5);
		p.push(4);
		p.push(element);
		p.push(6);

		// Act
		p.pop();
		Integer resultat = p.peek();
		
		// Assert
		assertSame(element, resultat);
		assertEquals(2, p.size());
	}
	
	@Test
	public void testPeekPileVideRetourneNull() {
		// Setup
		p.push(4);
		p.push(5);
		p.push(6);
		p.pop();
		p.pop();
		p.pop();

		// Act
		Integer resultat = p.peek();
		
		// Assert
		assertNull(resultat);
	}

	@Test (expected = NoSuchElementException.class)
	public void testPopPileVideLanceException() {
		// Setup
		p.push(4);
		p.push(5);
		p.push(6);
		p.pop();
		p.pop();
		p.pop();

		// Act
		p.pop();
	}


}
