using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeStats : MonoBehaviour
{
    public float health = 100;
    public TMP_Text hp;
    public GameObject camra;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hp.text = health.ToString();
        if (health <= 0)
        {
            Destroy(camra);
        }
    }

   
}
