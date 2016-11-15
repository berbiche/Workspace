package undo;


import cegep.CoursGroupe;
import cegep.Etudiant;
import cegep.Note;
import pile.Pile;

public class UndoManager {

    private static Pile<Operation> undoPile, redoPile;
    private static UndoManager undo = new UndoManager();

    public enum TypeOperation {
        SUPPRIMER,
        AJOUTER
    }

    private UndoManager() {
        undoPile = new Pile<>();
        redoPile = new Pile<>();
    }

    public static boolean canUndo() {
        return undoPile.size() > 0;
    }

    public static boolean canRedo() {
        return redoPile.size() > 0;
    }

    /**
     * @param e
     * @param cg
     * @param type
     */
    public static void faire(Etudiant e, CoursGroupe cg, UndoManager.TypeOperation type) {
        EtudiantOperation operation = new EtudiantOperation(e, cg, type);
        operation.faire();
        undoPile.push(operation);
    }

    /**
     * @param n
     * @param cg
     * @param e
     * @param nouvelleNote
     */
    public static void faire(Note n, CoursGroupe cg, Etudiant e, int nouvelleNote) {
        NoteOperation operation = new NoteOperation(n, cg, e, nouvelleNote);
        operation.faire();
        undoPile.push(operation);
    }

    /**
     *
     */
    public static boolean annuler() {
        Operation operation = undoPile.pop();
        if (operation != null) {
            operation.annuler();
            redoPile.push(operation);
            return true;
        }
        return false;
    }
    
    public static boolean refaire() {
        Operation operation = redoPile.pop();
        if (operation != null) {
            operation.faire();
            undoPile.push(operation);
            return true;
        }
        return false;
    }

}
