using TMPro;
using UnityEngine;

public class WinterQuest : MonoBehaviour
{
    public TextMeshProUGUI questText1, questText2, txtResult;
    void Start()
    {
        questText1.color = Color.black;
        questText2.color = Color.black;
    }

    void OnDisable()
    {
        questText1.color = Color.black;
        questText2.color = Color.black;
        txtResult.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null){
            int ii = GameManager.Instance.killCount;
            int playerExhausted = 0;
            if(GameManager.Instance.Health == 0){
                playerExhausted = GameManager.Instance.killCountBoss = 1;

            }
            
            if(playerExhausted > 0){
                WorldWhisperManager.Instance.TextBayLen("Nhiệm vụ thất bại");
                return;
            }
            if(ii > 111){
                ii = 111;
            }

            if(ii == 111){
                SetResult2(111,true);
                SetResult1(true);
                GameManager.Instance.isHalfQuest = true;
            }else{
                SetResult2(ii,false);
                SetResult1(false);
            }
            if(ii == 111){
                Color color = Color.red;
                txtResult.color = color;
                txtResult.text = "Hoàn thành nhiệm vụ";
                GameManager.Instance.isQuestDone = true;
            }else{
                txtResult.text = "";
            }
        }
    }
   void SetResult1(bool isColor){
        Color defaultColor;
        if (isColor)
        {
            defaultColor = Color.red;
        }
        else
        {
            defaultColor = questText1.color;
        }
        questText1.color = defaultColor;
        questText1.text = $"Sống sót sau quái triều"; 
    }

    void SetResult2(int quantity,bool isColor){
        Color defaultColor;
        if (isColor)
        {
            defaultColor = Color.red;
        }
        else
        {
            defaultColor = questText2.color;
        }
        questText2.color = defaultColor;
        questText2.text = $"Tiêu diệt {quantity}/111 quái vật";
    }
}
