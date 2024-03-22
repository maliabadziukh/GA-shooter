
using Unity.VisualScripting;
using UnityEngine;


public class Enemy : Character
{
    Transform playerTransform;
    private Vector2 normalizedRotation;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        playerTransform = FindObjectOfType<Player>().GameObject().transform;
    }

    protected override void Update()
    {
        base.Update();
        RotateToTarget(playerTransform.position);

    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > 1)
        {
            MoveInDirection(rotationVector);
            print("enemy moved");

        }
    }


}
