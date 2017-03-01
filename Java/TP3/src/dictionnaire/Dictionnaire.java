package dictionnaire;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

public class Dictionnaire {

    private final ArbreBinaire<String, ArrayList<String>> arbreBinaire;

    public Dictionnaire() {
        arbreBinaire = new ArbreBinaire<>();
    }

    public boolean contient(String mot) {
        return arbreBinaire.containsKey(mot);
    }

    public int nbMots() {
        return arbreBinaire.size();
    }

    public List<String> definitions(String mot) {
        ArrayList<String> mots = arbreBinaire.get(mot);
        if (mots == null)
            return null;
        return Collections.unmodifiableList(mots);
    }

    public ArrayList<String> mots() {
        ArrayList<String> mots = new ArrayList<String>();
        for (String s: arbreBinaire) {
            mots.add(s);
        }
        return mots;
    }

    public void ajouter(String mot, String definition) {
        ArrayList<String> mots = arbreBinaire.get(mot);
        if (mots != null)
            throw new DictionnaireException();

        mots = new ArrayList<String>();
        mots.add(definition);
        arbreBinaire.put(mot, mots);
    }

    public boolean retirer(String mot) {
        return arbreBinaire.remove(mot) != null;
    }

    public void ajouterDefinition(String mot, String definition) {
        ArrayList<String> mots = arbreBinaire.get(mot);
        if (mots == null)
            throw new DictionnaireException();

        mots.add(definition);
    }

    public String retirerDefinition(String mot, int index) {
        ArrayList<String> mots = arbreBinaire.get(mot);
        if (mots == null)
            throw new DictionnaireException();

        if (index < 0 || index >= arbreBinaire.size())
            throw new IndexOutOfBoundsException();

        return mots.remove(index);
    }

    public String modifierDefinition(String mot, int index, String definition) {
        ArrayList<String> mots = arbreBinaire.get(mot);
        if (mots == null)
            throw new DictionnaireException();
        return mots.set(index, definition);
    }

    public class DictionnaireException extends  RuntimeException {

        public DictionnaireException() {
            this("");
        }

        public DictionnaireException(String erreur) {
            super(erreur);
        }

    }

}
