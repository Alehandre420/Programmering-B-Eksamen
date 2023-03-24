using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    TowerScript towerScript;
    GameManager gameManager;
    
    private void Awake()
    {
        towerScript = transform.GetComponentInParent<TowerScript>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Upgrade()
    {
        if (gameManager.money >= towerScript.cost * 0.5f)
        {
            towerScript.damage *= 1.1f;
            towerScript.atkSpeed *= 1.1f;
            gameManager.money -= Mathf.Round(towerScript.cost * 0.5f);
            towerScript.cost *= 1.2f;
        }
        else
        {
            print("not enough money");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
