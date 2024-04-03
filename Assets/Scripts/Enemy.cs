
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Character
{
    Transform playerTransform;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        playerTransform = FindObjectOfType<Player>().GameObject().transform;
    }

    protected override void Update()
    {
        base.Update();

    }

    void FixedUpdate()
    {
        RotateToTarget(playerTransform.position);
        if (Vector2.Distance(transform.position, playerTransform.position) > 1)
        {
            MoveInDirection(rotationVector);
        }
    }


}
