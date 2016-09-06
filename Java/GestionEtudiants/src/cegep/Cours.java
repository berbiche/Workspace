package cegep;

public class Cours {
	
	private String nom;
	private int numero, longueur;
	
	public String getNom() {
		return nom;
	}
	public void setNom(String nom) {
		this.nom = nom;
	}
	public int getLongueur() {
		return longueur;
	}
	public void setLongueur(int longueur) {
		this.longueur = longueur;
	}
	public int getNumero() {
		return numero;
	}
	
	public Cours(int numero) {
		this(numero, "", 0);
	}
	
	/**
	 * @param numero : Le numéro du cours
	 * @param nom : Le nom du cours
	 * @param longueur : La longueur de prestation du cours
	 */
	public Cours(int numero, String nom, int longueur) {
		super();
		this.numero = numero;
		this.nom = nom;
		this.longueur = longueur;
	}
	
}
