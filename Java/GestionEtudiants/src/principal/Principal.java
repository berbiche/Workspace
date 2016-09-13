package principal;
import java.util.ArrayList;
import cegep.*;

public class Principal {
	private static ArrayList<Etudiant> listeEtudiant = new ArrayList<Etudiant>();

	public static void main(String[] args) {
		Cegep monCegep = new Cegep();
		Etudiant e1 = monCegep.ajoutEtudiant("1422222");
		monCegep.ajouterBenevole(e1);
		listeEtudiant.add(e1);
	}
	
}
