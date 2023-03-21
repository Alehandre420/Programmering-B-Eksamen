using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float damage;
    public float atkSpeed;
    public float cost;

    bool enemyInRadius;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyInRadius = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
