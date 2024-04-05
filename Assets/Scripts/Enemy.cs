
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Character
{

    Transform playerTransform;
    GameController gameController;
    EvolutionManager evolutionManager;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        playerTransform = FindObjectOfType<Player>().GameObject().transform;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        evolutionManager = GameObject.Find("GameController").GetComponent<EvolutionManager>();
    }

    private void Update()
    {
        if (currentHealth < 0)
        {
            Die();
        }

    }

    void FixedUpdate()
    {
        rotationVector.Normalize();
        RotateToTarget(playerTransform.position);
        if (Vector2.Distance(transform.position, playerTransform.position) > 2)
        {
            MoveInDirection(rotationVector);
        }
    }

    public void Die()
    {
        print("oh no i'm dead");
        currentHealth = 1000;
        //remove from currently spawned for correct wave management
        gameController.spawnedEnemies.Remove(gameObject);

        // check if specimen DNA should be added to best specimen in evolution manager for later respwaning
        timeSurvived = Time.timeSinceLevelLoad - initializationTime;
        evolutionManager.CheckFitness(timeSurvived, DNA);

        Destroy(gameObject);
    }

}
