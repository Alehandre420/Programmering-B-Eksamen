using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    public GameObject basic;
    public GameObject speed;
    public GameObject tank;
    public List<GameObject> enemies;
    public int currentWave = 1;
    public TMP_Text wave;
    public float timeBetweenEnemies;
    float timeBetweenWaves = 0f;

    bool ongoingWave;
    bool waiting = true;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float enemyCount = 2 + 3 * currentWave;
        if (!ongoingWave)
        {
            StartCoroutine(BasicSpawnAndWait(enemyCount));
            if (currentWave >= 8)
            {
                StartCoroutine(SpeedSpawnAndWait(enemyCount));
            }
            if (currentWave >=15)
            {
                StartCoroutine(TankSpawnAndWait());
            }
            
        }
        else if (enemies.Count <= 0 && !waiting)
        {
            StartCoroutine(waitBetweenWaves(5));
        }

        wave.text = $"wave {currentWave.ToString()} / 100";
        if (currentWave >= 100)
        {
            print("you did it");

        }
    }

    private IEnumerator waitBetweenWaves(float waitTime)
    {
        waiting = true;
        
        yield return new WaitForSeconds(waitTime);
        if (currentWave < 100)
        {
           currentWave += 1;
            print($"wave {currentWave} has started");
        }
        gm.money += Mathf.Round(5 * currentWave / 3);
        ongoingWave = false;
    }

    private IEnumerator BasicSpawnAndWait(float amount)
    {
        ongoingWave = true;
        
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(timeBetweenEnemies);
            GameObject basicEnemy = Instantiate(basic, transform);
            basicEnemy.GetComponent<EnemyStats>().maxHealth += 1.2f * currentWave;
            basicEnemy.GetComponent<EnemyStats>().speed -= 0.005f * currentWave ;
            basicEnemy.GetComponent<EnemyStats>().damage += 0.5f * currentWave;
            enemies.Add(basicEnemy);
        }
        waiting = false;
    }
    private IEnumerator SpeedSpawnAndWait(float amount)
    {
        for (int i = 0; i < Mathf.Floor(amount / 2); i++)
        {
            yield return new WaitForSeconds(timeBetweenEnemies * 1.5f);
            GameObject speedEnemy = Instantiate(speed, transform);
            enemies.Add(speedEnemy);
            speedEnemy.GetComponent<EnemyStats>().maxHealth += 0.2f * currentWave;
            speedEnemy.GetComponent<EnemyStats>().speed -= 0.005f * currentWave;
            speedEnemy.GetComponent<EnemyStats>().damage += 0.2f * currentWave;
        }
    }
    private IEnumerator TankSpawnAndWait()
    {
        for (int i = 0; i < Mathf.Floor(currentWave / 10); i++)
        {
            yield return new WaitForSeconds(timeBetweenEnemies * 5);
            GameObject tankEnemy = Instantiate(tank, transform);
            enemies.Add(tankEnemy);
            tankEnemy.GetComponent<EnemyStats>().maxHealth *= currentWave;
            tankEnemy.GetComponent<EnemyStats>().damage *= 0.5f * currentWave;
        }
    }
}
