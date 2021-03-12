using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable
{
    Animator animator;
    private float maxlifes = 5f;
    GameManager gm;
    public HealthBar HealthBar;
    public AudioClip shootSFX;
    public GameObject bullet;
    public Transform arma01;
    public float shootDelay = 0.1f;
    private float _lattShootTimestamp = 0.0f;
    private void Start()
    {
        gm = GameManager.GetInstance();
        HealthBar.SetHealth(gm.vidas, maxlifes);

        animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        if (Time.time - _lattShootTimestamp < shootDelay) return;
        _lattShootTimestamp = Time.time;
        
        Instantiate(bullet, arma01.transform.position, Quaternion.identity);
        AudioManager.PlaySFX(shootSFX);
    }

    public void TakeDamage()
    {
        gm.vidas--;
        HealthBar.SetHealth(gm.vidas, maxlifes);
        if (gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME){
            Die();
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    private float pontoTime = 0.0f;
    void FixedUpdate()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;

        if(Time.time - pontoTime > 1f){
            gm.pontos++;
            pontoTime = Time.time;
        }

        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        if(transform.position.y >= 13f || transform.position.y <= 3.5f){
            if(transform.position.y > 13f){
                Thrust(xInput, -1);
            }
            else{
                Thrust(xInput, 1);
            }
        }
        else{
            Thrust(xInput, yInput);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }

        if(Input.GetAxisRaw("Fire1") != 0)
        {
            Shoot();
        }

        if (yInput != 0 || xInput != 0)
        {
            animator.SetFloat("Velocity", 1.0f);
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
