package ui;

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
 * Created by Nicolas on 2016-10-31.
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
            JOptionPane.showMessageDialog(null, e.getMessage()+"\n"+e.getCause()+"\n"+e.getErrorCode());
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
                model.addRow(new Object[] {tmp, txtNomCategorie.getText()}); //Ajouter à la table

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

                model.addRow(new Object[] {id, txtNomProduit.getText(), numCat, noFournisseur, prix, uniteCo, uniteStock});

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

        model.setColumnIdentifiers(new String[] {"no_categorie", "nom_categorie"});
        tableCategorie.setModel(model);
        Object row[] = new Object[2];
        categories = DBManager.getTableCategorie();
        for (Categorie c: categories) {
            row[0] = c.getNo_categorie();
            row[1] = c.getNom_categorie();
            model.addRow(row);
        }

        //Charger la table des produits
        String[] headersProduits = new String[] {
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
        for (Produit p: produits) {
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
}

