import javax.swing.*;
import java.util.ArrayList;

/**
 * Created by Nicolas on 2016-10-27.
 */
public class GestionClient {

    private static GestionClient gestionClient;
    private static ArrayList<Client> clients;

    private JPanel jpanel;
    private JButton btnModifier;
    private JButton btnAjouter;
    private JButton btnRechercher;
    private JButton btnSupprimer;
    private JButton btnFirst;
    private JButton btnBefore;
    private JButton btnAfter;
    private JButton btnLast;
    private JTextField txtNom;
    private JTextField txtPrenom;
    private JTextField txtNumero;
    private JComboBox<Client[]> cbClients;

    public static void main(String[] args) {
        JFrame mainFrame = new JFrame("Gestion des clients");
        gestionClient = new GestionClient();
        mainFrame.setContentPane(gestionClient.jpanel);
        mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        mainFrame.pack();
        mainFrame.setVisible(true);
    }

}
