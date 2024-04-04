using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform spawnLocation;
    public int numberOfEnemies = 5;
    public int waveDuration = 100;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    public List<List<float>> enemiesToSpawn = new();
    public List<GameObject> spawnedEnemies = new();


    void Start()
    {
        StartNewWave();
        GameObject playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        player.SetInitialStats(NormalizeStats(new() { 100, 100, 100, 100, 100 }));

    }
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().SetInitialStats(enemiesToSpawn[0]);
                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
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

    }
    public void StartNewWave()
    {

        GenerateRandomEnemies();
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;

    }

    public void GenerateRandomEnemies()
    {

        List<List<float>> generatedEnemies = new();
        while (generatedEnemies.Count < numberOfEnemies)
        {
            List<float> newEnemy = NormalizeStats(new() { Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100), Random.Range(1, 100) });
            generatedEnemies.Add(newEnemy);
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

    }

    public List<float> NormalizeStats(List<float> rawStats)
    {
        float sum = 0;
        List<float> normalizedStats = new();

        foreach (float stat in rawStats)
        {
            sum += stat;
        }
        foreach (float stat in rawStats)
        {
            normalizedStats.Add(stat / sum);
        }
        return normalizedStats;



    }
}
