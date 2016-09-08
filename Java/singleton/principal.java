
public class Singleton {

    private static Singleton singleton = new Singleton();
    private int x;

    /**
     * Private constructor
     */
    private Singleton() {
        this.x = 0;
    }

    /**
     * Returns the only instance of the singleton
     * @return the only instance of the singleton
     */
    public static Singleton getInstance() {
        return singleton;
    }

}
