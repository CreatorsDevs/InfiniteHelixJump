using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Game Settings")]
    public int score = 0;
    private bool gameStarted;
    private static bool restartedGame;
    public bool GameStarted { get => gameStarted; private set => gameStarted = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 0;
        if (restartedGame)
            {
                startScreen.SetActive(false);
                StartGame();
            }
        else
            startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        UpdateScoreUI();
    }

    public void StartGame()
    {
        if (!restartedGame)
        {
            AudioManager.Instance.Play("Select");
        }
        GameStarted = true;
        Time.timeScale = 1; 
        startScreen.SetActive(false);
    }

    public void GameOver()
    {
        GameStarted = false;
        Time.timeScale = 0; 
        gameOverScreen.SetActive(true);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScore = score;
        }

        highScoreText.text = $"{highScore}";
    }

    public void RestartGame()
    {
        AudioManager.Instance.Play("Select");
        restartedGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"{score}";
    }
}
