using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {   MainMenu,
        Pause,
        Play
    }
    public GameState gameState;

    public List<Enemy> enemyList;
    public UnityEvent<bool> tooManyEnemies;
    public UnityEvent<int> updateHealth;
    public UnityEvent<int> updateScore;
    public UnityEvent<int> updateHighscore;

    [SerializeField] int maximumAmountOfEnemies = 10;

    public PlayerLogic player { get; set; }

    public int highScore { get; set; }
    public int score { get; set; }

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
            updateHighscore.Invoke(PlayerPrefs.GetInt("Highscore"));
        }
        // If there is no highscore yet
        else
        {
            updateHighscore.Invoke(0);
        }
    }

    public void SpawnEnemyListenerEvent(Enemy enemy)
    {
        // Add the enemy list to the enemy list
        enemyList.Add(enemy);

        // Check if there are too many enemies in the game world
        CheckGameWorldEnemies();
    }

    public void UpdateScore(int _score)
    {
        // Increase the score
        score += _score;
    }

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

    private void CheckGameWorldEnemies()
    {
        // If there are too many enemies on the game world
        if (enemyList.Count >= maximumAmountOfEnemies)
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

    public void PlayerDeath()
    {
        // See if the highscore should be updated
        UpdateHighscore(score);

        // something more here

        Debug.Log("Player Death");
    }

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
                updateHighscore.Invoke(PlayerPrefs.GetInt("Highscore"));
            }
        }
        // If there no highscore yet
        else
        {
            // Update the highscore
            PlayerPrefs.SetInt("Highscore", score);
            updateHighscore.Invoke(PlayerPrefs.GetInt("Highscore"));
        }
    }
}
