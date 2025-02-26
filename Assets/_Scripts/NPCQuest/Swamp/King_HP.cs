using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class King_HP : MonoBehaviour
{
    [Header("Main Health Bar")]
    public Image mainHealthFill;
    public float mainMaxHealth = 100f;
    public float mainCurrentHealth;


    [Header("Secondary Health Bar")]
    public Image secondaryHealthFill; 
    public float secondaryMaxHealth = 100f;
    public float secondaryCurrentHealth;

    [Header("HP Display Text")]
    public TextMeshProUGUI currentHPText;
    public TextMeshProUGUI secondaryCurrentHPText;
    KingEclipseNPC king;
    void Start()
    {
        king = GameObject.FindGameObjectWithTag("King").GetComponent<KingEclipseNPC>();
        mainMaxHealth = king.maxHealth;
        secondaryMaxHealth = mainMaxHealth / 2;
        mainCurrentHealth = mainMaxHealth;
        secondaryCurrentHealth = secondaryMaxHealth;
        UpdateHealthBars();
    }

    public void TakeDamage(float damage)
    {
        if (mainCurrentHealth > 0)
        {
            mainCurrentHealth -= damage;
            if(mainCurrentHealth == 75000){
                king.isEnrage = true;
                king.GetComponent<Animator>().SetBool("isEnrage", true);
            }
            if (mainCurrentHealth < 0)
            {
                // Sát thương dư âm chuyển sang máu 2
                float overflowDamage = -mainCurrentHealth;
                mainCurrentHealth = 0;
                secondaryCurrentHealth -= overflowDamage;
            }
            if (mainCurrentHealth < 0)
            {
                // Sát thương dư âm chuyển sang máu 2
                float overflowDamage = -mainCurrentHealth;
                mainCurrentHealth = 0;
                secondaryCurrentHealth -= overflowDamage;
            }
        }
        else if (secondaryCurrentHealth > 0)
        {
            secondaryCurrentHealth -= damage;
        }

        mainCurrentHealth = Mathf.Clamp(mainCurrentHealth, 0, mainMaxHealth);
        secondaryCurrentHealth = Mathf.Clamp(secondaryCurrentHealth, 0, secondaryMaxHealth);

        UpdateHealthBars();
    }

    // Hàm cập nhật giao diện thanh HP
    void UpdateHealthBars()
    {
        mainHealthFill.fillAmount = mainCurrentHealth / mainMaxHealth;
        secondaryHealthFill.fillAmount = secondaryCurrentHealth / secondaryMaxHealth;
        currentHPText.text = $"{mainCurrentHealth}/{mainMaxHealth}";
        secondaryCurrentHPText.text = $"{secondaryCurrentHealth}/{secondaryMaxHealth}";
    }
}
