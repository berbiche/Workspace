package principal;

import cegep.*;
import org.apache.commons.codec.language.Soundex;

import static org.junit.Assert.*;

public class Principal {
    public static void main(String[] args) {
        Cegep c = new Cegep();
        Etudiant mt = c.admettreEtudiant("Tremblay", "Mario");
        Etudiant jt = c.admettreEtudiant("Tremblay", "Jean");
        Etudiant ms = c.admettreEtudiant("Séguin", "Mario");
        Etudiant cm = c.admettreEtudiant("Marin", "Carl");
        Etudiant na = c.admettreEtudiant("Allard", "Nathalie");
        Etudiant jb = c.admettreEtudiant("Bédard", "Julie");
        Etudiant tb = c.admettreEtudiant("Beaudoin", "Tania");
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
        //a13prog1g1.ajouterNote(jt, 70);
        a13prog1g1.inscrireEtudiant(mt);
        //a13prog1g1.ajouterNote(mt, 80);
        a13prog1g2.inscrireEtudiant(ms);
        //a13prog1g2.ajouterNote(ms, 80);
        a13prog1g2.inscrireEtudiant(cm);
        //a13prog1g2.ajouterNote(cm, 90);
        h14prog2g1.inscrireEtudiant(mt);
        //h14prog2g1.ajouterNote(mt, 90);
        h14prog2g1.inscrireEtudiant(jt);
        //h14prog2g1.ajouterNote(jt, 100);
        h14prog2g1.inscrireEtudiant(ms);
        //h14prog2g1.ajouterNote(ms, 75);
        a14pog1.inscrireEtudiant(mt);
        //a14pog1.ajouterNote(mt, 80);
        a14pog1.inscrireEtudiant(jt);
        //a14pog1.ajouterNote(jt, 94);
        a14pog1.inscrireEtudiant(ms);
        //a14pog1.ajouterNote(ms, 82);
        a14prog1g1.inscrireEtudiant(cm);
        //a14prog1g1.ajouterNote(cm, 100);
        a14prog1g1.inscrireEtudiant(na);
        //a14prog1g1.ajouterNote(na, 66);
        a14prog1g2.inscrireEtudiant(jb);
        //a14prog1g1.ajouterNote(jb, 87);
        a14prog1g2.inscrireEtudiant(tb);
        //a14prog1g2.ajouterNote(tb, 88);
        a14prog1g2.inscrireEtudiant(mt);
        //a14prog1g1.ajouterNote(mt, 96);
        a14prog1g2.retirerEtudiant(mt);


        // Afficher le cours-groupe 420-D11, groupe 1, A13
        CoursGroupe cours1 = c.getCoursGroupe("420-D11", 1, "A13");
        System.out.println(cours1);
        System.out.println();
        // Afficher les étudiants de ce groupe
        System.out.println("Les étudiants du cours-groupe 420-D11, groupe 1, A13 sont:\n[");
        cours1.getListeEtudiant().forEach(etudiant -> System.out.println("\t" + etudiant));
        System.out.println("]\n");
        // fficher la moyenne de ce groupe
        System.out.println("La moyenne du groupe est: " + cours1.getMoyenne());
        System.out.println();
        // Afficher la note de Mario Tremblay pour ce cours
        System.out.println("La moyenne de Mario Tremblay est de: " + cours1.getNoteEtudiant("Mario", "Tremblay"));
        System.out.println();



        // Afficher le cours-groupe 420-D11, groupe 2, A13
        CoursGroupe cours2 = c.getCoursGroupe("420-D11", 2, "A13");
        System.out.println(cours2);
        System.out.println();
        // Afficher les étudiants de ce groupe
        System.out.println("Les étudiants du cours-groupe 420-D11, groupe 2, A13 sont:\n[");
        cours2.getListeEtudiant().forEach(etudiant -> System.out.println("\t" + etudiant));
        System.out.println("]\n");



        // Afficher le cours-groupe 420-D12, groupe 1, H14
        CoursGroupe cours3 = c.getCoursGroupe("420-D12", 1, "H14");
        System.out.println(cours3);
        System.out.println();
        // Afficher les étudiants de ce groupe
        System.out.println("Les étudiants du cours-groupe 420-D12, groupe 1, H14 sont:\n[");
        cours3.getListeEtudiant().forEach(etudiant -> System.out.println("\t" + etudiant));
        System.out.println("]\n");



        // Afficher le cours-groupe 420-D61, groupe 1, A14
        CoursGroupe cours4 = c.getCoursGroupe("420-D61", 1, "A14");
        System.out.println(cours4);
        System.out.println();
        // Afficher les étudiants de ce groupe
        System.out.println("Les étudiants du cours-groupe 420-D61, groupe 1, A14 sont:\n[");
        cours4.getListeEtudiant().forEach(etudiant -> System.out.println("\t" + etudiant));
        System.out.println("]\n");



        // Afficher le cours-groupe 420-D11, groupe 1, A14
        CoursGroupe cours5 = c.getCoursGroupe("420-D11", 1, "A14");
        System.out.println(cours5);
        System.out.println();
        // Afficher les étudiants de ce groupe
        System.out.println("Les étudiants du cours-groupe 420-D11, groupe 1, A14 sont:\n[");
        cours5.getListeEtudiant().forEach(etudiant -> System.out.println("\t" + etudiant));
        System.out.println("]\n");



        // Afficher le cours-groupe 420-D11, groupe 2, A14
        CoursGroupe cours6 = c.getCoursGroupe("420-D11", 2, "A14");
        System.out.println(cours6);
        System.out.println();
        // Afficher les étudiants de ce groupe
        System.out.println("Les étudiants du cours-groupe 420-D11, groupe 2, A14 sont:\n[");
        cours6.getListeEtudiant().forEach(etudiant -> System.out.println("\t" + etudiant));
        System.out.println("]\n");



        // Afficher tous les cours-groupe de Bob Jones à la session A13
        System.out.println("Les cours de Bob Jones à la session A13 sont:\n[");
        Professeur bobJones = c.getProfesseur("Jones", "Bob");
        bobJones.getCoursSession("A13").forEach(cg -> System.out.println("\t" + cg));
        System.out.println("]\n");



        // Afficher tous les cours-groupe de Bob Jones à la session A14
        System.out.println("Les cours de Bob Jones à la session A14 sont:\n[");
        bobJones.getCoursSession("A14").forEach(cg -> System.out.println("\t" + cg));
        System.out.println("]\n");



        // Afficher tous les cours-groupe de Joe Smith à la session A14
        System.out.println("Les cours de Joe Smith à la session A14 sont:\n[");
        Professeur joeSmith = c.getProfesseur("Smith", "Joe");
        joeSmith.getCoursSession("A13").forEach(cg -> System.out.println("\t" + cg));
        System.out.println("]\n");



        //	Afficher tous les cours de plusieurs étudiants (pour voir la correspondance)

        // Entrer les notes suivantes pour la session A13
        // Mario Tremblay D11: 70
        // Jean Tremblay D11: 80
        // Mario Séguin D11: 90
        // Carl Marin D11: 40
        System.out.println("Ajout de la note 70 à Mario Tremblay pour le cours 420-D11: "
                            + mt.getCoursGroupe("420-D11", "A13").ajouterNote(mt, 70));
        System.out.println("Ajout de la note 80 à Jean Tremblay pour le cours 420-D11: "
                            + jt.getCoursGroupe("420-D11", "A13").ajouterNote(jt, 80));
        System.out.println("Ajout de la note 90 à Mario Séguin pour le cours 420-D11: "
                            + ms.getCoursGroupe("420-D11", "A13").ajouterNote(ms, 90));
        System.out.println("Ajout de la note 40 à Carl Marin pour le cours 420-D11: "
                            + cm.getCoursGroupe("420-D11", "A13").ajouterNote(cm, 40));
        System.out.println();



        // Entrer les notes suivantes pour la session H14
        // Mario Tremblay D12: 80
        // Jean Tremblay D12: 90
        // Mario Séguin D12: 70
        System.out.println("Ajout de la note 80 à Mario Tremblay pour le cours 420-D12: "
                + mt.getCoursGroupe("420-D12", "H14").ajouterNote(mt, 80));
        System.out.println("Ajout de la note 90 à Jean Tremblay pour le cours 420-D12: "
                + jt.getCoursGroupe("420-D12", "H14").ajouterNote(jt, 90));
        System.out.println("Ajout de la note 70 à Mario Séguin pour le cours 420-D12: "
                + ms.getCoursGroupe("420-D12", "H14").ajouterNote(ms, 70));
        System.out.println();




        // Entrer les notes suivantes pour la session A14
        // Mario Tremblay D61: 80
        // Jean Tremblay D61: 90
        // Mario Séguin D61: 60
        // Carl Marin D11: 70
        // Nathalie Allard D11: 80
        // Julie Bédard D11: 90
        // Tania Beaudoin D11: 95
        System.out.println("Ajout de la note 80 à Mario Tremblay pour le cours 420-D61: "
                + mt.getCoursGroupe("420-D61", "A14").ajouterNote(mt, 80));
        System.out.println("Ajout de la note 90 à Jean Tremblay pour le cours 420-D61: "
                + jt.getCoursGroupe("420-D61", "A14").ajouterNote(jt, 90));
        System.out.println("Ajout de la note 60 à Mario Séguin pour le cours 420-D61: "
                + ms.getCoursGroupe("420-D61", "A14").ajouterNote(ms, 60));
        System.out.println("Ajout de la note 70 à Carl Marin pour le cours 420-D11: "
                + cm.getCoursGroupe("420-D11", "A14").ajouterNote(cm, 70));
        System.out.println("Ajout de la note 80 à Nathalie Allard pour le cours 420-D11: "
                + na.getCoursGroupe("420-D11", "A14").ajouterNote(na, 80));
        System.out.println("Ajout de la note 90 à Julie Bédard pour le cours 420-D11: "
                + jb.getCoursGroupe("420-D11", "A14").ajouterNote(jb, 90));
        System.out.println("Ajout de la note 95 à Tania Beaudoin pour le cours 420-D61: "
                + tb.getCoursGroupe("420-D11", "A14").ajouterNote(tb, 95));
        System.out.println();



        // Afficher toutes les notes du cours 420-D11, groupe 1, A13 (avec le nom des étudiants...)
        System.out.println("Notes des étudiants du cours 420-D11, groupe 1, A13 sont:\n[");
        c.getCoursGroupe("420-D11", 1, "A13")
                .getNotes()
                .forEach(note -> System.out.println("\t" + note.getEtudiant() + " => " + note.getResultat()));
        System.out.println("]\n");



        // Afficher la moyenne du cours 420-D11, groupe 1, A13
        System.out.println("Moyenne du cours-groupe 420-D11, groupe 1, A13: "
                            + c.getCoursGroupe("420-D11", 1, "A13").getMoyenne());
        System.out.println();



        // Afficher tous les cours qu'a suivi Carl Marin
        System.out.println("Cours de Carl Marin:\n[");
        cm.getCours().forEach(coursGroupe -> System.out.println("\t" + coursGroupe));
        System.out.println("]\n");



        // Afficher tous les cours qu'a suivi Mario Tremblay à la session A13
        System.out.println("Cours de Mario Tremblay à la session A13:\n[");
        mt.getCoursSession("A13")
                .forEach(coursGroupe -> System.out.println("\t" + coursGroupe));
        System.out.println("]\n");



        // Afficher toutes les notes de Mario Tremblay à la session A13
        System.out.println("Notes de Mario Tremblay à la session A13:\n[");
        mt.getNotesSession("A13")
                .forEach(note -> System.out.println("\t" + note));
        System.out.println("]\n");




        // Afficher la moyenne de Mario Tremblay à la session A13
        System.out.println("Moyenne de Mario Tremblay à la session A13:" + mt.getMoyenneSession("A13"));
        System.out.println();



        // Afficher toutes les notes de Mario Tremblay
        System.out.println("Toutes les notes de Mario Tremblay:\n[");
        mt.getNotes().forEach(note -> System.out.println("\t" + note));
        System.out.println("]\n");



        // Afficher la moyenne générale de Mario Tremblay
        System.out.println("Moyenne générale de Mario Tremblay: " + mt.getMoyenne());
        System.out.println();



        // Corriger la note de Mario Tremblay dans le cours 420-D11, groupe 1, A13 pour 90
        System.out.print("Corriger la note de Mario Tremblay cours-groupe 420-D11, groupe 1, A13:"
                            + "\n\t" + mt.getNotesCours(mt.getCoursGroupe("420-D11", "A13")).getResultat());
        mt.getNotesCours(mt.getCoursGroupe("420-D11", "A13")).setResultat(90);
        System.out.print(" => " + mt.getNotesCours(mt.getCoursGroupe("420-D11", "A13")).getResultat());
        System.out.println();
        System.out.println();



        // Afficher la moyenne de Mario Tremblay à la session A13
        System.out.println("Moyenne de Mario Tremblay à la session A13: " + mt.getMoyenneSession("A13"));
        System.out.println();



        // Afficher la moyenne du cours 420-D11, groupe 1, A13
        System.out.println("La moyenne du cours-groupe 420-D11, groupe 1, A13: "
                            + c.getCoursGroupe("420-D11", 1, "A13").getMoyenne());
    }
}
