package undo;

import cegep.CoursGroupe;
import cegep.Etudiant;
import cegep.Note;

class NoteOperation extends Operation {

    private final Note note;
    private final int ancienneNote, nouvelleNote;

    NoteOperation(Note note, int ancienneNote, int nouvelleNote) {
        this.note = note;
        this.ancienneNote = ancienneNote;
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
