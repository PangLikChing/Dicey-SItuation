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
    public UnityEvent<int> updateHealth;
    public UnityEvent<int> updateScore;

    public PlayerLogic player { get; set; }

    public int highScore { get; set; }
    public int score { get; set; }
    public int HP { get; set; }

    [Tooltip("Enemy death gives this score to the player")]
    public int enemyScoreValue = 10;

    private void Start()
    {
        enemyList = new List<Enemy>();
    }

    public void SpawnEnemyListenerEvent(Enemy enemy)
    {
        enemyList.Add(enemy);
    }

    public void UpdateScoreEvent(int _score)
    {
        score += _score;
    }

    public void EnemyDiedListenerEvent(Enemy enemy)
    {
        updateScore?.Invoke(enemyScoreValue);
    }

}
