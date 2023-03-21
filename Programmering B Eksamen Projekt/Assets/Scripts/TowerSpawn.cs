using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameManager gm;
    public TowerScript ts;

    private void OnMouseDown()
    {
        if (gm.money >= ts.cost)
        {
            GameObject tower = Instantiate(towerPrefab, transform);
            tower.transform.localPosition = new Vector3(0, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
