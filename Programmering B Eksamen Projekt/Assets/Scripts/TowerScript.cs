using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float damage;
    public float atkSpeed;
    public float cost;
    public List<GameObject> enemyList;
    public EnemyStats enemyStats;

    bool enemyInRadius;
    bool isInterrupted;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            enemyInRadius = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemyList.Contains(other.gameObject))
        {
            enemyList.Remove(other.gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            enemyInRadius = false;
        }
    }

    IEnumerator attackEnemy()
    {
        isInterrupted = true;
        print("gtims");
        //remove 10 health from enemy in list
        yield return new WaitForSeconds(2);
        isInterrupted = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!isInterrupted)
            {
                StartCoroutine(attackEnemy());
            }
        }
    }
}
