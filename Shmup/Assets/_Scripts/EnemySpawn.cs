using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REFERENCIA https://www.youtube.com/watch?v=E7gmylDS1C4
public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemyPrefab;
    public float respawnTime = 3.0f;
    private Vector3 screenBounds;
    GameManager gm;
    void Start()
    {
        if(GameObject.FindWithTag("Player")){
            screenBounds = GameObject.FindWithTag("Player").transform.position;
        }
        StartCoroutine(enemyWave());
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState != GameManager.GameState.GAME) return;
        
        if(GameObject.FindWithTag("Player")){
            screenBounds = GameObject.FindWithTag("Player").transform.position;
        }
    }
    private void spawnEnemy()
    {
        if(GameObject.FindWithTag("Player") && gm.gameState == GameManager.GameState.GAME){
            GameObject a = Instantiate(enemyPrefab) as GameObject;
            a.transform.position = new Vector2(screenBounds.x + 12, Random.Range(13, 3.5f));
        }
    }
    IEnumerator enemyWave(){
        if(GameObject.FindWithTag("Player")){
            while(true){
                yield return new WaitForSeconds(respawnTime);
                spawnEnemy();
            }
        }
    }
}
