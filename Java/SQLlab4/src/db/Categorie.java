package db;

/**
 * Created by Nicolas on 2016-11-01.
 */
public class Categorie {

    private int no_categorie;
    private String nom_categorie;

    public int getNo_categorie() {
        return no_categorie;
    }

    public String getNom_categorie() {
        return nom_categorie;
    }

    public Categorie(int no_categorie, String nom_categorie) {
        this.no_categorie = no_categorie;
        this.nom_categorie = nom_categorie;
    }

    @Override
    public String toString() {
        return "Categorie{" +
                "no_categorie=" + no_categorie +
                ", nom_categorie='" + nom_categorie + '\'' +
                '}';
    }

}
