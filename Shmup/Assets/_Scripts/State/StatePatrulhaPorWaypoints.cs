using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrulhaPorWaypoints : State, IDamageable
{
    public Transform[] waypoints;  
    Rigidbody2D rb;
    private float lifes;
    private float maxlifes = 2f;
    GameManager gm;
    public HealthBar HealthBar;
    public AudioClip explosionSFX;

    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        // Configure a transição para outro estado aqui.
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        lifes = maxlifes;
        HealthBar.SetHealth(lifes, maxlifes);
        gm = GameManager.GetInstance();
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        
        if(GameObject.FindWithTag("Player")){
            Vector3 screenBounds = GameObject.FindWithTag("Player").transform.position;

            if(transform.position.x < (screenBounds.x - 15)){
                Destroy(gameObject);
            }
        
            else{
                if(Vector3.Distance(transform.position, screenBounds) > .1f) {
                    Vector3 direction = screenBounds - transform.position;
                    direction.Normalize();
                    rb.MovePosition(rb.position + new Vector2(direction.x, direction.y) * Time.fixedDeltaTime);
                } else {
                    screenBounds = GameObject.FindWithTag("Player").transform.position;
                }
            }
        }
        else{
            Destroy(gameObject);
        }
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
        gm.pontos += 10;
    }
 
}