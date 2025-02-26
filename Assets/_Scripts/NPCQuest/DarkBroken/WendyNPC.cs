using System.Collections.Generic;
using UnityEngine;

public class WendyNPC : MonoBehaviour
{
    public GameObject interactionUI;
    public DarkBrokenDialogQuestManager dialogueManager;
    Sprite npcSprite; 
    bool isTalkRange;
    public List<string> dialogueLines; 

    void Start()
    {
        isTalkRange = false;
        npcSprite = GetComponent<SpriteRenderer>().sprite;
        SetDialogs();
        Debug.Log("ShinobiDesert Start");
        // if(AudioManager.Instance != null){
        //     AudioManager.Instance.PlaySFX(AudioManager.Instance.campA);
        // }
    }

    void SetDialogs(){
        dialogueLines.Clear();
        if(!GameManager.Instance.isGetQuest){
            dialogueLines.Add("Chào Onii~ đằng đó, ta là vị thần của gió và màn đêm, tên ta là Windy");
            dialogueLines.Add("Bóng tối đã bao trùm mảnh đất này từ rất lâu, thật nhiều kỉ niệm cũ cứ khiến ta nhớ về");
            dialogueLines.Add("Ánh nhật dương không thể chiếu tới đây, người dân đề rời đi hết rồi...");
            dialogueLines.Add("Cũng đã từ rất lâu rồi chưa có ai tới đây, tất cả chỉ còn là một nơi tối tăm đổ vỡ");
            dialogueLines.Add("Cũng do lũ quái vật từ chiều không gian khác tới, chúng tàn phá và chiếm cứ nơi đây");
            dialogueLines.Add("Hỡi Onii~ dũng cảm, nhà ngươi có sẵn sàng giúp ta một nhiệm vụ này không? Tất nhiên ta sẽ trả cho nhà ngươi 1 khoản thù kếch xù");
        }else if(!GameManager.Instance.isQuestDone && GameManager.Instance.isGetQuest){
            dialogueLines.Add("Hãy cẩn thận nhé Onii~");
            Debug.Log("ShinobiDeser, is get quest: " + GameManager.Instance.isGetQuest);
        }else if(GameManager.Instance.isHalfQuest && GameManager.Instance.isQuestDone){
            dialogueLines.Add("Au mai gaa, Onii~ giỏi quá đi mất");
            dialogueLines.Add("Hi vọng tương lai gần, nơi đây sẽ khôi phục lại được vẻ đẹp vốn có của nó");
            dialogueLines.Add("Phải rồi, đây là thù lao dành cho Onii nhà ngươi");
            dialogueLines.Add("Cảm ơn nhà ngươi vì đã giúp đỡ ta tiêu diệt quái vật nơi này nhé");
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
