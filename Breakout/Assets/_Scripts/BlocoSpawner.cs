using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlocoSpawner : MonoBehaviour
{
    public GameObject Bloco;
    GameManager gm;

    void Start()
    {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += Construir;
        Construir();
    }

    void Construir()
    {
        if (gm.gameState == GameManager.GameState.GAME)
        {
            for(int i = 0; i < 12; i++)
            {
                int j = 0;
                for(j = 0; j < 4; j++){
                    Vector3 posicao = new Vector3(-9 + 1.55f * i, 4 - 0.55f * j);

                    Instantiate(Bloco, posicao, Quaternion.identity, transform);
                }
            }
        }
    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            gm.ChangeState(GameManager.GameState.ENDGAME);
        }
    }

}

