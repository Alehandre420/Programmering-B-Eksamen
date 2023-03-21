using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float speed;
    public float worth;
    public float damage;

    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            gm.money += worth;
            Destroy(gameObject);
        }
    }
}
