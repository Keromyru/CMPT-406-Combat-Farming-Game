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
    // The radius the enemies can spawn randomly within
    private float spawnRadius = 6f;

    // Will be replaced later for enemys spawning at night
    public float timeBetweenWaves;
    public float waveCountdown;
    public int dayCount = 0;

    // Checks if there are enemies every second given (default is checks if there are enemies every 1 second)
    // Basically to lower resource cost on computer
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    // The database that holds all the enemies
    public EnemyDatabaseSO baddies;

    // Checks if enemies are still alive
    public bool aliveEnemies = false;


    // Bool to check if it is night time
    public bool isNight = true;

    private void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced");
        }
        List<Transform> spawnPointsReduction = new List<Transform>(spawnPoints);
        spawnPointsReduction = spawnPointsReduction.OrderBy(x => Random.value).ToList();
        EnemySpawnList.setList(spawnPointsReduction);
        
        waveCountdown = timeBetweenWaves;
        DayNightCycle.isNowDay += onDay;
        DayNightCycle.isNowNight += onNight;
    }

    private void Update()
    {
        if (isNight)
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
                if (dayCount != 0)
                {
                    if (state != SpawnState.SPAWNING)
                    {
                        // Start spawning wave
                        StartCoroutine(SpawnWave(waves[nextWave]));
                    }
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
        else if (aliveEnemies)
        {
            onDay();
        }

        // Refreshes the spawn locations available
        if (EnemySpawnList.getList().Count <= 0)
        {
            List<Transform> spawnPointsReduction = new List<Transform>(spawnPoints);
            spawnPointsReduction = spawnPointsReduction.OrderBy(x => Random.value).ToList();
            EnemySpawnList.setList(spawnPointsReduction);
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
        aliveEnemies = true;
        return true;
    }

    // Spawn the wave with 1 enemy per rate
    private IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.waveName);
        state = SpawnState.SPAWNING;

        Transform spawningLocation = EnemySpawnList.getFirstSpawn();

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            if (!isNight)
            {
                state = SpawnState.WAITING;
                yield break;
            }

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
        Vector2 randomSpawningPoint = new Vector2(Random.insideUnitCircle.x * spawnRadius, Random.insideUnitCircle.y * spawnRadius / 3.5f);
        baddies.spawnEnemy(_enemy, location.position + new Vector3(randomSpawningPoint.x, randomSpawningPoint.y, location.position.z));
    }

    // Used to upgrade the enemies count and their stats after each night
    private void upgradeEnemies()
    {
        if (dayCount != 0)
        {
            if (dayCount % 1 == 0)
            {
                upgradeEnemiesAssist("Crawlie");
            }
            if (dayCount % 2 == 0)
            {
                upgradeEnemiesAssist("SplitStrider");
            }
            if (dayCount % 3 == 0)
            {
                upgradeEnemiesAssist("Fly Boy");
            }
        }
    }

    // Used only by upgradeEnemies
    private void upgradeEnemiesAssist(string name)
    {
        foreach (Wave theWave in waves)
        {
            if (theWave.enemyName == name)
            {
                theWave.count += 1;
            }
        }
    }

    // Deletes all enemies
    private void ClearAllEnemies()
    {
        System.Array.ForEach(GameObject.FindGameObjectsWithTag("Enemy"), b => Destroy(b));
    }

    // When day hits all enemies are deleted (This will need to be implemented to day night cycle)
    private void onDay()
    {
        EnemySpawnList.removeFirstSpawn();
        isNight = false;
        aliveEnemies = false;
        ClearAllEnemies();
        upgradeEnemies();
        waveCountdown = timeBetweenWaves;
    }

    // When night hits enemies start to spawn (This will need to be implemented to day night cycle)
    private void onNight()
    {
        isNight = true;
        dayCount += 1;
    }
}