package ui;

import com.intellij.uiDesigner.core.GridConstraints;
import com.intellij.uiDesigner.core.GridLayoutManager;
import db.Categorie;
import db.DBManager;
import db.Produit;

import javax.swing.*;
import javax.swing.event.TableModelEvent;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableModel;
import java.awt.*;
import java.math.BigDecimal;
import java.sql.SQLException;
import java.util.ArrayList;

/**
 * Created by Nicolas on 2016-10-30.
 */
public class Lab4Frame extends JFrame {
    private JPanel panel1;
    private JTabbedPane tabbedPane1;
    private JTable tableCategorie;
    private JButton btnAddCategorie;
    private JTable tableProduit;
    private JButton btnAddProduit;
    private JTextField txtNomCategorie;
    private JTextField txtNomProduit;
    private JTextField txtNumeroCategorie;
    private JTextField txtNumeroFournisseur;
    private JTextField txtPrixUnitaire;
    private JTextField txtUniteCommande;
    private JTextField txtUniteStock;
    private JTextField txtNoCategorie;
    private ArrayList<Categorie> categories;
    private ArrayList<Produit> produits;

    public Lab4Frame() {
        initUI();
    }

    public static void main(String[] args) {
        try {
            DBManager.initialize();
        } catch (SQLException e) {
            JOptionPane.showMessageDialog(null, e.getMessage() + "\n" + e.getCause() + "\n" + e.getErrorCode());
            JOptionPane.showMessageDialog(null, "Adapter la variable CHAINE_CONNECTION pour votre DB");
            System.exit(1);
        } catch (Exception e) {
            JOptionPane.showMessageDialog(null, e.getStackTrace());
            System.exit(1);
        }
        EventQueue.invokeLater(() -> {
            Lab4Frame lab4 = new Lab4Frame();
            lab4.setVisible(true);
        });
    }

    private void initUI() {
        setTitle("Base de données IGA");
        setSize(600, 600);
        setLocationRelativeTo(null);
        setContentPane(panel1);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        btnAddCategorie.addActionListener(e -> {
            try {
                int tmp = parseInt(txtNoCategorie.getText());

                DBManager.insertCategorie(new Categorie(tmp, txtNomCategorie.getText()));

                DefaultTableModel model = (DefaultTableModel) tableCategorie.getModel();
                tableProduit.getSelectionModel().clearSelection();
                model.addRow(new Object[]{tmp, txtNomCategorie.getText()}); //Ajouter à la table

                //Pour redessiner la table
                model.fireTableRowsInserted(0, model.getRowCount() - 1);

                //Vider les textfields
                txtNoCategorie.setText(null);
                txtNomCategorie.setText(null);

            } catch (SQLException f) {
                JOptionPane.showMessageDialog(null, f.getMessage());
            } catch (Exception f) {
                JOptionPane.showMessageDialog(null, f.getMessage());
            }
        });

        btnAddProduit.addActionListener(e -> {
            try {
                int numCat = parseInt(txtNumeroCategorie.getText());
                int noFournisseur = parseInt(txtNumeroFournisseur.getText());
                int uniteCo = parseInt(txtUniteCommande.getText());
                int uniteStock = parseInt(txtUniteStock.getText());
                BigDecimal prix = new BigDecimal(txtPrixUnitaire.getText());

                int id = DBManager.insertProduit(new Produit(
                        0,
                        txtNomProduit.getText(),
                        numCat,
                        noFournisseur,
                        prix,
                        uniteCo,
                        uniteStock
                ));

                tableProduit.getSelectionModel().clearSelection();

                DefaultTableModel model = (DefaultTableModel) tableProduit.getModel();

                model.addRow(new Object[]{id, txtNomProduit.getText(), numCat, noFournisseur, prix, uniteCo, uniteStock});

                model.fireTableRowsInserted(0, model.getRowCount() - 1);

                txtNumeroFournisseur.setText(null);
                txtUniteCommande.setText(null);
                txtUniteStock.setText(null);
                txtNomProduit.setText(null);
                txtNumeroCategorie.setText(null);
                txtPrixUnitaire.setText(null);
            } catch (SQLException f) {
                JOptionPane.showMessageDialog(null, f.getMessage());
            } catch (Exception f) {
                JOptionPane.showMessageDialog(null, f.getStackTrace());
            }
        });

        loadTables();

        TableModel model = tableCategorie.getModel();
        model.addTableModelListener(e -> {
            if (e.getType() == TableModelEvent.UPDATE) {
                TableModel mm = (TableModel) e.getSource();
                try {
                    Categorie categorie = categories.get(e.getFirstRow());

                    int no_categorie = parseInt(mm.getValueAt(e.getFirstRow(), 0));
                    String nom_categorie = (String) mm.getValueAt(e.getFirstRow(), 1);

                    DBManager.updateCategorie(categorie.getNo_categorie(), new Categorie(no_categorie, nom_categorie));

                    categorie.setNo_categorie(no_categorie);
                    categorie.setNom_categorie(nom_categorie);

                } catch (SQLException f) {
                    JOptionPane.showMessageDialog(null, f.getMessage());
                } catch (ClassCastException f) {
                    JOptionPane.showMessageDialog(null, f.getMessage());
                }
            }
        });

        model = tableProduit.getModel();
        model.addTableModelListener(e -> {
            if (e.getType() == TableModelEvent.UPDATE) {
                TableModel mm = (TableModel) e.getSource();
                try {
                    Produit produit = produits.get(e.getFirstRow());

                    String nom_produit = (String) mm.getValueAt(e.getFirstRow(), 1);
                    BigDecimal prix_unitaire = (BigDecimal) mm.getValueAt(e.getFirstRow(), 4);
                    int no_categorie = parseInt(mm.getValueAt(e.getFirstRow(), 2)),
                            no_fournisseur = (int) mm.getValueAt(e.getFirstRow(), 3),
                            unites_commandees = (int) mm.getValueAt(e.getFirstRow(), 5),
                            unites_en_stock = (int) mm.getValueAt(e.getFirstRow(), 6);

                    DBManager.updateProduit(new Produit(produit.getNo_produit(), nom_produit, no_categorie,
                            no_fournisseur, prix_unitaire, unites_commandees, unites_en_stock
                    ));

                    produit.setNom_produit(nom_produit);
                    produit.setNo_categorie(no_categorie);
                    produit.setNo_fournisseur(no_fournisseur);
                    produit.setPrix_unitaire(prix_unitaire);
                    produit.setUnites_commandees(unites_commandees);
                    produit.setUnites_en_stock(unites_en_stock);

                } catch (SQLException f) {
                    JOptionPane.showMessageDialog(null, f.getMessage());
                } catch (ClassCastException f) {
                    JOptionPane.showMessageDialog(null, f.getStackTrace());
                }
            }
        });
    }

    private void loadTables() {
        //Charger la table des catégories
        DefaultTableModel model = new DefaultTableModel(0, 2);

        model.setColumnIdentifiers(new String[]{"no_categorie", "nom_categorie"});
        tableCategorie.setModel(model);
        Object row[] = new Object[2];
        categories = DBManager.getTableCategorie();
        for (Categorie c : categories) {
            row[0] = c.getNo_categorie();
            row[1] = c.getNom_categorie();
            model.addRow(row);
        }

        //Charger la table des produits
        String[] headersProduits = new String[]{
                "no_produit",
                "nom_produit",
                "no_categorie",
                "no_fournisseur",
                "prix_unitaire",
                "unites_commandees",
                "unites_en_stock"
        };
        model = new DefaultTableModel(headersProduits, 0) {
            @Override
            public boolean isCellEditable(int row, int column) {
                return column != 0;
            }
        };
        tableProduit.setModel(model);
        row = new Object[7];
        produits = DBManager.getTableProduit();
        for (Produit p : produits) {
            row[0] = p.getNo_produit();
            row[1] = p.getNom_produit();
            row[2] = p.getNo_categorie();
            row[3] = p.getNo_fournisseur();
            row[4] = p.getPrix_unitaire();
            row[5] = p.getUnites_commandees();
            row[6] = p.getUnites_en_stock();
            model.addRow(row);
        }
    }

    private static int parseInt(Object o) {
        return Integer.parseInt((String) o);
    }

    {
// GUI initializer generated by IntelliJ IDEA GUI Designer
// >>> IMPORTANT!! <<<
// DO NOT EDIT OR ADD ANY CODE HERE!
        $$$setupUI$$$();
    }

    /**
     * Method generated by IntelliJ IDEA GUI Designer
     * >>> IMPORTANT!! <<<
     * DO NOT edit this method OR call it in your code!
     *
     * @noinspection ALL
     */
    private void $$$setupUI$$$() {
        panel1 = new JPanel();
        panel1.setLayout(new GridLayoutManager(1, 1, new Insets(0, 0, 0, 0), -1, -1));
        panel1.setBackground(new Color(-1));
        tabbedPane1 = new JTabbedPane();
        panel1.add(tabbedPane1, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, new Dimension(200, 200), null, 0, false));
        final JPanel panel2 = new JPanel();
        panel2.setLayout(new GridLayoutManager(3, 1, new Insets(0, 0, 0, 0), -1, -1));
        tabbedPane1.addTab("Catégories", panel2);
        final JPanel panel3 = new JPanel();
        panel3.setLayout(new GridLayoutManager(2, 2, new Insets(0, 0, 0, 0), -1, -1));
        panel2.add(panel3, new GridConstraints(1, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        final JLabel label1 = new JLabel();
        label1.setText("Nom de la catégorie");
        panel3.add(label1, new GridConstraints(1, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtNomCategorie = new JTextField();
        panel3.add(txtNomCategorie, new GridConstraints(1, 1, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(100, -1), null, 0, false));
        final JLabel label2 = new JLabel();
        label2.setText("Numéro de la catégorie");
        panel3.add(label2, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtNoCategorie = new JTextField();
        txtNoCategorie.setText("");
        panel3.add(txtNoCategorie, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
        final JScrollPane scrollPane1 = new JScrollPane();
        panel2.add(scrollPane1, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_WANT_GROW, null, null, null, 0, false));
        tableCategorie = new JTable();
        tableCategorie.setDropMode(DropMode.USE_SELECTION);
        scrollPane1.setViewportView(tableCategorie);
        btnAddCategorie = new JButton();
        btnAddCategorie.setActionCommand("");
        btnAddCategorie.setText("Ajouter une catégorie");
        panel2.add(btnAddCategorie, new GridConstraints(2, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JPanel panel4 = new JPanel();
        panel4.setLayout(new GridLayoutManager(5, 4, new Insets(0, 0, 0, 0), -1, -1));
        tabbedPane1.addTab("Produits", panel4);
        final JPanel panel5 = new JPanel();
        panel5.setLayout(new GridLayoutManager(1, 1, new Insets(0, 0, 0, 0), -1, -1));
        panel4.add(panel5, new GridConstraints(4, 0, 1, 4, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        btnAddProduit = new JButton();
        btnAddProduit.setText("Ajouter un produit");
        panel5.add(btnAddProduit, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JScrollPane scrollPane2 = new JScrollPane();
        panel4.add(scrollPane2, new GridConstraints(0, 0, 1, 4, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_WANT_GROW, null, null, null, 0, false));
        tableProduit = new JTable();
        tableProduit.setDropMode(DropMode.USE_SELECTION);
        tableProduit.setEditingRow(0);
        tableProduit.setEnabled(true);
        tableProduit.putClientProperty("terminateEditOnFocusLost", Boolean.TRUE);
        scrollPane2.setViewportView(tableProduit);
        final JLabel label3 = new JLabel();
        label3.setText("Nom du produit");
        panel4.add(label3, new GridConstraints(1, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JLabel label4 = new JLabel();
        label4.setText("Numéro de catégorie");
        panel4.add(label4, new GridConstraints(2, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtNomProduit = new JTextField();
        panel4.add(txtNomProduit, new GridConstraints(1, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
        txtNumeroCategorie = new JTextField();
        panel4.add(txtNumeroCategorie, new GridConstraints(2, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
        txtNumeroFournisseur = new JTextField();
        panel4.add(txtNumeroFournisseur, new GridConstraints(1, 3, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
        final JLabel label5 = new JLabel();
        label5.setText("Numéro du fournisseur");
        panel4.add(label5, new GridConstraints(1, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JLabel label6 = new JLabel();
        label6.setText("Prix unitaire");
        panel4.add(label6, new GridConstraints(2, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtPrixUnitaire = new JTextField();
        panel4.add(txtPrixUnitaire, new GridConstraints(2, 3, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
        final JLabel label7 = new JLabel();
        label7.setText("Unités commandées");
        panel4.add(label7, new GridConstraints(3, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtUniteCommande = new JTextField();
        panel4.add(txtUniteCommande, new GridConstraints(3, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
        final JLabel label8 = new JLabel();
        label8.setText("Unités en stock");
        panel4.add(label8, new GridConstraints(3, 2, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        txtUniteStock = new JTextField();
        panel4.add(txtUniteStock, new GridConstraints(3, 3, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(150, -1), null, 0, false));
    }

    /**
     * @noinspection ALL
     */
    public JComponent $$$getRootComponent$$$() {
        return panel1;
    }
}

