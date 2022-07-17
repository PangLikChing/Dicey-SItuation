using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MainMenu,
        Pause,
        Play,
        GameOver
    }
    [HideInInspector] public GameState gameState;

    List<Enemy> enemyList;

    [Tooltip("The name of the main menu scene")]
    [SerializeField] string mainMenuSceneName = "Main Menu";
    [Tooltip("The maximum amount of enemies can be present at once in the game world")]
    [SerializeField] int maximumEnemies = 10;

    public UnityEvent<bool> tooManyEnemies;
    public UnityEvent<int> updateHealth;
    public UnityEvent<int> updateScore;
    public UnityEvent<int> updateHighscore;
    public UnityEvent<Sprite> updateWeaponSprite;
    public UnityEvent<int, int> updateAmmo;

    [Tooltip("The player of the game")]
    [HideInInspector] public PlayerLogic player { get; set; }
    [Tooltip("The current score of the player")]
    [HideInInspector] public int score { get; set; }
    [Tooltip("The highscore of the player")]
    [HideInInspector] public int highScore { get; set; }

    private void Start()
    {
        // Initialize
        enemyList = new List<Enemy>();

        // Update the current score UI
        updateScore.Invoke(0);

        // Update the highscore UI
        // If there is a highscore
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highScore = PlayerPrefs.GetInt("Highscore");
            updateHighscore.Invoke(highScore);
        }
        // If there is no highscore yet
        else
        {
            updateHighscore.Invoke(0);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }


    // Response when an enemy is spawned
    public void SpawnEnemyListenerEvent(Enemy enemy)
    {
        // Add the enemy list to the enemy list
        enemyList.Add(enemy);

        // Check if there are too many enemies in the game world
        CheckGameWorldEnemies();
    }

    // Function to update current score in the game manger
    public void UpdateScore(int _score)
    {
        // Increase the score
        score += _score;
    }

    // Response when an enemy is dead
    public void EnemyDiedListenerEvent(Enemy enemy)
    {
        // Remove the enemy from the enemy list
        enemyList.Remove(enemy);

        // Check if there are too many enemies in the game world
        CheckGameWorldEnemies();

        // Update the score
        UpdateScore(enemy.enemyScoreValue);

        // Update the current score
        updateScore?.Invoke(score);
    }

    // Function to check if there is too many enemies in the game world
    private void CheckGameWorldEnemies()
    {
        // If there are too many enemies on the game world
        if (enemyList.Count >= maximumEnemies)
        {
            // Broadcast the message that there are too many enemies in the game world
            tooManyEnemies.Invoke(true);
        }
        else
        {
            // Broadcast the message that there are not enough enemies in the game world
            tooManyEnemies.Invoke(false);
        }
    }

    // Response when the player is dead
    public void PlayerDeath()
    {
        // See if the highscore should be updated
        UpdateHighscore(score);

        // Throw the player back to the main menu
        SceneManager.LoadScene(mainMenuSceneName);

        Debug.Log("Player Death");
    }

    // Function to update highscore in the UI
    private void UpdateHighscore(int score)
    {
        // If there is a highscore already
        if (PlayerPrefs.HasKey("Highscore"))
        {
            // Compare and see if the current score is higher than the highscore
            if (score > PlayerPrefs.GetInt("Highscore"))
            {
                // Update the highscore
                PlayerPrefs.SetInt("Highscore", score);
                highScore = PlayerPrefs.GetInt("Highscore");
                updateHighscore.Invoke(highScore);
            }
        }
        // If there no highscore yet
        else
        {
            // Update the highscore
            PlayerPrefs.SetInt("Highscore", score);
            highScore = PlayerPrefs.GetInt("Highscore");
            updateHighscore.Invoke(highScore);
        }
    }
}
