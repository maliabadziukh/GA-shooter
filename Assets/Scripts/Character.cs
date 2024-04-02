
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class Character : MonoBehaviour
{
    public float health;
    public float damage;
    public float speed;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    protected Rigidbody2D rbBody;
    protected Transform gunTransform;
    protected Vector3 rotationVector;
    protected float rotationZ;
    protected float currentHealth;


    protected virtual void Awake()
    {
        rbBody = GetComponent<Rigidbody2D>();
        gunTransform = transform.Find("Gun");
    }

    protected virtual void Start()
    {
        StartCoroutine(ShootTarget());

    }

    protected virtual void Update()
    {
        rotationVector.Normalize();

    }

    public void SetInitialStats(float health, float damage, float speed, float bulletSpeed)
    {
        this.health = health;
        this.damage = damage;
        this.speed = speed;
        this.bulletSpeed = bulletSpeed;
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
        yield return new WaitForSeconds(1.0f);
        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(gunTransform.up * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(ShootTarget());

    }



}
