using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level = 1;
    public int lives = 3;
    public int score = 0;
    private Ball ball;
    private Paddle paddle;
    private Bricks[] bricks;
    private const int NUM_LEVELS = 3;

    public UiManager ui;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FindSceneReferences();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            SceneManager.sceneLoaded -= OnLevelLoaded;
        }
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Bricks>();
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            // Restart at level 1 or load a "Win" scene if available
            SceneManager.LoadScene("Winner");
            return;
        }

        // Ensure only one subscription of OnLevelLoaded
        SceneManager.sceneLoaded -= OnLevelLoaded;
        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level-{level}");
        Destroy(gameObject);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSceneReferences();
    }

    public void OnBallMiss()
    {
        lives--;

        if (ui != null && ui.lives != null)
        {
            ui.lives.SetText("Lives: " + lives);
        }

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        paddle?.ResetPaddle();
        ball?.ResetBall();
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Gameover");
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;

        if (ui != null)
        {
            ui.Score?.SetText("Score: " + score);
            ui.lives?.SetText("Lives: " + lives);
        }

        LoadLevel(1);
    }

    public void OnBrickHit(Bricks brick)
    {
        score += brick.points;

        if (ui != null && ui.Score != null)
        {
            ui.Score.SetText("Score: " + score);
        }

        if (IsLevelCleared())
        {
            LoadLevel(level + 1);
        }
    }

    private bool IsLevelCleared()
    {
        foreach (var brick in bricks)
        {
            if (brick.gameObject.activeInHierarchy && !brick.unbreakable)
            {
                return false;
            }
        }

        return true;
    }
}
