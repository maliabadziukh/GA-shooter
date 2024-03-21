using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;


    void Start()
    {
        GameObject playerObj = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        player.SetInitialStats(100, 5, 1);

        GameObject enemyObj = Instantiate(enemyPrefab, new Vector3(-7, -2, -1), Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.SetInitialStats(100, 5, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
