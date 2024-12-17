
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpCtrller : MonoBehaviour
{
    public UnityEngine.UI.Image fillBar, manaBar;
    public TextMeshProUGUI healthText,  lvText,manaText; 
    void Update()
    {
        UpdateHealthBar(
            GameManager.Instance.Health, 
            GameManager.Instance.MaxHealth,
            GameManager.Instance.Mana,
            GameManager.Instance.MaxMana
        );
    }
    public void UpdateHealthBar(int currentHealthValue, int maxHealthValue, int currentManaValue, int maxManaValue){
        fillBar.fillAmount = (float)currentHealthValue / maxHealthValue;
        healthText.text = currentHealthValue.ToString() + "/" + maxHealthValue.ToString();
        
        manaBar.fillAmount = (float)currentManaValue / maxManaValue;
        manaText.text = currentManaValue.ToString() + "/" + maxManaValue.ToString();

        lvText.text = "Lv:   " + GameManager.Instance.playerData.stat.level.ToString();
    }
}
