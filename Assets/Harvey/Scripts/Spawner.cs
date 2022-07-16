using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Tooltip("The list of prefebs you want to spawn with the spawner")]
    float spawnTimer = 0.0f;
    [Tooltip("The bool to communitcate with game manager and see if there are too many enemies in the game world")]
    bool tooManyEnemies = false;

    [Tooltip("The list of prefebs you want to spawn with the spawner")]
    [SerializeField] GameObject[] listOfSpawn;
    [Tooltip("The time interval that you want the spawner to spawn objects")]
    [SerializeField] float spawnInterval;

    void Start()
    {
        // If the list of spawning object is empty for some reason
        if (IsGameObjectArrayEmpty(listOfSpawn) == true)
        {
            // Ignore this spawner
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Update the spawn timer and see if this spawner should spawn an object
        CountDownToSpawn();
    }

    private void CountDownToSpawn()
    {
        // If it is not time to spawn an object yet
        if (spawnTimer < spawnInterval)
        {
            // Increament the spawnTimer by the time passed in real time
            spawnTimer += Time.deltaTime;
        }
        // If it is time to spawn an object
        else
        {
            // See if there are too many enemies in the game world
            if (tooManyEnemies == true)
            {
                // Do not spawn an enemy
                return;
            }
            // If there are not too many enemies in the game world
            else
            {
                // Randomize which enemy I should spawn
                int index = Random.Range(0, listOfSpawn.Length);

                // Spawn the gameObject prefeb
                SpawnGameObject(listOfSpawn[index]);
            }
        }
    }

    private void SpawnGameObject(GameObject gameObject)
    {
        // Instantiate a new gameObject with the prefeb provided
        Instantiate(gameObject, transform);

        // Reset the spawn timer
        spawnTimer = 0.0f;
    }

    // Return "true" if the gameObject array is empty
    private bool IsGameObjectArrayEmpty(GameObject[] gameObjects)
    {
        // If the list of spawning object is empty
        if (gameObjects.Length <= 0)
        {
            // Return true
            return true;
        }

        // Else, return false
        return false;
    }

    public void UpdateTooManyEnemyStatus(bool boolean)
    {
        // Update the status of the game world in terms of the amount of enemies
        tooManyEnemies = boolean;
    }
}
