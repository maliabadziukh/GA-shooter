
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;
    public float bulletSpeed;
    public float reloadTime;
    public GameObject bulletPrefab;
    protected GameObject tank;
    protected Rigidbody2D rbBody;
    protected Transform gunTransform;
    protected Vector3 rotationVector;
    protected float rotationZ;
    public float currentHealth;
    public List<float> DNA = new();
    protected GameController gameController;




    protected virtual void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        tank = transform.Find("Tank").gameObject;
        rbBody = GetComponent<Rigidbody2D>();
        gunTransform = tank.transform.Find("Gun");
    }
    public void SetInitialStats(List<float> stats)
    {
        if (gameObject.CompareTag("Enemy"))
        {
            this.DNA = stats;

            this.health = stats[0] * 250;
            this.damage = stats[1] * 50;
            this.speed = (float)(stats[2] * 1.5);
            this.bulletSpeed = stats[3] * 5;
            this.reloadTime = (float)((1 - stats[4]) * 4);
            this.currentHealth = this.health;
        }
        else
        {
            this.health = stats[0] * 500;
            this.damage = stats[1] * 150;
            this.speed = (1 - stats[2]) * 3;
            this.bulletSpeed = stats[3] * 10;
            this.reloadTime = (1 - stats[4]) * 2;
            this.currentHealth = this.health;
        }

    }
    public void RotateToTarget(Vector3 target)
    {
        rotationVector = target - transform.position;
        rotationZ = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
        tank.transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90);
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }


}
