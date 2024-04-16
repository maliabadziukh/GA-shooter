
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Character
{

    Transform towerTransform;
    EvolutionManager evolutionManager;
    private float timeSurvived;
    private float initializationTime;

    protected override void Start()
    {
        base.Start();
        towerTransform = FindObjectOfType<Tower>().GameObject().transform;
        evolutionManager = GameObject.Find("GameController").GetComponent<EvolutionManager>();
        initializationTime = Time.timeSinceLevelLoad;
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
        RotateToTarget(towerTransform.position);
        if (Vector2.Distance(transform.position, towerTransform.position) > 2)
        {
            MoveInDirection(rotationVector);
        }
    }
    public void MoveInDirection(Vector2 direction)
    {
        rbBody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
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
