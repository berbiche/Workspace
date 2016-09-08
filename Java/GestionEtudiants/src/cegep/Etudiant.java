package cegep;

public class Etudiant extends Personne {
	
	private String noDossier;

	public String getNumDossier() {
		return noDossier;
	}
	
	Etudiant(String noDossier) {
		this("", "", noDossier);
	}

	/**
	 * Constructeur par défaut d'Étudiant
	 * @param nom : Le nom de famille
	 * @param prenom : Le prénom
	 * @param noDossier : Le numéro de dossier unique
	 */
	Etudiant(String nom, String prenom, String noDossier) {
		super(nom, prenom);
		this.noDossier = noDossier;
	}
	
	@Override
	public String toString() {
		return "Etudiant [nom=" + nom + ", prenom=" + prenom + ", noDossier=" + noDossier + "]";
	}
	
}
