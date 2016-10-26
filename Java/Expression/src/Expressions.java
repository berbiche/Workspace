import java.io.BufferedReader;

public class Expressions {
	
	static double calculer(double d1, double d2, String c) {
		switch(c) {
			case "-": return d2 - d1;
			case "+": return d2 + d1;
			case "*": return d2 * d1;
			case "/": return d2 / d1;
			case "^": return Math.pow(d2, d1);
			default: return 0;
		}
	}
	
	static int priorite(String s) {
		switch(s) {
			case "-":
			case "+":
				return 1;
			case "/":
			case "*":
				return 2;
			case "^":
				return 3;
			default:
				return 0;
		}
	}

	public static double evaluerEAPP(BufferedReader entree) {
		Pile<Double> operandes = new Pile<Double>();
		Pile<String> operateurs = new Pile<String>();
		
		String s = "";
		try {
			s = entree.readLine();
		} catch (Exception e) {
			e.printStackTrace();
		}
		while(!s.equals("=")) {
			switch (s) {
				case "-":
				case "+":
				case "*":
				case "/":
				case "^":
					operateurs.push(s);
					break;
				case "(":
					break;
				case ")":
					operandes.push(calculer(operandes.pop(), operandes.pop(), operateurs.pop()));
					break;
				default:
					operandes.push(Double.parseDouble(s));
					break;
			}
			try {
				s = entree.readLine();
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		if (operandes.size() == 1)
			return operandes.pop();
		else
			return calculer(operandes.pop(), operandes.pop(), operateurs.pop());
	}

	public static double evaluerEASPAPO(BufferedReader entree) {
		Pile<Double> operandes = new Pile<Double>();
		Pile<String> operateurs = new Pile<String>();
		
		String s = "";
		try {
			s = entree.readLine();
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		return 0;
	}

	public static double evaluerEAAPEPO(BufferedReader entree) {
		return 0;
	}

}
