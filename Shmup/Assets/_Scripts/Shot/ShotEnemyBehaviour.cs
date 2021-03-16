using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyBehaviour : SteerableBehaviour
{

    private Vector3 direction;
    GameManager gm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigos") || collision.CompareTag("Inimigos2") || collision.CompareTag("Powerup") || collision.CompareTag("Powerup2")) return;

        IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if (!(damageable is null))
        {
            damageable.TakeDamage();
        }
        Destroy(gameObject);
    }

    void Start()
    {
        gm = GameManager.GetInstance();
        if(GameObject.FindWithTag("Player")){
            Vector3 posPlayer = GameObject.FindWithTag("Player").transform.position;
            direction =  (posPlayer - transform.position).normalized;
        }
    }

    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        Thrust(direction.x, direction.y);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
