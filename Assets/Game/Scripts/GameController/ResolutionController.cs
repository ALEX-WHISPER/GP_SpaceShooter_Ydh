using UnityEngine;

public class ResolutionController : MonoBehaviour {
    public int screenWidth = 625;
    public int screenHeight = 1000;

    private static ResolutionController _instance;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        Screen.SetResolution(screenWidth, screenHeight, false);
    }
}
