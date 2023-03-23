using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject basic;
    public List<GameObject> enemies;
    int currentWave = 1;

    bool ongoingWave;
    bool waiting;

    // Start is called before the first frame update
    void Start()
    {
        
        
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
            StartCoroutine(waitBetweenWaves(5));
        }

    }

    private IEnumerator waitBetweenWaves(float waitTime)
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        ongoingWave = false;
        waiting = false;
    }

    private IEnumerator SpawnAndWait(float amount)
    {
        ongoingWave = true;
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(basic, transform);
            enemies.Add(enemy);
            yield return new WaitForSeconds(0.8f);
        }
        currentWave += 1;
    }
}
