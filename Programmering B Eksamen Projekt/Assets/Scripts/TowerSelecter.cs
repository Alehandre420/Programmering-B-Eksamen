using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelecter : MonoBehaviour
{
    public LayerMask towerMask;

    // Update is called once per frame
    void Update()
    {
        Ray rayToMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rayToMouse, out hit, Mathf.Infinity, towerMask) && Input.GetMouseButtonDown(1))
        {
            GameObject btn = hit.transform.Find("Canvas").gameObject;
            if (btn.activeInHierarchy)
            {
                btn.SetActive(false);
            }
            else if (!btn.activeInHierarchy)
            {
                btn.SetActive(true);
            }
        }
    }
}
