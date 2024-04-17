using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    private GameObject shooter;

    void Start()
    {
        shooter = transform.parent.gameObject;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collisionObj = collider.gameObject;

        if (collisionObj.CompareTag("Enemy"))
        {
            Enemy enemyHit = collisionObj.GetComponent<Enemy>();
            enemyHit.TakeDamage(damage);

            if (collisionObj == shooter)
            {
                shooter.GetComponent<Enemy>().damageToSelf += damage;
            }
            else if (shooter.CompareTag("Enemy"))
            {
                shooter.GetComponent<Enemy>().damageToOthers += damage;
            }
        }
        else if (collisionObj.CompareTag("Tower"))
        {
            collisionObj.GetComponent<Tower>().TakeDamage(damage);
            shooter.GetComponent<Enemy>().damageToTower += damage;
        }
        Destroy(gameObject);

    }

}
