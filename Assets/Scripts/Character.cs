
using UnityEngine;
using UnityEngine.UIElements;
public class Character : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;
    public Rigidbody2D rigidBody;
    protected Vector3 rotationVector;
    private float rotationZ;

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

    }

    protected virtual void Update()
    {
        rotationVector.Normalize();

    }

    public void SetInitialStats(float health, float damage, float speed)
    {
        this.health = health;
        this.damage = damage;
        this.speed = speed;
    }
    public void RotateToTarget(Vector3 target)
    {
        rotationVector = target - transform.position;
        rotationZ = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90);
    }

    public void MoveInDirection(Vector2 direction)
    {
        rigidBody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    public void Shoot()
    {

    }



}
