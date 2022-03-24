using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Written by Blake following a Brackeys tutorial
public class WaveSpawner : MonoBehaviour
{
    // Just used for checking what is going on with the wave
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    // Allows input for what types of enemies spawn in a wave, how many, how fast
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public string enemyName;
        public int count;
        public float rate;
    }

    // Keeps track of the waves
    public Wave[] waves;
    private int nextWave = 0;

    // Spawn points for all enemies
    public Transform[] spawnPoints;
    // Copy the spawnPoints list and this one will be used to find which spawnpoint will be used for the next night
    private List<Transform> spawnPointsReduction;
    // The radius the enemies can spawn randomly within
    private float spawnRadius = 2f;

    // Will be replaced later for enemys spawning at night
    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    // Checks if there are enemies every second given (default is checks if there are enemies every 1 second)
    // Basically to lower resource cost on computer
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    // The database that holds all the enemies
    public EnemyDatabaseSO baddies;


    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
        spawnPointsReduction = new List<Transform>(spawnPoints);
        spawnPointsReduction = spawnPointsReduction.OrderBy(x => Random.value).ToList();

        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if enemies are still alive
            if (!EnemyIsAlive())
            {
                // Begin new round/wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        // Refreshes the spawn locations available
        if (spawnPointsReduction.Count <= 0)
        {
            spawnPointsReduction = new List<Transform>(spawnPoints);
            spawnPointsReduction = spawnPointsReduction.OrderBy(x => Random.value).ToList();
        }
    }

    // Starts a new Wave if there are more, else it loops back to the first wave
    private void WaveCompleted()
    {
        Debug.Log("wave completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Completed all waves. Looping...");
        }
        else
        {
            nextWave++;
        }
    }

    // Checks if there are enemies alive every second
    private bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    // Spawn the wave with 1 enemy per rate
    private IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        Transform spawningLocation = spawnPointsReduction[0];
        spawnPointsReduction.RemoveAt(0);

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyName, spawningLocation);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    // Allows the enemy to be spawned
    private void SpawnEnemy(string _enemy, Transform location)
    {
        // Spawn Enemy
        Debug.Log("Spawning Enemy: " + _enemy);
        Vector2 randomSpawningPoint = Random.insideUnitCircle * spawnRadius;
        baddies.spawnEnemy(_enemy, location.position + new Vector3(randomSpawningPoint.x, randomSpawningPoint.y, location.position.z));
    }
}
