package undo;


import cegep.CoursGroupe;
import cegep.Etudiant;
import cegep.Note;
import pile.Pile;

public class UndoManager {

    private static Pile<Operation> undoPile, redoPile;

    private UndoManager() {
        operationPile = new Pile<>(), redoPile = new Pile<>();
    }

    public void do(Etudiant e, CoursGroupe cg, EtudiantOperation.TypeOperation type) {
        EtudiantOperation operation = new EtudiantOperation(e, cg, type);
        operation.faire();
        undoPile.push(operation);
    }

    public void do(Note n, int ancienneNote, int nouvelleNote) {
        NoteOperation operation = new NoteOperation(n, ancienneNote, nouvelleNote);
        operation.faire();
        undoPile.push(operation);
    }

    public void undo() {
        Operation operation = operationPile.pop();
        if (operation != null) {
            operation.annuler();
            redoPile.push(operation);
        }
    }
    
    public void redo() {
        Operation operation = redoPile.pop();
        if (operation != null) {
            operation.faire();
            undoPile.push(operation);
        }
    }

}
