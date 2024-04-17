
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
    private float fitnessScore;

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

    private void Die()
    {
        print("oh no i'm dead");
        //reset health so Die() doesn't get called multiple times
        currentHealth = 1000;
        gameController.spawnedEnemies.Remove(gameObject);
        evolutionManager.LogSpecimen(DNA, Fitness());
        Destroy(gameObject);
    }

    private float Fitness()
    {
        timeSurvived = Time.timeSinceLevelLoad - initializationTime;
        return timeSurvived;
    }
}
