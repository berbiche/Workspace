
public class Collection {
	
//	triBulle non optimisé
//	@param tableau
	public static void triBulle(int[] tableau) {
		for (int i = 0; i < tableau.length - 1; i++) {
			for (int j = 0; j < tableau.length - i - 1; j++) {
				if (tableau[j] > tableau[j+1]) {
					int tmp = tableau[j];
					tableau[j] = tableau[j+1];
					tableau[j+1] = tmp;
				}
			}
		}
	}
	
//	triBulle optimisé v1
//	@param tableau
	public static void triBulleV2(int[] tableau) {
		for (int i = 0; i < tableau.length - 1; i++) {
			boolean change = false;
			for (int j = 0; j < tableau.length - i - 1; j++) {
				if (tableau[j] > tableau[j+1]) {
					int tmp = tableau[j];
					tableau[j] = tableau[j+1];
					tableau[j+1] = tmp;
					change = true;
				}
			}
			if (!change) {
				System.out.println("Breaking");
				break;
			}
		}
	}
}
