using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collision = collider.gameObject;
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(this.damage);
        }
        else if (collision.CompareTag("Tower"))
        {
            collision.GetComponent<Tower>().TakeDamage(this.damage);
        }
        Destroy(gameObject);

    }

}
