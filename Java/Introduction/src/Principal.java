import java.io.BufferedReader;
import java.io.InputStreamReader;

public class Principal {

	public static void main(String[] args) {
		triTableauV1();
	}

	/**
	 * Tri d'un tableau initialis� de fa�on statique.
	 */
	public static void triTableauV1() {

		int[] tableau;
		try {
			tableau = lireTableau();
			
			Collection.triBulleV2(tableau);
			
			printTable(tableau);
			
			System.out.println("Nombre � chercher: ");
			
			BufferedReader entre = new BufferedReader(new InputStreamReader(System.in));
			
			int nombreRecherche = Integer.parseInt(entre.readLine());
			System.out.println("Index: "+rechercheDichotomique(tableau, nombreRecherche));
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public static void printTable(int[] tableau) {
		for (int i = 0; i < tableau.length; i++)
			System.out.println(tableau[i]);
	}
	
	/**
	 * Cr�� un tableau de taille variable via user input
	 * @return un tableau d'entier
	 * @throws Exception
	 */
	public static int[] lireTableau() throws Exception {
		System.out.println("Combien de nombre?");
		int[] tableau = new int[0];
		try {
			BufferedReader entre = new BufferedReader(new InputStreamReader(System.in));
			int taille = Integer.parseInt(entre.readLine());
			tableau = new int[taille];
			for (int i = 0; i < tableau.length; i++) {
				System.out.println("Entre un nombre :");
				tableau[i] = Integer.parseInt(entre.readLine());
			}
			
		} catch (Exception e) {
			e.printStackTrace();
		}
		return tableau;
	}
	
	/**
	 * 
	 * @param tableau
	 * @param nombre
	 * @return index du premier nombre
	 */
	public static int rechercheDichotomique(int[] tableau, int nombre) {
		int debut = 0, fin = tableau.length - 1, milieu = 0;
		while (debut <= fin) {
			milieu = (debut + fin) / 2;
			
			if (nombre == tableau[milieu])
				return milieu;
			
			else if (nombre > tableau[milieu])
				debut = milieu + 1;
			
			else
				fin = milieu - 1;
		}
		return -1;
	}
	
	public static int insert(int[] tableau, int nombre) {
		return 1;
	}
}
