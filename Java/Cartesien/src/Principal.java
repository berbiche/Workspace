
public class Principal {

	public static void main(String[] args) {
		Point p1 = new Point(2,3);
		
		Point p2 = new Point();
		p2.setX(p1.getX());
		p2.setY(p1.getY());
		
		Point p3 = new Point(p1);
		

		System.out.println("Distance p1 = " + p1.distance(0, 0));
		
		System.out.println("p2.x = " + p2.getX() + ", p2.y = " + p2.getY());
		System.out.println("p3.x = " + p3.getX() + ", p3.y = " + p3.getY());
		
		p1.setX(4);
		p2.setX(5);
		p3.setX(6);
		
		System.out.println("p1.x = " + p1.getX() + ", p1.y = " + p1.getY());
		System.out.println("p2.x = " + p2.getX() + ", p2.y = " + p2.getY());
		System.out.println("p3.x = " + p3.getX() + ", p3.y = " + p3.getY());
		
		Droite d1 = new Droite(p1, p2);
		System.out.println("A (p1 : " + (d1.getA()).getX() + ", " + (d1.getA()).getY() + ")\nB (p2 : " + (d1.getB()).getX() + ", " + (d1.getB()).getY() + ")\nLongueur = " + d1.getLongueur());
		p1.setX(420); p1.setY(420); p2.faireSymetrieX(); p2.faireSymetrieY();
		System.out.println("A (p1 : " + (d1.getA()).getX() + ", " + (d1.getA()).getY() + ")\nB (p2 : " + (d1.getB()).getX() + ", " + (d1.getB()).getY() + ")\nLongueur = " + d1.getLongueur());
		System.out.println("A (p1 : " + (d1.getA()).getX() + ", " + (d1.getA()).getY() + ")\nB (p2 : " + (d1.getB()).getX() + ", " + (d1.getB()).getY() + ")");

		
		
	}

}
