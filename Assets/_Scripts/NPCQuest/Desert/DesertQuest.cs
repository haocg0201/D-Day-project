using TMPro;
using UnityEngine;

public class DesertQuest : MonoBehaviour
{
    public TextMeshProUGUI questText1, questText2, txtResult;
    void Start()
    {
        
    }
    void OnDisable()
    {
        questText1.color = Color.white;
        questText2.color = Color.white;
        txtResult.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null){
            int i = GameManager.Instance.killCount;
            int ii = GameManager.Instance.killCountBoss;
            
            if(i > 50){
                i = 50;
            }

            if(ii > 3){
                ii = 3;
            }

            if(i < 50){
                SetResult1(i,false);
            }else{
                GameManager.Instance.isHalfQuest = true;
                SetResult1(50,true);
            }

            if(ii == 3){
                SetResult2(3,true);
            }else{
                SetResult2(ii,false);
            }
            if(ii == 3 && i == 50){
                Color color = Color.yellow;
                txtResult.color = color;
                txtResult.text = "Hoàn thành nhiệm vụ";
                GameManager.Instance.isQuestDone = true;
            }else{
                txtResult.text = "";
            }
        }
    }

   void SetResult1(int i, bool isColor){
        Color defaultColor;
        if (isColor)
        {
            defaultColor = Color.yellow;
        }
        else
        {
            defaultColor = questText1.color;
        }
        questText1.color = defaultColor;
        questText1.text = $"Đánh bại {i}/50 quái vật"; 
    }

    void SetResult2(int quantity,bool isColor){
        Color defaultColor;
        if (isColor)
        {
            defaultColor = Color.yellow;
        }
        else
        {
            defaultColor = questText2.color;
        }
        questText2.color = defaultColor;
        questText2.text = $"Tìm {quantity}/3 băng bó xác ướp";
    }
}
