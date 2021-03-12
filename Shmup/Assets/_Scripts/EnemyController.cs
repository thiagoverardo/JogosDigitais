using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SteerableBehaviour, IShooter, IDamageable
{
    private float lifes;
    private float maxlifes = 3f;
    GameManager gm;
    public HealthBar HealthBar;
    private void Start()
    {
        lifes = maxlifes;
        gm = GameManager.GetInstance();
        HealthBar.SetHealth(lifes, maxlifes);
    }
    public GameObject tiro;
    public void Shoot()
    {
        Instantiate(tiro, transform.position, Quaternion.identity);
        //throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        gm.pontos += 10;
        lifes--;
        HealthBar.SetHealth(lifes, maxlifes);

        if(lifes <= 0) Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    float angle = 0;

    private void FixedUpdate()
    {        
        if (gm.gameState != GameManager.GameState.GAME) return;
        
        if(GameObject.FindWithTag("Player")){
            Vector3 screenBounds = GameObject.FindWithTag("Player").transform.position;        
            if(transform.position.x < (screenBounds.x - 15)){
                Destroy(gameObject);
            }
            angle += 0.1f;
            Mathf.Clamp(angle, 0.0f, 2.0f * Mathf.PI);
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            Thrust(x - 1f, y);
        }
        else{
            Destroy(gameObject);
        }              
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeDamage();
        }
    }  
}
