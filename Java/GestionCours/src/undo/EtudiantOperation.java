package undo;

import cegep.CoursGroupe;
import cegep.Etudiant;

class EtudiantOperation extends Operation {

    private final TypeOperation type;
    private final Etudiant etudiant;
    private final CoursGroupe coursGroupe;

    public enum TypeOperation {
        SUPPRIMER,
        AJOUTER
    }

    EtudiantOperation(Etudiant etudiant, CoursGroupe coursGroupe, TypeOperation type) {
        this.etudiant = etudiant;
        this.coursGroupe = coursGroupe;
        this.type = type;
    }

    @Override
    void annuler() {
        switch(type) {
            case AJOUTER:
                coursGroupe.retirerEtudiant(etudiant);
                break;
            case SUPPRIMER:
                coursGroupe.inscrireEtudiant(etudiant);
                break;
        }
    }

    @Override
    void faire() {
        switch(type) {
            case AJOUTER:
                coursGroupe.inscrireEtudiant(etudiant);
                break;
            case SUPPRIMER:
                coursGroupe.retirerEtudiant(etudiant);
                break;
        }
    }

}
