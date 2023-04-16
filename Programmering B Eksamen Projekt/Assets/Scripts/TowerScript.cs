using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    public float damage;
    float aoeDamage;
    float singleDamage;
    public float atkSpeed;
    public float aoeRange;
    public float singleRange;
    public float cost;
    public LayerMask towerMask;
    public List<GameObject> enemyList;
    public List<float> enemyHealth;
    public bool isAOE;
    bool isInterrupted;
    CapsuleCollider towerCollider;
    TMP_Text levelText;
    ButtonScript buttonScript;
    LevelTextScript levelTextScript;

    bool attackFirst;
    bool attackLast;
    bool attackStrong;

    private void Awake()
    {
        towerCollider = GetComponent<CapsuleCollider>();

        buttonScript = transform.GetComponentInChildren<ButtonScript>();
        levelTextScript = transform.GetComponentInChildren<LevelTextScript>();
        levelText = levelTextScript.gameObject.GetComponent<TMP_Text>();
        attackFirst = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemyList.Contains(other.gameObject))
        {
            enemyList.Add(other.gameObject);
            enemyHealth.Add(other.GetComponent<EnemyStats>().currentHealth);
            bubbleSort();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && enemyList.Contains(other.gameObject))
        {
            enemyList.Remove(other.gameObject);
            enemyHealth.Remove(other.GetComponent<EnemyStats>().currentHealth);
            bubbleSort();
        }
    }

    IEnumerator attackEnemy(int i, float tempDamage)
    {
        isInterrupted = true;
        enemyHealth[i] = enemyList[i].GetComponent<EnemyStats>().currentHealth;
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
        if (!isInterrupted)
        {
            for (int i = 0; i < enemyList.Count; i++)
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

    public void aoeChange()
    {
        if (isAOE)
        {
            isAOE = false;
        }
        else if (!isAOE)
        {
            isAOE = true;
        }
    }

    public void changeSort()
    {
        if (attackFirst)
        {
            attackFirst = false;
            attackLast = true;
        }
        else if (attackLast)
        {
            attackLast = false;
            attackStrong = true;
        }
        else if (attackStrong)
        {
            attackStrong = false;
            attackFirst = true;
        }

        bubbleSort();
    }
    
    public void bubbleSort()
    {
        int n = enemyList.Count;
        bool swapped = false;

        if (attackFirst)
        {
            for (int i = 1; i < n - 1; i++)
            {
                if (enemyList[i - 1].GetComponent<EnemyStats>().timeAlive < enemyList[i].GetComponent<EnemyStats>().timeAlive)
                {
                    float timp = enemyHealth[i];
                    GameObject temp = enemyList[i];

                    enemyHealth[i - 1] = enemyHealth[i];
                    enemyList[i - 1] = enemyList[i];

                    enemyHealth[i] = timp;
                    enemyList[i] = temp;

                    swapped = true;
                }
            }
        }
        else if (attackLast)
        {
            for (int i = 1; i < n - 1; i++)
            {
                if (enemyList[i - 1].GetComponent<EnemyStats>().timeAlive > enemyList[i].GetComponent<EnemyStats>().timeAlive)
                {
                    float timp = enemyHealth[i];
                    GameObject temp = enemyList[i];

                    enemyHealth[i - 1] = enemyHealth[i];
                    enemyList[i - 1] = enemyList[i];

                    enemyHealth[i] = timp;
                    enemyList[i] = temp;

                    swapped = true;
                }
            }
        }
        else if (attackStrong)
        {
            for (int i = 1; i < n - 1; i++)
            {
                if (enemyList[i - 1].GetComponent<EnemyStats>().maxHealth < enemyList[i].GetComponent<EnemyStats>().maxHealth)
                {
                    float timp = enemyHealth[i];
                    GameObject temp = enemyList[i];

                    enemyHealth[i - 1] = enemyHealth[i];
                    enemyList[i - 1] = enemyList[i];

                    enemyHealth[i] = timp;
                    enemyList[i] = temp;

                    swapped = true;
                }
            }
        }

        if (swapped)
        {
            bubbleSort();
        }

    }

    void Update()
    {
        levelText.text = "Level " + buttonScript.level;
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                enemyHealth.RemoveAt(i);
            }
        }
        for (int i = 0; i < enemyHealth.Count; i++)
        {
            if (enemyList.Count < 1)
            {
                enemyHealth.RemoveAt(i);
            }
        }
        if (isAOE)
        {
            towerCollider.radius = aoeRange;
            aoeDamage = damage;
            aoeAttack(aoeDamage);
        }
        else if (!isAOE)
        {
            towerCollider.radius = singleRange;
            singleDamage = damage * 1.5f;
            singleAttack(singleDamage);
        }
    }
}
