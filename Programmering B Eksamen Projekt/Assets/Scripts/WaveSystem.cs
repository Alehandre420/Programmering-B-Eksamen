using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSystem : MonoBehaviour
{
    public GameObject basic;
    public List<GameObject> enemies;
    int currentWave = 1;
    public TMP_Text wave;
    public float timeBetweenEnemies;
    float timeBetweenWaves = 0f;

    bool ongoingWave;
    bool waiting;
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
            StartCoroutine(SpawnAndWait(enemyCount));
        }
        else if (enemies.Count <= 0 && !waiting)
        {
            timeBetweenWaves += Time.deltaTime;

            if (enemies.Count > 0)
            {
                timeBetweenWaves = 0f;
            }
            else if (timeBetweenWaves > timeBetweenEnemies)
            {
                StartCoroutine(waitBetweenWaves(5));
            }
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
        if (enemies.Count <= 0)
        {
            ongoingWave = false;
            if (currentWave < 100)
            {
                currentWave += 1;
            }
            gm.money += Mathf.Round(5 * currentWave / 3);
        }
        waiting = false;
    }

    private IEnumerator SpawnAndWait(float amount)
    {
        ongoingWave = true;
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(basic, transform);
            enemies.Add(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }
}
