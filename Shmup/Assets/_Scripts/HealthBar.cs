using UnityEngine;
using UnityEngine.UI;

// Referencia: https://www.youtube.com/watch?v=v1UGTTeQzbo
public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;
    GameManager gm;

    public void SetHealth(float health, float maxHealth)
    {
        gm = GameManager.GetInstance();
        if(health > 0){
            Slider.gameObject.SetActive(health < maxHealth);
            Slider.value = health;
            Slider.maxValue = maxHealth;

            Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue );
        }
        else if(health <= 0){
            Slider.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.gameState == GameManager.GameState.GAME || gm.gameState == GameManager.GameState.PAUSE){
            Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        }
        else{
            Slider.gameObject.SetActive(false);
        }
    }
}
