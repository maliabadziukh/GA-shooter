using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform spawnLocation;
    public int waveDuration = 100;
    private float waveTimer;
    public int waveIndex = 0;
    private float spawnInterval;
    private float spawnTimer;
    public List<List<float>> enemiesToSpawn = new();
    public List<GameObject> spawnedEnemies = new();
    private EvolutionManager evolutionManager;


    void Start()
    {
        evolutionManager = GetComponent<EvolutionManager>();
        GameObject playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        player.SetInitialStats(evolutionManager.NormalizeDNA(new() { 100, 100, 100, 100, 100 }));
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
            enemiesToSpawn.Clear();
            waveIndex++;
            print("Starting wave " + waveIndex);
            StartWave();
        }

    }
    public void StartWave()
    {
        enemiesToSpawn = evolutionManager.CreateGeneration();
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }




}
