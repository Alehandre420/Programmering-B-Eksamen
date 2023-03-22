using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameManager gm;
    public TowerScript ts;
    public LayerMask tile;

    private void OnMouseDown()
    {
        if (gm.money >= ts.cost)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        

        if (Physics.Raycast(rayToMouse, out hit, Mathf.Infinity, tile) && Input.GetMouseButtonDown(0))
        {
            if (hit.transform.childCount != 0)
                return;

            GameObject tower = Instantiate(towerPrefab, hit.transform);
            tower.transform.localPosition = new Vector3(0, 1, 0);
        }


    }
}
