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

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null){
            int i = GameManager.Instance.killCount;
            int ii = GameManager.Instance.killCountBoss;
            
            if(i > 50){
                i = 50;
                if(ii > 1){
                    ii = 1;
                }
                SetResult(i,ii);
            }
            SetResult(i,ii);
            if(i == 50){
                GameManager.Instance.isHalfQuest = true;
                SetResult(50,ii);
                if(ii == 1){
                    SetResult(50,1);
                    txtResult.text = "Hoàn thành nhiệm vụ";
                    Color color = Color.yellow;
                    txtResult.color = color;
                    GameManager.Instance.isQuestDone = true;
                }
            }
        }
    }

    void SetResult(int i, int ii){
        questText1.text = $"Đánh bại {i}/50 quái vật";
        questText2.text = $"Đánh bại {ii}/1 ???";
        txtResult.text = "";
    }
}
