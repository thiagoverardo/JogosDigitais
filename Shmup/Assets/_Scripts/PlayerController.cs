using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    Animator animator;
    public AudioClip shootSFX;
   
    private int lifes;
    private void Start()
    {
        lifes = 2;
        animator = GetComponent<Animator>();
    }
    public GameObject bullet;
    public Transform arma01;
    public float shootDelay = 0.1f;
    private float _lattShootTimestamp = 0.0f;
    public void Shoot()
    {
        if (Time.time - _lattShootTimestamp < shootDelay) return;
        _lattShootTimestamp = Time.time;
        
        Instantiate(bullet, arma01.transform.position, Quaternion.identity);
        AudioManager.PlaySFX(shootSFX);
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

    void FixedUpdate()
    {
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");
        Thrust(xInput, yInput);
        if (yInput != 0 || xInput != 0)
        {
            animator.SetFloat("Velocity", 1.0f);
        }
        if(Input.GetAxisRaw("Fire1") != 0)
        {
            Shoot();
        }
        else
        {
            animator.SetFloat("Velocity", 0.0f);
        }
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigos"))
        {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }    
}
