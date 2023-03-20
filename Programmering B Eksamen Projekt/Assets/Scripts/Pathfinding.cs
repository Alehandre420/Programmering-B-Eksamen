using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    HomeStats hs;
    public GameObject[] path;
    int index = 0;
    public float speed = 2;
    float currentLerpTime;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        hs = FindObjectOfType<HomeStats>();
        path = GameObject.FindGameObjectsWithTag("Path");
        startPos = transform.position;
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
            index++;
        }

        float perc = currentLerpTime / speed;
        transform.position = Vector3.Lerp(startPos, path[index].transform.position, perc);
        if (transform.position == path[index].transform.position)
        {
            startPos = transform.position;
            index++;
            currentLerpTime = 0f;
        }


        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Home"))
        {
            hs.health--;
            Destroy(this);
        }
    }
}
