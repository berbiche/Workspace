package dictionnaire;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.NoSuchElementException;

public class ArbreBinaire<K extends Comparable<K>, V> implements Iterable<K> {

    private Noeud racine = null;
    private int size = 0;
    private boolean direction = false;

    public int size() {
        return size;
    }

    public boolean containsKey(K cle) {
        if (cle == null)
            throw new NullPointerException();

        return get(cle, racine) != null;
    }

    public V put(K cle, V element) {
        if (cle == null)
            throw new NullPointerException();

        if (racine == null) {
            racine = new Noeud(cle, element, null);
            size++;
            return null;
        }

        return put(cle, element, racine, racine);
    }

    private V put(K cle, V element, Noeud noeud, Noeud parent) {
        if (noeud == null) {
            noeud = new Noeud(cle, element, parent);
            size++;
            return element;
        }

        if(noeud.cle.compareTo(cle) == 0) {
            V contenu = noeud.contenu;
            noeud.contenu = element;
            return contenu;
        } else if (cle.compareTo(noeud.cle) < 0) {
            if (noeud.gauche != null)
                return put(cle, element, noeud.gauche, noeud);
            size++;
            noeud.gauche = new Noeud(cle, element, noeud);
            return null;
        }
        if (noeud.droite != null)
            return put(cle, element, noeud.droite, noeud);
        size++;
        noeud.droite = new Noeud(cle, element, noeud);
        return null;
    }

    public V get(K cle) {
        if (cle == null)
            throw new NullPointerException();

        Noeud n = get(cle, racine);
        if (n != null)
            return n.contenu;
        return null;
    }

    private Noeud get(K cle, Noeud noeud) {
        if (noeud == null)
            return null;

        if (cle.compareTo(noeud.cle) == 0)
            return noeud;
        else if (cle.compareTo(noeud.cle) < 0)
            return get(cle, noeud.gauche);
        return get(cle, noeud.droite);
    }

    public V remove(K cle) {
        if (cle == null)
            throw new NullPointerException();

        if (size == 1 && cle.compareTo(racine.cle) == 0) {
            V contenu = racine.contenu;
            racine = null;
            size--;
            return contenu;
        }

        return remove(get(cle, racine));
    }

    private V remove(Noeud noeudToRemove) {
        if (noeudToRemove == null)
            return null;

        if (noeudToRemove.gauche != null && noeudToRemove.droite != null) {
            //Pour conserver le contenu original que doit retourner la méthode
            V contenuTemp = noeudToRemove.contenu;
            //Suppression noeud avec 2 enfants
            remove(removeTwoChild(noeudToRemove));
            return contenuTemp;
        } else if (noeudToRemove.gauche != null || noeudToRemove.droite != null) {
            //Suppression noeud avec 1 enfant
            size--;
            return removeOneChild(noeudToRemove);
        }
        //Suppression noeud sans enfant
        size--;
        return removeAlone(noeudToRemove);
    }

    private V removeAlone(Noeud noeud) {
        V contenuTemp = noeud.contenu;
        if (noeud.parent != null) {
            if (noeud.parent.gauche != null && noeud.parent.gauche.cle.compareTo(noeud.cle) == 0)
                noeud.parent.gauche = null;
            else if (noeud.parent.droite != null && noeud.parent.droite.cle.compareTo(noeud.cle) == 0)
                noeud.parent.droite = null;
        }
        noeud = null;
        return contenuTemp;
    }

    private V removeOneChild(Noeud noeud) {
        if (noeud.gauche != null)
            if (noeud.parent.gauche.cle.compareTo(noeud.cle) == 0)
                noeud.parent.gauche = noeud.gauche;
            else
                noeud.parent.droite = noeud.gauche;
        else
            if (noeud.parent.droite.cle.compareTo(noeud.cle) == 0)
                noeud.parent.droite = noeud.droite;
            else
                noeud.parent.gauche = noeud.droite;
        return noeud.contenu;
    }

    private Noeud removeTwoChild(Noeud noeud) {
        Noeud n;
        if (direction) {
            n = pgp(noeud.gauche);
            direction = false;
        } else {
            n = ppg(noeud.droite);
            direction = true;
        }
        noeud.contenu = n.contenu;
        noeud.cle = n.cle;
        return n;
    }

    //plus grand des plus petits
    private Noeud pgp(Noeud noeud) {
        if (noeud.droite != null) {
            return pgp(noeud.droite);
        }
        return noeud;
    }

    //plus petit des plus grands
    private Noeud ppg(Noeud noeud) {
        if (noeud.gauche != null) {
            return ppg(noeud.gauche);
        }
        return noeud;
    }

    @Override
    public Iterator<K> iterator() {
        return new Iterateur();
    }

    private class Iterateur implements Iterator<K> {

        private List<K> contenu;
        private int index = -1;

        public Iterateur() {
            contenu = new ArrayList<K>();

            init(racine);
        }

        private void init(Noeud noeud) {
            if (noeud != null) {
                init(noeud.gauche);
                contenu.add(noeud.cle);
                init(noeud.droite);
            }
        }

        @Override
        public boolean hasNext() {
            return index < contenu.size() - 1;
        }

        @Override
        public K next() {
        	if (!hasNext())
        		throw new NoSuchElementException();
            index++;
            return contenu.get(index);
        }

    }

    private class Noeud {

        private K cle;
        private V contenu;
        private Noeud parent = null;
        private Noeud gauche = null;
        private Noeud droite = null;

        public Noeud(K cle, V contenu, Noeud parent) {
            this.cle = cle;
            this.contenu = contenu;
            this.parent = parent;
        }

    }

}