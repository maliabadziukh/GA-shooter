
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class Character : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;
    public float bulletSpeed;
    public float reloadTime;
    public GameObject bulletPrefab;
    protected Rigidbody2D rbBody;
    protected Transform gunTransform;
    protected Vector3 rotationVector;
    protected float rotationZ;
    protected float currentHealth;
    protected float initializationTime;
    protected float timeSurvived;


    protected virtual void Awake()
    {
        rbBody = GetComponent<Rigidbody2D>();
        gunTransform = transform.Find("Gun");
    }

    protected virtual void Start()
    {
        StartCoroutine(ShootTarget());
        initializationTime = Time.timeSinceLevelLoad;
    }

    protected virtual void Update()
    {
        rotationVector.Normalize();
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public void SetInitialStats(List<float> stats)
    {
        this.health = stats[0] * 1000;
        this.damage = stats[1] * 100;
        this.speed = stats[2] * 3;
        this.bulletSpeed = stats[3] * 10;
        this.reloadTime = (1 - stats[4]) * 5;
        this.currentHealth = this.health;
    }
    public void RotateToTarget(Vector3 target)
    {
        rotationVector = target - transform.position;
        rotationZ = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ - 90);
    }

    public void MoveInDirection(Vector2 direction)
    {
        rbBody.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        print(gameObject.tag + " :" + currentHealth);
    }

    public IEnumerator ShootTarget()
    {
        yield return new WaitForSeconds(reloadTime);
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(gunTransform.up * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(ShootTarget());

    }

    public void Die()
    {
        GameObject.Find("GameController").GetComponent<GameController>().spawnedEnemies.Remove(gameObject);
        timeSurvived = Time.timeSinceLevelLoad - initializationTime;
        print("I survived for: " + timeSurvived);
        Destroy(gameObject);
    }
}
