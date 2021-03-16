using UnityEngine;
using UnityEngine.UI;
public class UI_FimDeJogo : MonoBehaviour
{
    public Text message;
    GameManager gm;
    private void OnEnable()
    {
        gm = GameManager.GetInstance();

        if(gm.vidas <= 0)
        {
            message.text = "GAME OVER";
        }
        if(gm.vidas >= 1000){
            message.text = "EASTER EGG";
        }
    }
    public void Inicio()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}
