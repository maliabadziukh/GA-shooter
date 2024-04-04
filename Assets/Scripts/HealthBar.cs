using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    private Character character;
    float initialHealth;
    GameObject healthBar;
    void Start()
    {


        healthBar = transform.Find("Health").gameObject;
        character = transform.parent.gameObject.GetComponent<Enemy>();
        if (character == null)
        {
            character = transform.parent.gameObject.GetComponent<Player>();
        }
        initialHealth = character.health;

    }

    void Update()
    {
        float fillAmount = character.currentHealth / initialHealth * 0.9f;
        healthBar.transform.localScale = new Vector3(fillAmount, 0.75f, 1);
    }
}
