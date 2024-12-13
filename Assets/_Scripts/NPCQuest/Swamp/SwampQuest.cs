using TMPro;
using UnityEngine;

public class SwampQuest : MonoBehaviour
{
    public TextMeshProUGUI questText1, questText2, txtResult;
    void Start()
    {
        SetResult();
        questText1.color = Color.black;
        questText2.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance != null){
            int i = GameManager.Instance.killCountBoss;
            
            if(i == 1){
                GameManager.Instance.isQuestDone = true;
                txtResult.text = "Hoàn thành nhiệm vụ";
                Color color = Color.blue;
                txtResult.color = color;
                GameManager.Instance.isQuestDone = true;
            }
        }
    }

    void SetResult(){
        questText1.text = "Sống sót";
        questText2.text = "Đánh bại The dead present";
        txtResult.text = "";
    }
}
