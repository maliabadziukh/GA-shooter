using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform spawnLocation;
    public int numberOfEnemies = 5;
    public int waveDuration = 30;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    public List<List<float>> enemiesToSpawn = new();
    public List<GameObject> spawnedEnemies = new();


    void Start()
    {
        print("START");
        StartNewWave();
        GameObject playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        player.SetInitialStats(new() { 100, 5, 1, 3 });
    }
    void FixedUpdate()
    {
        print("FIXED UPDATE STARTING");

        if (spawnTimer <= 0)
        {
            //spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().SetInitialStats(enemiesToSpawn[0]);
                enemiesToSpawn.RemoveAt(0); // and remove it
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0; // if no enemies remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
        if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            StartNewWave();
        }
        print("FIXED UPDATE RAN");

    }
    public void StartNewWave()
    {
        print("START NEW WAVE");

        GenerateRandomEnemies();
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
        print("NEW WAVE STARTED");

    }

    public void GenerateRandomEnemies()
    {
        print("GENERATE RANDOM ENEMIES");

        List<List<float>> generatedEnemies = new();
        while (generatedEnemies.Count < numberOfEnemies)
        {
            List<float> newEnemy = new() { Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100) };
            generatedEnemies.Add(newEnemy);
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
        print("RANDOM ENEMIES GENERATED");

    }
}
