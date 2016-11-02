package db;

import java.math.BigDecimal;

public class Produit {

    @SuppressWarnings("CanBeFinal")
    private int no_produit, no_categorie, no_fournisseur, unites_commandees, unites_en_stock;
    private BigDecimal prix_unitaire;
    private String nom_produit;

    public int getNo_produit() {
        return no_produit;
    }

    public int getNo_categorie() {
        return no_categorie;
    }

    public int getNo_fournisseur() {
        return no_fournisseur;
    }

    public int getUnites_commandees() {
        return unites_commandees;
    }

    public int getUnites_en_stock() {
        return unites_en_stock;
    }

    public BigDecimal getPrix_unitaire() {
        return prix_unitaire;
    }

    public String getNom_produit() {
        return nom_produit;
    }

    public void setNo_categorie(int no_categorie) {
        this.no_categorie = no_categorie;
    }

    public void setNo_fournisseur(int no_fournisseur) {
        this.no_fournisseur = no_fournisseur;
    }

    public void setUnites_commandees(int unites_commandees) {
        this.unites_commandees = unites_commandees;
    }

    public void setUnites_en_stock(int unites_en_stock) {
        this.unites_en_stock = unites_en_stock;
    }

    public void setPrix_unitaire(BigDecimal prix_unitaire) {
        this.prix_unitaire = prix_unitaire;
    }

    public void setNom_produit(String nom_produit) {
        this.nom_produit = nom_produit;
    }

    public Produit(int no_produit, String nom_produit, int no_categorie, int no_fournisseur, BigDecimal prix_unitaire, int unites_commandees, int unites_en_stock) {
        this.no_produit = no_produit;
        this.nom_produit = nom_produit;
        this.no_categorie = no_categorie;
        this.no_fournisseur = no_fournisseur;
        this.prix_unitaire = prix_unitaire;
        this.unites_commandees = unites_commandees;
        this.unites_en_stock = unites_en_stock;
    }

    @Override
    public String toString() {
        return "Produit{" +
                "no_produit=" + no_produit +
                ", no_categorie=" + no_categorie +
                ", no_fournisseur=" + no_fournisseur +
                ", unites_commandees=" + unites_commandees +
                ", unites_en_stock=" + unites_en_stock +
                ", prix_unitaire=" + prix_unitaire +
                ", nom_produit='" + nom_produit + '\'' +
                '}';
    }

    @Override
    public boolean equals(Object obj) {
        return obj instanceof Produit && ((Produit) obj).no_produit == no_produit;
    }
}
