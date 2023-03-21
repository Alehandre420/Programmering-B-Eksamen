using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    HomeStats hs;
    EnemyStats stats;

    float speed;
    float worth;
    float damage;

    public GameObject[] path;
    int index;
    float currentLerpTime;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HomeStats>();
        stats = FindObjectOfType<EnemyStats>();
        path = GameObject.FindGameObjectsWithTag("Path");

        //getting the stats from the enemy
        speed = stats.speed;
        damage = stats.damage;

        index = path.Length - 1;
        startPos = path[index].transform.position;
        currentLerpTime = 0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > speed)
        {
            currentLerpTime -= speed;
            startPos = path[index].transform.position;
            index--;
        }

        float perc = currentLerpTime / speed;
        transform.position = Vector3.Lerp(startPos, path[index].transform.position, perc);
        if (transform.position == path[index].transform.position)
        {
            startPos = transform.position;
            index--;
            currentLerpTime = 0f;
        }

        Vector3 newPos = new Vector3(transform.position.x, 1.5f, transform.position.z);
        transform.position = newPos;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Home"))
        {
            hs.health = hs.health - damage;
            Destroy(gameObject);
        }
    }
}
