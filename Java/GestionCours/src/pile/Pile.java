package pile;

import java.util.*;

public class Pile<T> {
    private List<T> elements;

    public Pile() {
        elements = new ArrayList<>();
    }

    public int size() {
        return elements.size();
    }

    public T peek() {
        return size() == 0 ? null : elements.get(size() - 1);
    }

    /**
     * Enlève le dernier élément
      * @return le dernier élément ou null
     */
    public T pop() {
        if (size() == 0) {
            return null;
        }
        T e = elements.get(size() - 1);
        elements.remove(size() - 1);
        return e;
    }

    public void push(T e) {
        elements.add(e);
    }
}
