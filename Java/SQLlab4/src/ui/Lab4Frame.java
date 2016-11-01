package ui;

import db.DBManager;

import javax.swing.*;
import java.awt.EventQueue;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

/**
 * Created by Nicolas on 2016-10-31.
 */
public class Lab4Frame extends JFrame {
    private JPanel panel1;
    private JTabbedPane tabbedPane1;
    private JTable tableCategorie;
    private JButton btnAddCategorie;
    private JButton btnRemoveCategorie;
    private JTable tableProduit;
    private JButton btnAddProduit;
    private JButton btnRemoveProduit;

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
        pack();

        btnAddCategorie.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                JDialog categorieDialog = new CategorieDialog();
                categorieDialog.setModal(true);
                categorieDialog.setVisible(true);
                categorieDialog.dispose();
            }
        });
        btnRemoveCategorie.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                JDialog produitDialog = new ProduitDialog();
                produitDialog.setModal(true);
                produitDialog.setVisible(true);
                produitDialog.dispose();

            }
        });

        loadTable();
    }

    private void loadTable() {
        DBManager.getTableCategorie();
    }

}
