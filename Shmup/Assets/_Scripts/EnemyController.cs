using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : SteerableBehaviour, IShooter, IDamageable
{
    private float lifes;
    private float maxlifes = 3f;
    GameManager gm;
    public HealthBar HealthBar;
    public AudioClip explosionSFX;
    private Animator anim;
    private void Start()
    {
        lifes = maxlifes;
        gm = GameManager.GetInstance();
        HealthBar.SetHealth(lifes, maxlifes);
        anim = GetComponent<Animator>();
    }
    public GameObject tiro;
    public void Shoot()
    {
        Instantiate(tiro, transform.position, Quaternion.identity);
        //throw new System.NotImplementedException();
    }

    public void TakeDamage()
    {
        lifes--;
        HealthBar.SetHealth(lifes, maxlifes);

        if(lifes <= 0) Die();
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void Die()
    {
        anim.SetTrigger("Death");
        AudioManager.PlaySFX(explosionSFX);
        gm.pontos += 20;
    }

    float angle = 0;

    private void FixedUpdate()
    {        
        if (gm.gameState != GameManager.GameState.GAME || lifes <= 0) return;
        
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
            Die();
        }
    }  
}
