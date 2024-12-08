
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HpCtrller : MonoBehaviour
{
    public UnityEngine.UI.Image fillBar;
    public TextMeshProUGUI healthText;
    void Update()
    {
        UpdateHealthBar(GameManager.Instance.Health, GameManager.Instance.MaxHealth);
    }
    public void UpdateHealthBar(int currentHealthValue, int maxHealthValue){
        fillBar.fillAmount = (float)currentHealthValue / maxHealthValue;
        healthText.text = currentHealthValue.ToString() + "/" + maxHealthValue.ToString();
    }
}
