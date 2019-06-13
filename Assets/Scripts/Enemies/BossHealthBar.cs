using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{

    Vector3 localScale;
    public Enemy boss;

    void Start()
    {
        localScale = transform.localScale;
    }

    void Update()
    {
        localScale.x = boss.currentHealth;
        transform.localScale = localScale;
    }
}
