package undo;

import cegep.CoursGroupe;
import cegep.Etudiant;
import cegep.Note;

class NoteOperation extends Operation {

    private final Note note;
    private final int ancienneNote, nouvelleNote;

    NoteOperation(Note note, CoursGroupe cg, Etudiant e, int nouvelleNote) {
        if (note == null) {
            cg.ajouterNote(e, nouvelleNote);
            this.ancienneNote = -1;
            this.note = e.getNoteCours(cg);
        } else {
            this.note = note;
            ancienneNote = note.getResultat();
        }
        this.nouvelleNote = nouvelleNote;
    }

    @Override
    void annuler() {
        note.setResultat(ancienneNote);
    }

    @Override
    void faire() {
        note.setResultat(nouvelleNote);
    }

}
