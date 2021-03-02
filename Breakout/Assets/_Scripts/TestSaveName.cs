using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField textBox;
    GameManager gm;

    public void clickSaveButton(){
        gm = GameManager.GetInstance();

        PlayerPrefs.SetString("name", textBox.text);
        Debug.Log("Your Name Is: " + PlayerPrefs.GetString("name"));
        Debug.Log("Your Score Is: " + gm.pontos.ToString());
    }
}
