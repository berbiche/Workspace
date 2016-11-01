package db;

import java.sql.*;

public final class DBManager {

	private static final String CHAINE_CONNECTION       = "jdbc:sqlserver://localhost:1433;database=IGA;user=lab4;password=secret",
								CREATE_TABLE_CATEGORIES = "CREATE TABLE Categories"
                                                        + "(no_categorie integer Primary Key, nom_categorie varchar(30))",
								CREATE_TABLE_PRODUITS   = "CREATE TABLE Produits"
                                                        + "(no_produit integer identity(1,1) Primary Key, nom_produit "
                                                        + "varchar(40), no_categorie integer Foreign Key references Categories(no_categorie),"
                                                        + "no_fournisseur integer not null, prix_unitaire money, unites_commandees integer,"
                                                        + "unites_en_stock integer)";

    private static DBManager self = new DBManager();
    private static Connection connexion;
    private static ResultSet tableCategorie, tableProduit;

    public static ResultSet getTableCategorie() {
        return tableCategorie;
    }

    public static ResultSet getTableProduit() {
        return tableProduit;
    }

    public static DBManager getInstance() {
        return self;
    }
	
	private DBManager() {}

	public static void initialize() throws SQLException {

        try {
            //Test si les tables existent
            connexion = DriverManager.getConnection(CHAINE_CONNECTION);
        } catch (SQLException e) {
            throw e;
        }

        try (Statement st = connexion.createStatement()){
            st.executeUpdate(CREATE_TABLE_CATEGORIES);
            st.executeUpdate(CREATE_TABLE_PRODUITS);
        } finally {
            tableCategorie = connexion.createStatement().executeQuery("Select * From Categoris");
            tableProduit = connexion.createStatement().executeQuery("Select * From Produits");
        }


        connexion.close();
    }

}
