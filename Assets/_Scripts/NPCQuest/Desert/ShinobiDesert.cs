using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShinobiDesert : MonoBehaviour
{
    public GameObject interactionUI;
    public DesertQuestDialogManager dialogueManager;
    Sprite npcSprite; 
    bool isTalkRange;
    public List<string> dialogueLines; 

    void Start()
    {
        isTalkRange = false;
        npcSprite = GetComponent<SpriteRenderer>().sprite;
        SetDialogs();
        Debug.Log("ShinobiDesert Start");
    }

    void SetDialogs(){
        dialogueLines.Clear();
        if(!GameManager.Instance.isGetQuest){
            dialogueLines.Add("Chào người du hành, tôi là vị thần của chiến tranh, tên tôi là Athena");
            dialogueLines.Add("Nhìn vùng đất cằn cỗi này xem, nó mang đầy kỉ niệm đau thương và xưa cũ");
            dialogueLines.Add("Nhật dương quang chiếu sáng tới tận thiên thu chẳng bao giờ dừng lại, nó ban phát cho vùng đất này sự chết chóc");
            dialogueLines.Add("Cũng đã từ rất lâu rồi chưa có ai tới đây, tất cả chỉ còn là một nơi tàn tích đổ vỡ");
            dialogueLines.Add("Cũng chỉ tạo lũ quái vật từ chiều không gian khác tới, chúng tàn phá và chiếm cứ nơi đây");
            dialogueLines.Add("Hơi người du hành dũng cảm, cậu có sẵn sàng giúp ta một nhiệm vụ này không? Tất nhiên ta sẽ trả cho cậu 1 khoản thù lao xứng đáng");
        }else if(!GameManager.Instance.isQuestDone && GameManager.Instance.isGetQuest){
            dialogueLines.Add("Hãy cẩn thận nhé");
            Debug.Log("ShinobiDeser, is get quest: " + GameManager.Instance.isGetQuest);
        }else if(GameManager.Instance.isQuestDone){
            dialogueLines.Add("Thật phi thường, bạn đã hoàn thành được nhiệm vụ");
            dialogueLines.Add("Hi vọng tương lai gần, nơi đây sẽ khôi phục lại được vẻ đẹp vốn có của nó");
            dialogueLines.Add("Phải rồi, đây là thù lao dành cho bạn");
            dialogueLines.Add("Cảm ơn bạn vì đã giúp đỡ tôi tiêu diệt quái vật trong vùng này");
            dialogueLines.Add("<3");
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTalkRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            isTalkRange = false;
            if(WorldWhisperManager.Instance != null){
                WorldWhisperManager.Instance.OffShowWhisper();
            }
        }
    }

    private void Update()
    {
        
        if(isTalkRange){
            if(WorldWhisperManager.Instance != null){
                WorldWhisperManager.Instance.ShowWhisper("Ấn F để tương tác");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                SetDialogs();
                interactionUI.SetActive(true);
                WorldWhisperManager.Instance.OffShowWhisper();
                dialogueManager.StartDialogue(npcSprite, dialogueLines);
            }
        }
        
    }
}
