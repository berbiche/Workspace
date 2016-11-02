package ui;

import db.Categorie;
import db.DBManager;
import db.Produit;

import javax.swing.*;
import javax.swing.event.TableModelEvent;
import javax.swing.event.TableModelListener;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableColumn;
import javax.swing.table.TableModel;
import java.awt.EventQueue;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.InputEvent;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.math.BigDecimal;
import java.sql.SQLException;
import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.Vector;

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
                int tmp = Integer.parseInt(txtNoCategorie.getText());
                DBManager.insertCategorie(new Categorie(tmp, txtNomCategorie.getText()));
                DefaultTableModel model = (DefaultTableModel)tableCategorie.getModel();
                model.addRow(new Object[] {tmp, txtNomCategorie.getText()});
                model.fireTableDataChanged();
                txtNoCategorie.setText(null);
                txtNomCategorie.setText(null);
            } catch (SQLException f) {
                JOptionPane.showMessageDialog(null, f.getStackTrace());
            } catch (Exception g) {
                JOptionPane.showMessageDialog(null, "Données invalides");
            }
        });

        btnAddProduit.addActionListener(e -> {
            try {
                int numCat = Integer.parseInt(txtNumeroCategorie.getText());
                int noFournisseur = Integer.parseInt(txtNumeroFournisseur.getText());
                int uniteCo = Integer.parseInt(txtUniteCommande.getText());
                int uniteStock = Integer.parseInt(txtUniteStock.getText());
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
                DefaultTableModel model = (DefaultTableModel)tableProduit.getModel();
                model.addRow(new Object[] {
                        id,
                        txtNomProduit.getText(),
                        numCat,
                        noFournisseur,
                        prix,
                        uniteCo,
                        uniteStock
                });
                model.fireTableDataChanged();
                txtNumeroFournisseur.setText(null);
                txtUniteCommande.setText(null);
                txtUniteStock.setText(null);
                txtNomProduit.setText(null);
                txtNumeroCategorie.setText(null);
                txtPrixUnitaire.setText(null);
            } catch (SQLException f) {
                JOptionPane.showMessageDialog(null, f.getStackTrace());
            } catch (Exception g) {
                JOptionPane.showMessageDialog(null, "Données invalides");
            }
        });

        loadTables();

        TableModel model = (TableModel) tableCategorie.getModel();
        model.addTableModelListener(e -> {
            TableModel mm = (TableModel) e.getSource();
            Object no_categorie  = mm.getValueAt(e.getFirstRow(), 0),
                   nom_categorie = mm.getValueAt(e.getFirstRow(), 1);
            categories.get(e.getFirstRow());
            System.out.println();
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
}

