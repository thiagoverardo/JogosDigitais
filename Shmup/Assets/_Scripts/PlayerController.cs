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
    public float shootDelay = 0.4f;
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
        if(gm.gameState == GameManager.GameState.NEWGAME){
            gm.ChangeState(GameManager.GameState.GAME);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }

        else{
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
            if(transform.position.x <= 0){
                gm.pontos = -1000;
                gm.vidas = 1100;
                gm.ChangeState(GameManager.GameState.ENDGAME);
            }

        }

    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inimigos"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>(); 
            enemy.Die();
            TakeDamage();
        }
        if (collision.CompareTag("Inimigos2"))
        {
            StatePatrulhaPorWaypoints enemy = collision.gameObject.GetComponent<StatePatrulhaPorWaypoints>(); 
            enemy.Die();
            TakeDamage();
        }
        if (collision.CompareTag("Powerup"))
        {
            Destroy(collision.gameObject);
            shootDelay = 0.2f;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            StartCoroutine(ResetPowerup());
        }
        if (collision.CompareTag("Powerup2"))
        {
            Destroy(collision.gameObject);
            if(gm.vidas < maxlifes){
                 gm.vidas += 1;
            }
            HealthBar.SetHealth(gm.vidas, maxlifes);
        }
    }
    private IEnumerator ResetPowerup(){
        yield return new WaitForSeconds(4);
        shootDelay = 0.4f;
        GetComponent<SpriteRenderer>().color = Color.white;
    }     
}
