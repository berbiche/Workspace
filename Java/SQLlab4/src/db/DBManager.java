package db;

import java.sql.*;
import java.util.ArrayList;

public final class DBManager {

	private static final String CHAINE_CONNECTION       = "jdbc:sqlserver://localhost;database=IGA;user=lab4;password=secret",
								CREATE_TABLE_CATEGORIES = "CREATE TABLE Categories"
                                                        + "(no_categorie integer Primary Key, nom_categorie varchar(30))",
								CREATE_TABLE_PRODUITS   = "CREATE TABLE Produits"
                                                        + "(no_produit integer identity(1,1) Primary Key, nom_produit "
                                                        + "varchar(40), no_categorie integer Foreign Key references Categories(no_categorie)"
                                                        + "ON UPDATE CASCADE ON DELETE SET NULL,"
                                                        + "no_fournisseur integer, prix_unitaire money, unites_commandees integer,"
                                                        + "unites_en_stock integer)",
                                INSERT_TABLE_CATEGORIES = "INSERT INTO Categories(no_categorie,nom_categorie) VALUES (?,?)",
                                INSERT_TABLE_PRODUITS   = "INSERT INTO Produits(nom_produit,no_categorie,no_fournisseur,"
                                                        + "prix_unitaire,unites_commandees,unites_en_stock) VALUES (?,?,?,?,?,?)",
                                UPDATE_TABLE_CATEGORIES = "UPDATE Categories SET no_categorie=?,nom_categorie=? WHERE no_categorie=?",
                                UPDATE_TABLE_PRODUITS   = "UPDATE Produits SET nom_produit=?,no_categorie=?,no_fournisseur=?,"
                                                        + "prix_unitaire=?,unites_commandees=?,unites_en_stock=? WHERE no_produit=?";

    private static final DBManager self = new DBManager();
    private static Connection connexion;
    private static ArrayList<Categorie> tableCategorie;
    private static ArrayList<Produit> tableProduit;

    @SuppressWarnings("unused")
    public static DBManager getInstance() {
        return self;
    }
	
	private DBManager() {
        tableCategorie = new ArrayList<>();
        tableProduit = new ArrayList<>();
    }

    private static Connection getConnection() throws SQLException {
        return DriverManager.getConnection(CHAINE_CONNECTION);
    }

	public static void initialize() throws SQLException {
        try {
            connexion = getConnection();
            //Créer la table catégorie
            try (Statement st = connexion.createStatement()) {
                st.executeUpdate(CREATE_TABLE_CATEGORIES);
            } catch (SQLException e) {
                //La table existe déjà
            }

            try (Statement st = connexion.createStatement()) {
                st.executeUpdate(CREATE_TABLE_PRODUITS);
            } catch (SQLException e) {
                //La table existe déjà
            }
        } finally {
            connexion.close();
        }
    }

    public static ArrayList<Categorie> getTableCategorie() {
        //Aller chercher la table Categorie
        try {
            connexion = getConnection();
            Statement st = connexion.createStatement();
            ResultSet tmp = st.executeQuery("Select * From Categories");
            while (tmp.next()) {
                tableCategorie.add(new Categorie(tmp.getInt(1), tmp.getString(2)));
            }
            tmp.close();
            st.close();
            return tableCategorie;
        } catch (SQLException e) {
            return new ArrayList<>();
        }
    }

    public static ArrayList<Produit> getTableProduit() {
        try {
            Statement st = connexion.createStatement();
            ResultSet tmp = st.executeQuery("SELECT * FROM Produits");
            while (tmp.next()) {
                tableProduit.add(new Produit(
                        tmp.getInt(1),
                        tmp.getString(2),
                        tmp.getInt(3),
                        tmp.getInt(4),
                        tmp.getBigDecimal(5),
                        tmp.getInt(6),
                        tmp.getInt(7)
                ));
            }
            tmp.close();
            st.close();
            return tableProduit;
        } catch (SQLException e) {
            return new ArrayList<>();
        }
    }

    public static void insertCategorie(Categorie categorie) throws SQLException {
        try {
            connexion = getConnection();
            try (PreparedStatement ps = connexion.prepareStatement(INSERT_TABLE_CATEGORIES)) {
                ps.setInt(1, categorie.getNo_categorie());
                ps.setString(2, categorie.getNom_categorie());
                ps.executeUpdate();
            }
        } finally {
            connexion.close();
        }
    }

    public static int insertProduit(Produit produit) throws SQLException {
        try {
            connexion = getConnection();
            try (PreparedStatement ps = connexion.prepareStatement(INSERT_TABLE_PRODUITS, Statement.RETURN_GENERATED_KEYS)) {
                ps.setString(1, produit.getNom_produit());
                ps.setInt(2, produit.getNo_categorie());
                ps.setInt(3, produit.getNo_fournisseur());
                ps.setBigDecimal(4, produit.getPrix_unitaire());
                ps.setInt(5, produit.getUnites_commandees());
                ps.setInt(6, produit.getUnites_en_stock());
                ps.executeUpdate();

                try (ResultSet keys = ps.getGeneratedKeys()) {
                    if (keys.next())
                        return keys.getInt(1);
                    else
                        throw new SQLException();
                }
            }
        } finally {
            connexion.close();
        }
    }

    public static void updateCategorie(int pk, Categorie categorie) throws SQLException {
        try {
            connexion = getConnection();
            try (PreparedStatement ps = connexion.prepareStatement(UPDATE_TABLE_CATEGORIES)) {
                ps.setInt(1, categorie.getNo_categorie());
                ps.setString(2, categorie.getNom_categorie());
                ps.setInt(3, pk); //WHERE no_categorie
                ps.executeUpdate();
            }
        } finally {
            connexion.close();
        }
    }

    public static void updateProduit(Produit produit) throws SQLException {
        try {
            connexion = getConnection();
            try (PreparedStatement ps = connexion.prepareStatement(UPDATE_TABLE_PRODUITS)) {
                ps.setString(1, produit.getNom_produit());
                ps.setInt(2, produit.getNo_categorie());
                ps.setInt(3, produit.getNo_fournisseur());
                ps.setBigDecimal(4, produit.getPrix_unitaire());
                ps.setInt(5, produit.getUnites_commandees());
                ps.setInt(6, produit.getUnites_en_stock());
                ps.setInt(7, produit.getNo_produit()); //WHERE no_produit
                ps.executeUpdate();
            }
        } finally {
            connexion.close();
        }
    }

}
