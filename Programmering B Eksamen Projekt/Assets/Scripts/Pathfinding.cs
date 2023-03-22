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
    public bool bezier;
    public bool bezierLerp;

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
        

        if (!bezierLerp)
        {
            float perc = currentLerpTime / speed;
            transform.position = Vector3.Lerp(startPos, path[index].transform.position, perc);
            if (transform.position == path[index].transform.position)
            {
                startPos = transform.position;
                index--;
                currentLerpTime = 0f;

                if (bezier)
                {
                    bezierLerp = true;
                    bezier = false;
                }
            }

        }
        else if (bezierLerp)
        {
            float perc = currentLerpTime / speed / 1.5f;
            Vector3 A = Vector3.Lerp(startPos, path[index].transform.position, perc);
            Vector3 B = Vector3.Lerp(path[index].transform.position, path[index - 1].transform.position, perc);
            transform.position = Vector3.Lerp(A, B, perc);
            if (transform.position == path[index - 1].transform.position)
            {
                startPos = transform.position;
                index = index - 2;
                currentLerpTime = 0f;

                if (!bezier)
                {
                    bezierLerp = false;
                }
                else if (bezier)
                {
                    bezierLerp = true;
                    bezier = false;
                }
            }
        }
        

        Vector3 newPos = new Vector3(transform.position.x, stats.scale, transform.position.z);
        transform.position = newPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Home"))
        {
            hs.health -= damage;
            stats.currentHealth = 0;
        }

        if (other.gameObject.CompareTag("Bezier"))
        {
            bezier = true;
        }
    }
}
