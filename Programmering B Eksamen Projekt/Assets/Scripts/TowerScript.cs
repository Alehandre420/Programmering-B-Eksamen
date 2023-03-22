using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public float damage;
    float aoeDamage;
    float singleDamage;
    public float atkSpeed;
    public float cost;
    public List<GameObject> enemyList;
    public List<float> enemyHealth;
    public bool isAOE;
    bool isInterrupted;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
            enemyHealth.Add(other.GetComponent<EnemyStats>().currentHealth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemyList.Contains(other.gameObject))
        {
            enemyList.Remove(other.gameObject);
            enemyHealth.Remove(other.GetComponent<EnemyStats>().currentHealth);
        }
    }

    IEnumerator attackEnemy(int i, float tempDamage)
    {
        isInterrupted = true;
        enemyHealth[i] -= tempDamage;
        enemyList[i].GetComponent<EnemyStats>().currentHealth = enemyHealth[i];
        if (enemyHealth[i] <= 0)
        {
            enemyHealth.Remove(enemyHealth[i]);
            enemyList.Remove(enemyList[i]);
        }
        yield return new WaitForSeconds(2/atkSpeed);
        isInterrupted = false;
    }

    private void aoeAttack(float tempDamage)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (!isInterrupted)
            {
                StartCoroutine(attackEnemy(i, tempDamage));
            }
        }
    }
    private void singleAttack(float tempDamage)
    {
        if (!isInterrupted && enemyList.Count > 0)
        {
            StartCoroutine(attackEnemy(0, tempDamage));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }
        }
        if (isAOE)
        {
            aoeDamage = damage / 2;
            aoeAttack(aoeDamage);
        }
        else
        {
            singleDamage = damage * 2;
            singleAttack(singleDamage);
        }
        
    }
}
