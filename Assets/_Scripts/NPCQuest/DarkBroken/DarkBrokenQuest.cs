using TMPro;
using UnityEngine;

public class DarkBrokenQuest : MonoBehaviour
{
    public TextMeshProUGUI questText1, questText2, txtResult;

    private void OnEnable() {
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

            if(ii > 1){
                ii = 1;
            }

            if(i < 50){
                SetResult1(i,false);
            }else{
                GameManager.Instance.isHalfQuest = true;
                SetResult1(50,true);
            }

            if(ii == 1){
                SetResult2(true);
            }else{
                SetResult2(false);
            }
            if(ii == 1 && i == 50){
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

    void SetResult2(bool isColor){
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
        questText2.text = "Tìm và mang Cookie về nhà";
    }
}
