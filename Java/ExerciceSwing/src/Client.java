import java.util.*;

public class Client {

	private String noClient;
	private String nomFamille;
	private String prenom;

	public Client(String noClient) {
		this(noClient, "", "");
	}

	public Client(String noClient, String nomFamille, String prenom) {
		this.noClient = noClient;
		this.nomFamille = nomFamille;
		this.prenom = prenom;
	}

	public String getNoClient() {
		return noClient;
	}

	public String getNomFamille() {
		return nomFamille;
	}

	public void setNomFamille(String nomFamille) {
		this.nomFamille = nomFamille;
	}

	public String getPrenom() {
		return prenom;
	}

	public void setPrenom(String prenom) {
		this.prenom = prenom;
	}

	public String toString() {
		return nomFamille + ", " + prenom;
	}

	public static ArrayList<Client> initialiserClients() {
		ArrayList<Client> clients = new ArrayList<Client>();
		clients.add(new Client("1111", "Tremblay", "Bob"));
		clients.add(new Client("2222", "Tellier", "Julie"));
		clients.add(new Client("3333", "Girard", "Joe"));
		clients.add(new Client("4444", "Smith", "Sam"));
		clients.add(new Client("5555", "Tremblay", "Sarah"));
		
		return clients;
		
	}

}
