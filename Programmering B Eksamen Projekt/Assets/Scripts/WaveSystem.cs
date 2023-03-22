using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public GameObject basic;
    public List<GameObject> enemies;
    int currentWave = 1;

    bool ongoingWave;

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
        else if (enemies.Count <= 0)
        {
            ongoingWave = false;
        }

    }

    private IEnumerator SpawnAndWait(float amount)
    {
        ongoingWave = true;
        for (int i = 0; i < amount; i++)
        {
            GameObject enemy = Instantiate(basic, transform);
            enemies.Add(enemy);
            yield return new WaitForSeconds(1.3f);
        }
        currentWave += 1;
    }
}
