using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    EnemyStats stats;
    float maxH;
    float curH;

    Camera target;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        maxH = stats.maxHealth;
        target = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);

        curH = stats.currentHealth;


    }
}
