
public class Droite {
	
	private Point a, b;
	
	public Point getA() {
		return a;
	}

	public Point getB() {
		return b;
	}
	
	public void faireSymetrieA() {
		a.faireSymetrieX();
		a.faireSymetrieY();
	}
	
	public void faireSymetrieB() {
		b.faireSymetrieX();
		b.faireSymetrieY();
	}

	public Droite() {
		this(new Point(), new Point());
	}
	
	public Droite(Point a, Point b) {
		this.a = a;
		this.b = b;
	}
	
	public double getLongueur() {
		return a.distance(b);
	}

}
