using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float simulationSpeed = 1;
    public GameObject towerPrefab;
    public List<float> towerDNA = new() { 100, 100, 100, 100, 100 };
    public GameObject enemyPrefab;
    public List<Transform> spawnOptions;
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
        Time.timeScale = simulationSpeed;
        evolutionManager = GetComponent<EvolutionManager>();
        GameObject towerObj = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
        Tower tower = towerObj.GetComponent<Tower>();
        tower.SetInitialStats(evolutionManager.NormalizeDNA(towerDNA));

        print("Tower DNA is:");
        evolutionManager.PrintDNA(tower.DNA);
    }
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                spawnLocation = spawnOptions[Random.Range(0, spawnOptions.Count)];
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
