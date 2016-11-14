package undo;


import cegep.CoursGroupe;
import cegep.Etudiant;
import cegep.Note;
import pile.Pile;

public class UndoManager {

    private static Pile<Operation> operationPile;

    private UndoManager() {
        operationPile = new Pile<>();
    }

    public void faire(Etudiant e, CoursGroupe cg, EtudiantOperation.TypeOperation type) {
        EtudiantOperation operation = new EtudiantOperation(e, cg, type);
        operationPile.push(operation);
        operation.faire();
    }

    public void faire(Note n, int ancienneNote, int nouvelleNote) {
        NoteOperation operation = new NoteOperation(n, ancienneNote, nouvelleNote);
        operation.faire();
        operationPile.push(operation);
    }

    public void annuler() {
        Operation operation = operationPile.pop();
        if (operation != null) {
            operation.annuler();
        }
    }

}
