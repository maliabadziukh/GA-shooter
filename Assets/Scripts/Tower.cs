
using System.Collections;
using UnityEngine;


public class Tower : Character
{
    private Vector3 target;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Rotate());
        StartCoroutine(Shoot());

    }


    private void Update()
    {

        if (currentHealth < 0)
        {
            print("Tower was killed, resetting health...");
            currentHealth = health;

        }
    }

    public IEnumerator Rotate()
    {
        yield return new WaitForSeconds(speed);

        if (gameController.spawnedEnemies.Count != 0)
        {
            target = gameController.spawnedEnemies[Random.Range(0, gameController.spawnedEnemies.Count)].transform.position;
            print(target);
            RotateToTarget(target);
        }
        StartCoroutine(Rotate());
    }
    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(reloadTime);

        GameObject bullet = Instantiate(bulletPrefab, gunTransform.position, gunTransform.rotation);
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(gunTransform.up * bulletSpeed, ForceMode2D.Impulse);
        StartCoroutine(Shoot());

    }
}

