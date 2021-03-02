using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Pontos : MonoBehaviour
{
    Text textComp;
    GameManager gm;
    public Text highScore;
    void Start()
    {   
        gm = GameManager.GetInstance();
        highScore.text =  String.Format("HighScore: {0}", PlayerPrefs.GetInt("HighScore", 0));
        textComp = GetComponent<Text>();
    }
   
    void Update()
    {
        textComp.text = $"Pontos: {gm.pontos}"; 

        if(gm.pontos > PlayerPrefs.GetInt("HighScore", 0)){
            PlayerPrefs.SetInt("HighScore", gm.pontos);
            highScore.text = String.Format("HighScore: {0}", gm.pontos);
        }   
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        highScore.text = "0";
    }
}
