using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    EnemyStats stats;
    Slider healthBar;
    Camera target;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponentInParent<EnemyStats>();
        target = FindObjectOfType<Camera>();
        healthBar = GetComponent<Slider>();

        healthBar.maxValue = stats.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);

        healthBar.value = stats.currentHealth;
    }
}
