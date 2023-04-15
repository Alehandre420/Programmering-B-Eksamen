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

    private void Awake()
    {
        towerCollider = GetComponent<CapsuleCollider>();
        buttonScript = transform.GetComponentInChildren<ButtonScript>();
        levelTextScript = transform.GetComponentInChildren<LevelTextScript>();
        levelText = levelTextScript.gameObject.GetComponent<TMP_Text>();
        /*for (int i = 0; i < transform.hierarchyCount; i++)
        {
            print("hej");
            if (transform.GetChild(i).CompareTag("LevelTag"))
            {
                levelText = transform.GetChild(i).GetComponent<TextMeshPro>();
            }
        }*/
    }
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
