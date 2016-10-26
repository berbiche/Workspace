import java.sql.*;

public class Class1 {
	
	private final static String Chaine1 = "jdbc:sqlserver://localhost\\sqlexpress;database=AIRTRANSAT;user=sa;password=secret",
						 Chaine2 = "jdbc:sqlserver://sv54.cmaisonneuve.qc.ca;database=JDBCDB;user=jdbclogin;password=secret";
	
	public static void main(String[] args) {
		test1();
		//test2();
	}
	
	public static void test1() {
		// �tape 1
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			System.out.println("Le DRIVER JDBC a �t� trouv� !");
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
		
		// �tape 2
		try {
			Connection connexion = DriverManager.getConnection(Chaine2);
			System.out.println("Connexion r�ussi !");
			
			// �tape 3
			Statement CmdSql = connexion.createStatement();
			ResultSet Resultat = CmdSql.executeQuery("SELECT * FROM EMP");
			
			// �tape 4
			while (Resultat.next()) {
				System.out.println(Resultat.getString(2) + " : " + Resultat.getInt(1));
			}
			
			// �tape 5
			CmdSql.close();
			connexion.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}		
	}
	
	public static void test2() {
		// �tape 1
		try {
			Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
			System.out.println("Le DRIVER JDBC a �t� trouv� !");
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
		
		// �tape 2
		try {
			Connection c1 = DriverManager.getConnection(Chaine2);
			System.out.println("Connexion r�ussi !");
			
			// �tape 3
			Statement s1 = c1.createStatement();
			int retour = s1.executeUpdate("CREATE TABLE Dept(no_dept INT, nom_dept VARCHAR(20))");
			System.out.println(retour);
			ResultSet r1 = s1.executeQuery("SELECT matricule, nom FROM Dept");
			
			// �tape 4
			while (r1.next()) {
				System.out.println(r1.getString("nom") + " " + r1.getInt(1));
			}
			
			// �tape 5
			s1.close();
			c1.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}		
	}
}