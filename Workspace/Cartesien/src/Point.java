
public class Point {
	
	private double x, y;
	
	
	/**
	 * Getter & Setter
	 */
	public double getX() {
		return x;
	}
	
	public void setX(double x) {
		this.x = x;
	}

	public double getY() {
		return y;
	}

	public void setY(double y) {
		this.y = y;
	}
	
	public void faireSymetrieX() {
		x = -x;
	}
	
	public void faireSymetrieY() {
		y = -y;
	}
	
	/**
	 * Constructeurs par d?faut
	 */
	public Point() {
		this(0, 0);
	}
	
	/**
	 * Constructeur avec un point
	 * @param P : Instance de Point
	 */
	public Point(Point p) {
		this(p.x, p.y);
	}
	
	/**
	 * Constructeur avec des coordonnées
	 * @param x : Coordonnée x
	 * @param y : Coordonnée y
	 */
	public Point(double x, double y) {
		this.x = x;
		this.y = y;
	}
	
	/**
	 * Calcule la distance entre deux coordonnées
	 * @param p : Instance de Point
	 * @return La distance entre deux coordonnées
	 */
	public double distance(Point p) {
		return distance(p.x, p.y);
	}
	
	/**
	 * Calcule la distance entre deux coordonnées
	 * @param x : Coordonnée x
	 * @param y : Coordonnée y
	 * @return La distance entre deux coordonnées
	 */
	public double distance(double x, double y) {
		return Math.hypot(this.y - y, this.x - x);
	}
	
}
