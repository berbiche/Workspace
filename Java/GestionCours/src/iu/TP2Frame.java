package iu;

import com.intellij.uiDesigner.core.GridConstraints;
import com.intellij.uiDesigner.core.GridLayoutManager;

import javax.swing.*;
import javax.swing.border.TitledBorder;
import javax.swing.event.TableModelEvent;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableModel;
import javax.swing.table.TableRowSorter;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.util.ArrayList;

import cegep.*;
import com.intellij.uiDesigner.core.Spacer;
import undo.UndoManager;

enum Session {
    A14,
    H14,
    A15,
    H15,
    A16,
    H16
}

public class TP2Frame extends JFrame {

    private Cegep cegep;
    private JPanel panel1;
    private JComboBox cboSession;
    private JComboBox cboCoursGroupe;
    private JTable tblEtudiants;
    private JList listEtudiants;
    private JButton btnAjouterEtudiant;
    private JButton btnRetirerEtudiant;
    private JButton btnUndo;
    private JButton btnRedo;

    public TP2Frame() {
        cegep = initialiserCegep();
        initUI();
    }

    public static void main(String[] args) {
        EventQueue.invokeLater(() -> {
            TP2Frame gestionCours = new TP2Frame();
            gestionCours.setVisible(true);
        });
    }

    private Cegep initialiserCegep() {
        Etudiant mt, jt, ms, cm, na, jb, tb;

        Cegep c = new Cegep();
        mt = c.admettreEtudiant("Tremblay", "Mario");
        jt = c.admettreEtudiant("Tremblay", "Jean");
        ms = c.admettreEtudiant("Séguin", "Mario");
        cm = c.admettreEtudiant("Marin", "Carl");
        na = c.admettreEtudiant("Allard", "Nathalie");
        jb = c.admettreEtudiant("Bédard", "Julie");
        tb = c.admettreEtudiant("Beaudoin", "Tania");
        Professeur bj = c.embaucherProfesseur("111222333", "Jones", "Bob");
        Professeur js = c.embaucherProfesseur("444555666", "Smith", "Joe");
        Cours prog1 = c.ajouterCours("420-D11", "Prog 1", 5);
        Cours prog2 = c.ajouterCours("420-D12", "Prog 2", 6);
        Cours po = c.ajouterCours("420-D61", "PO", 7);
        CoursGroupe a13prog1g1 = c.ajouterCoursGroupe(prog1, 1, "A13");
        a13prog1g1.setProfesseur(bj);
        a13prog1g1.setNoLocal("D3737");
        CoursGroupe a13prog1g2 = c.ajouterCoursGroupe(prog1, 2, "A13");
        a13prog1g2.setProfesseur(js);
        a13prog1g2.setNoLocal("D3741");
        CoursGroupe h14prog2g1 = c.ajouterCoursGroupe(prog2, 1, "H14");
        h14prog2g1.setProfesseur(bj);
        h14prog2g1.setNoLocal("D3737");
        CoursGroupe a14pog1 = c.ajouterCoursGroupe(po, 1, "A14");
        a14pog1.setProfesseur(js);
        a14pog1.setNoLocal("D3741");
        CoursGroupe a14prog1g1 = c.ajouterCoursGroupe(prog1, 1, "A14");
        a14prog1g1.setProfesseur(bj);
        a14prog1g1.setNoLocal("D3737");
        CoursGroupe a14prog1g2 = c.ajouterCoursGroupe(prog1, 2, "A14");
        a14prog1g2.setProfesseur(bj);
        a14prog1g2.setNoLocal("D3741");

        a13prog1g1.inscrireEtudiant(jt);
        a13prog1g1.inscrireEtudiant(mt);
        a13prog1g2.inscrireEtudiant(ms);
        a13prog1g2.inscrireEtudiant(cm);
        h14prog2g1.inscrireEtudiant(mt);
        h14prog2g1.inscrireEtudiant(jt);
        h14prog2g1.inscrireEtudiant(ms);
        a14pog1.inscrireEtudiant(mt);
        a14pog1.inscrireEtudiant(jt);
        a14pog1.inscrireEtudiant(ms);
        a14prog1g1.inscrireEtudiant(cm);
        a14prog1g1.inscrireEtudiant(na);
        a14prog1g2.inscrireEtudiant(jb);
        a14prog1g2.inscrireEtudiant(tb);
        a14prog1g2.inscrireEtudiant(mt);
        a14prog1g2.retirerEtudiant(mt);

        return c;
    }

    @SuppressWarnings("unchecked")
    private void initUI() {
        setTitle("Gestion de cours");
        setSize(650, 650);
        setLocationRelativeTo(null);
        setContentPane(panel1);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        cboSession.setModel(new DefaultComboBoxModel(Session.values()));
        setModelCoursGroupe(((Session) cboSession.getSelectedItem()).name());

        String[] headers = new String[]{"Numéro de dossier", "Prénom et nom", "Note"};
        tblEtudiants.setModel(new DefaultTableModel(headers, 0) {
            @Override
            public boolean isCellEditable(int row, int column) {
                return column == 2; //On veut uniquement être en mesure de modifier la note de l'étudiant
            }
        });
        reloadTableEtudiant((CoursGroupe) cboCoursGroupe.getSelectedItem());
        reloadListEtudiant(((CoursGroupe) cboCoursGroupe.getSelectedItem()).getListeEtudiant());

        cboSession.addActionListener(e -> {
            setModelCoursGroupe(((Session) cboSession.getSelectedItem()).name());
            CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
            if (cg != null) {
                reloadTableEtudiant(cg);
                reloadListEtudiant(cg.getListeEtudiant());
            } else {
                clearTableEtudiant();
                reloadListEtudiant(null);
            }
        });

        cboCoursGroupe.addActionListener(e -> {
            CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
            if (cg != null) {
                reloadTableEtudiant(cg);
                reloadListEtudiant(cg.getListeEtudiant());
            } else {
                clearTableEtudiant();
                reloadListEtudiant(null);
            }
        });

        //Modification des données de la table
        tblEtudiants.getModel().addTableModelListener(e -> {
            if (e.getType() == TableModelEvent.UPDATE) {
                TableModel mm = (TableModel) e.getSource();
                String noDossier = (String) mm.getValueAt(e.getFirstRow(), 0);
                Etudiant etudiant = cegep.getEtudiant(noDossier);
                if (etudiant != null) {
                    int nouvelleNote = -1;
                    try {
                        nouvelleNote = Integer.parseInt((String) mm.getValueAt(e.getFirstRow(), 2));
                    } catch (Exception f) {
                        //rien à faire
                    }
                    if (nouvelleNote >= 0 && nouvelleNote <= 100) {
                        CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
                        UndoManager.faire(etudiant.getNoteCours(cg), cg, etudiant, nouvelleNote);
                        setUndoRedoButton();
                    }
                }
            }
        });

        //Sorter du JTable
        TableRowSorter<DefaultTableModel> sorter = new TableRowSorter<>((DefaultTableModel) tblEtudiants.getModel());
        tblEtudiants.setRowSorter(sorter);

        tblEtudiants.getTableHeader().addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                int index = tblEtudiants.columnAtPoint(e.getPoint());
                if (index == 1) {
                    sorter.setComparator(1, Etudiant.getComparateurNumeroDossier());
                    sorter.sort();
                } else if (index == 2) {
                    sorter.setComparator(2, Etudiant.getComparateurNoms());
                    sorter.sort();
                }
            }
        });

        //Ajouter un étudiant au CoursGroupe
        btnAjouterEtudiant.addActionListener(e -> {
            Etudiant etudiant = (Etudiant) listEtudiants.getSelectedValue();
            if (etudiant != null) {
                CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
                if (cg != null) {
                    UndoManager.faire(etudiant, cg, UndoManager.TypeOperation.AJOUTER);
                    //il est plus simple de recalculer les éléments allant dans la liste d'étudiants
                    //que de commencer à enlever/réajoute un étudiant suite à un undo/redo
                    reloadTableEtudiant(cg);
                    reloadListEtudiant(cg.getListeEtudiant());
                    setUndoRedoButton();
                }
            }
        });

        //Retirer un étudiant du CoursGroupe
        btnRetirerEtudiant.addActionListener(e -> {
            int row = tblEtudiants.getSelectedRow();
            Etudiant etudiant = null;
            if (row != -1)
                etudiant = cegep.getEtudiant((String) tblEtudiants.getValueAt(row, 0));
            if (etudiant != null) {
                CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
                UndoManager.faire(etudiant, cg, UndoManager.TypeOperation.SUPPRIMER);
                reloadTableEtudiant(cg);
                reloadListEtudiant(cg.getListeEtudiant());
                setUndoRedoButton();
            }
        });

        //Undo
        btnUndo.addActionListener(e -> {
            if (UndoManager.annuler()) {
                //Il faudrait que j'implémente un moyen de connaître quel était le type d'opération qui a été effectué
                //sans pour autant retourner l'objet Operation
                //operation.getClass().toString() est une alternative
                CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
                if (cg != null) {
                    reloadTableEtudiant(cg);
                    reloadListEtudiant(cg.getListeEtudiant());
                }
            }
            setUndoRedoButton();
        });

        //Redo
        btnRedo.addActionListener(e -> {
            if (UndoManager.refaire()) {
                CoursGroupe cg = (CoursGroupe) cboCoursGroupe.getSelectedItem();
                if (cg != null) {
                    reloadTableEtudiant(cg);
                    reloadListEtudiant(cg.getListeEtudiant());
                }
            }
            setUndoRedoButton();
        });
    }

    private void setUndoRedoButton() {
        btnUndo.setEnabled(UndoManager.canUndo());
        btnRedo.setEnabled(UndoManager.canRedo());
    }

    private void setModelCoursGroupe(String session) {
        cboCoursGroupe.setModel(new DefaultComboBoxModel(cegep.getListeCoursGroupe(session).toArray()));
    }

    private void clearTableEtudiant() {
        ((DefaultTableModel) tblEtudiants.getModel()).setRowCount(0);
    }

    private void reloadTableEtudiant(CoursGroupe cg) {
        DefaultTableModel model = (DefaultTableModel) tblEtudiants.getModel();
        model.setRowCount(0);
        for (Etudiant e : cg.getListeEtudiant()) {
            Note note = e.getNoteCours(cg);
            model.addRow(new Object[]{
                    e.getNoDossier(),
                    e.getPrenom() + " " + e.getNomFamille(),
                    note == null ? "" : note.getResultat() != -1 ? note.getResultat() : ""
            });
        }
    }

    private void reloadListEtudiant(ArrayList<Etudiant> cg) {
        //Considérant la grosseur de l'application, il n'est pas nécessaire d'optimiser la méthode qui s'occupera
        //de recharger la liste des étudiants qui peuvent être ajoutés au CoursGroupe
        ArrayList<Etudiant> etudiants = cegep.getListeEtudiants();
        if (cg != null) {
            listEtudiants.setListData(etudiants.stream().filter(e -> !cg.contains(e)).filter().toArray());
        } else {
            listEtudiants.setListData(etudiants.toArray());
        }
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
        panel1.setLayout(new GridLayoutManager(3, 1, new Insets(0, 0, 0, 0), -1, -1));
        final JPanel panel2 = new JPanel();
        panel2.setLayout(new GridLayoutManager(4, 2, new Insets(20, 10, 0, 10), -1, -1));
        panel1.add(panel2, new GridConstraints(1, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        cboSession = new JComboBox();
        panel2.add(cboSession, new GridConstraints(1, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, new Dimension(151, 26), null, 0, false));
        cboCoursGroupe = new JComboBox();
        panel2.add(cboCoursGroupe, new GridConstraints(1, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JScrollPane scrollPane1 = new JScrollPane();
        panel2.add(scrollPane1, new GridConstraints(2, 0, 1, 2, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_WANT_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_WANT_GROW, null, null, null, 0, false));
        tblEtudiants = new JTable();
        scrollPane1.setViewportView(tblEtudiants);
        final JLabel label1 = new JLabel();
        label1.setText("Choix de session");
        panel2.add(label1, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JLabel label2 = new JLabel();
        label2.setHorizontalTextPosition(2);
        label2.setText("Choix de cours groupe");
        panel2.add(label2, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_WEST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_FIXED, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        btnRetirerEtudiant = new JButton();
        btnRetirerEtudiant.setText("Retirer l'étudiant sélectionné");
        panel2.add(btnRetirerEtudiant, new GridConstraints(3, 0, 1, 2, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JPanel panel3 = new JPanel();
        panel3.setLayout(new GridLayoutManager(1, 1, new Insets(10, 10, 10, 10), -1, -1));
        panel1.add(panel3, new GridConstraints(2, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, true));
        final JPanel panel4 = new JPanel();
        panel4.setLayout(new GridLayoutManager(1, 1, new Insets(0, 0, 0, 0), -1, -1));
        panel3.add(panel4, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, new Dimension(28, 48), null, 0, true));
        panel4.setBorder(BorderFactory.createTitledBorder(BorderFactory.createRaisedBevelBorder(), null));
        final JPanel panel5 = new JPanel();
        panel5.setLayout(new GridLayoutManager(1, 2, new Insets(5, 5, 5, 5), -1, -1));
        panel4.add(panel5, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, true));
        panel5.setBorder(BorderFactory.createTitledBorder(BorderFactory.createEmptyBorder(1, 1, 1, 1), null, TitledBorder.DEFAULT_JUSTIFICATION, TitledBorder.DEFAULT_POSITION, null, new Color(-14129468)));
        listEtudiants = new JList();
        listEtudiants.setDropMode(DropMode.USE_SELECTION);
        listEtudiants.setSelectionMode(0);
        panel5.add(listEtudiants, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_WANT_GROW, null, new Dimension(358, 50), null, 0, false));
        btnAjouterEtudiant = new JButton();
        btnAjouterEtudiant.setText("Ajouter l'étudiant sélectionné");
        btnAjouterEtudiant.setVisible(true);
        panel5.add(btnAjouterEtudiant, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_EAST, GridConstraints.FILL_NONE, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final JPanel panel6 = new JPanel();
        panel6.setLayout(new GridLayoutManager(1, 2, new Insets(0, 0, 0, 0), -1, -1));
        panel1.add(panel6, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, false));
        final JPanel panel7 = new JPanel();
        panel7.setLayout(new GridLayoutManager(1, 3, new Insets(0, 0, 0, 0), -1, -1));
        panel6.add(panel7, new GridConstraints(0, 0, 1, 2, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_BOTH, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, null, null, null, 0, true));
        btnUndo = new JButton();
        btnUndo.setEnabled(false);
        btnUndo.setText("Annuler");
        panel7.add(btnUndo, new GridConstraints(0, 1, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        btnRedo = new JButton();
        btnRedo.setEnabled(false);
        btnRedo.setText("Refaire");
        panel7.add(btnRedo, new GridConstraints(0, 2, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_CAN_SHRINK | GridConstraints.SIZEPOLICY_CAN_GROW, GridConstraints.SIZEPOLICY_FIXED, null, null, null, 0, false));
        final Spacer spacer1 = new Spacer();
        panel7.add(spacer1, new GridConstraints(0, 0, 1, 1, GridConstraints.ANCHOR_CENTER, GridConstraints.FILL_HORIZONTAL, GridConstraints.SIZEPOLICY_WANT_GROW, 1, null, null, null, 0, false));
        label1.setLabelFor(cboSession);
        label2.setLabelFor(cboCoursGroupe);
    }

    /**
     * @noinspection ALL
     */
    public JComponent $$$getRootComponent$$$() {
        return panel1;
    }
}
