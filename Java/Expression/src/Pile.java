import java.util.ArrayList;

public class Pile<E> {
	private ArrayList<E> elements;

	public Pile() {
		elements = new ArrayList<E>();
	}
	
	public int size() {
		return elements.size();
	}

	public void push(E nouvElem) {
		elements.add(nouvElem);
	}
	
	public E peek() {
		return elements.get(elements.size() - 1);
	}

	public E pop ( ) {
		E tmp = elements.get(elements.size() - 1);
		elements.remove(elements.size() - 1);
		return tmp;
	}
}
