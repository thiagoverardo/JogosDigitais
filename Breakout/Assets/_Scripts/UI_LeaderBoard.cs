using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Referencias:
// https://www.grimoirehex.com/unity-3d-local-leaderboard/
// https://www.youtube.com/watch?v=6GI9zzWsVm8&feature=emb_logo
public class PlayerInfo
{
    public string name;
    public int score;
    public PlayerInfo(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
public class UI_LeaderBoard : MonoBehaviour
{
    public InputField playerName;
    public Text displaynames;
    public Text displaypts;
    public Text displaypos;
    GameManager gm;

    //Lista que vai guardar as stats do jogador
    List<PlayerInfo> collectedStats;

    // Start is called before the first frame update
    void Start()
    {
        collectedStats = new List<PlayerInfo>();
        gm = GameManager.GetInstance();
        displaynames.text = "";
        displaypts.text = "";
        displaypos.text = "";
        LoadLeaderBoard();

    }
    public void SubmitButton()
    {
        if(playerName.text.Length > 0 && playerName.text.Length <= 10){
            PlayerInfo stats = new PlayerInfo(playerName.text, gm.pontos);
            collectedStats.Add(stats);
            playerName.text = "";
            SortStats();
        }
    }

    void SortStats()
    {
        for (int i = collectedStats.Count - 1; i > 0; i--)
        {
            if (collectedStats[i].score > collectedStats[i - 1].score)
            {
                PlayerInfo tempInfo = collectedStats[i - 1];

                collectedStats[i - 1] = collectedStats[i];
                collectedStats[i] = tempInfo;
            }
        }
        UpdatePlayerPrefsString();
    }

    void UpdatePlayerPrefsString()
    {
        string stats = "";
        for (int i = 0; i < collectedStats.Count; i++)
        {
            stats += collectedStats[i].name + ",";
            stats += collectedStats[i].score + ",";
        }

        //salva a string
        PlayerPrefs.SetString("LeaderBoards", stats);

        UpdateLeaderBoardVisual();
    }

    void UpdateLeaderBoardVisual()
    {
        displaynames.text = "";
        displaypts.text = "";
        displaypos.text = "";

        for (int i = 0; i <= collectedStats.Count - 1; i++)
        {
            displaypos.text += (i + 1)+ "\n";
            displaynames.text += collectedStats[i].name+ "\n";
            displaypts.text += collectedStats[i].score + "\n";
        }
    }

    void LoadLeaderBoard()
    {
        string stats = PlayerPrefs.GetString("LeaderBoards", "");
        string[] stats2 = stats.Split(',');

        for (int i = 0; i < stats2.Length - 2; i += 2)
        {
            PlayerInfo loadedInfo = new PlayerInfo(stats2[i], int.Parse(stats2[i + 1]));
            collectedStats.Add(loadedInfo);
            UpdateLeaderBoardVisual();
        }
    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteKey("LeaderBoards");
        displaynames.text = "";
        displaypts.text = "";
        displaypos.text = "";
        Start();
    }
}
