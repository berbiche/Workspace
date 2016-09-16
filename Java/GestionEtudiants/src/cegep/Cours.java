package cegep;

public class Cours {
	
	private String nom;
	private int numero, duree;
	
	public String getNom() {
		return nom;
	}
	public void setNom(String nom) {
		this.nom = nom;
	}
	public int getLongueur() {
		return duree;
	}
	public void setLongueur(int duree) {
		this.duree = duree;
	}
	public int getNumero() {
		return numero;
	}
	
	Cours(int numero) {
		this(numero, "", 0);
	}
	
	/**
	 * @param numero : Le numéro du cours
	 * @param nom : Le nom du cours
	 * @param longueur : La longueur de prestation du cours
	 */
	Cours(int numero, String nom, int duree) {
		super();
		this.numero = numero;
		this.nom = nom;
		this.duree = duree;
	}
	
}
