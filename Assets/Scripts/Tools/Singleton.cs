using UnityEngine;

public abstract class Singleton : MonoBehaviour {
    public static Singleton instance;

    //Awake is always called before any Start functions
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    //Initializes the game for each level.
    protected abstract void Init();
}