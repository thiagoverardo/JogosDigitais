using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolantBehaviour : SteerableBehaviour, IDamageable
{
    private int lifes;
    float angle = 0;

    private void Start()
    {
        lifes = 2;
    }
    private void FixedUpdate()
    {
        angle += 0.1f;
        if(angle > 2.0f * Mathf.PI) angle = 0.0f;
        Thrust(0, Mathf.Cos(angle));
    }
    public void TakeDamage()
    {
        lifes--;
        if (lifes <= 0) Die();
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}