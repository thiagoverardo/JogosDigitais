using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : SteerableBehaviour
{
    GameManager gm;
    void Start()
    {
        gm = GameManager.GetInstance();
    }
    private void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME ) return;
        Thrust(2,0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Powerup") || collision.CompareTag("Powerup2")) return;
        IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;

        if (!(damageable is null))
        {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
   }
   private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}