using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : Singleton<GameManager>
{
    public PlayerLogic player;
   
    public enum GameState
    {   MainMenu,
        Pause,
        Play
    }
    public GameState gameState;

    public List<GameObject> enemyList;

    //Store the player here when we get it
    //public PlayerLogic player { get; set; }

    public uint highScore { get; set; }
    public uint score { get; set; }
    public uint HP { get; set; }

    private void Start()
    {
        enemyList = new List<GameObject>();
    }

    public void SpawnEnemyListenerEvent()
    {
        //Add the enemy to the list to enemyList here when we have their class.
    }
}
