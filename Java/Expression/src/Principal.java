import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Principal {

	public static void main(String[] args)  throws Exception {
		evaluerEPP();
	}


	static void evaluerEPP() throws Exception {
		// À partir d'un fichier
//		String chemin = new File(".").getAbsolutePath();
//		BufferedReader entree = new BufferedReader(new FileReader(chemin + "\\Exp 1.txt"));
//		System.out.println("Résultat: " + Expressions.evaluerEAPP(entree));
		
		// À partir de la console
		BufferedReader entree = new BufferedReader(new InputStreamReader(
				System.in));
		System.out.println("Entrez l'expression à évaluer");
		System.out.println("Résultat: " + Expressions.evaluerEAPP(entree));
	
		// À partir d'un String
//		String expression = "( 5 + 4 ) = ".replaceAll("(\\s)", "\n");
//		BufferedReader entree = new BufferedReader(new StringReader(expression));
//		System.out.println("Résultat: " + Expressions.evaluerEAPP(entree));
	}
	

}
