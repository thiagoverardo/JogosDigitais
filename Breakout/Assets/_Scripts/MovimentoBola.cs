using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour

{
    [Range(1, 15)]
    public float velocidade = 20.0f;
    private Vector3 direcao;
    GameManager gm;
    public AudioSource BallSound;
    public AudioClip BallBounce;
    public AudioClip BallDeath;


    
    // Start is called before the first frame update
    void Start()
    {
        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(1.0f, 5.0f);

        direcao = new Vector3(dirX, dirY).normalized;        

        BallSound = GetComponent<AudioSource>();

        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        
        transform.position += direcao * Time.deltaTime * velocidade;

        Vector2 posicaoVP = Camera.main.WorldToViewportPoint(transform.position);

        if( posicaoVP.x <= 0 || posicaoVP.x >= 1 )
        {
            BallSound.clip = BallBounce;
            BallSound.Play();

            direcao = new Vector3(-direcao.x, direcao.y);
        }
        if(posicaoVP.y >= 1 )
        {
            BallSound.clip = BallBounce;
            BallSound.Play();
            direcao = new Vector3(direcao.x, -direcao.y);
        }
        if(posicaoVP.y <= 0)
        {
            BallSound.clip = BallDeath;
            BallSound.Play();
            Reset();
        }
        // Debug.Log($"Vidas: {gm.vidas} \t | \t Pontos: {gm.pontos}");
    }

    private void Reset()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = playerPosition + new Vector3(0, 0.5f, 0);

        float dirX = Random.Range(-5.0f, 5.0f);
        float dirY = Random.Range(2.0f, 5.0f);

        direcao = new Vector3(dirX, dirY).normalized;
        gm.vidas--;

        if(gm.vidas <= 0 && gm.gameState == GameManager.GameState.GAME)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        } 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
        if(col.gameObject.CompareTag("Player"))
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            BallSound.clip = BallBounce;
            BallSound.Play();


            float dirX = (playerPosition.x - transform.position.x)*-10;
            float dirY = Random.Range(1.0f, 5.0f);
            direcao = new Vector3(dirX, dirY).normalized;

        }
        else if(col.gameObject.CompareTag("TijoloGame"))
        {
            direcao = new Vector3(direcao.x, -direcao.y);
            gm.pontos++;
        }
    }

}
