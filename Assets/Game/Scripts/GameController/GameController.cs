using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject m_GameOverPanel;
    public GameObject m_GameWinPanel;

    private LevelLoader m_LevelLoader;
    private bool isGameOver;
    private static GameController _instance;

    public static GameController GetInstance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
        }

        m_LevelLoader = GetComponent<LevelLoader>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            OnGameOver();
        }

        if (!isGameOver) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            m_LevelLoader.LoadLevelWithUISettings(0);
        }
    }

    //  游戏胜利
    public void OnGameWin() {
        if (!m_GameWinPanel.activeSelf) {
            m_GameWinPanel.SetActive(true);
        }

        PlayerMoving.instance.DisableController();
        isGameOver = true;
    }

    //  游戏失败
    public void OnGameOver() {
        if (!m_GameOverPanel.activeSelf) {
            m_GameOverPanel.SetActive(true);
        }
        
        PlayerMoving.instance.DisableController();
        isGameOver = true;
    }

    //  本关胜利
    public void OnLevelPassed() {
        m_LevelLoader.LoadNextLevel();
    }
}
