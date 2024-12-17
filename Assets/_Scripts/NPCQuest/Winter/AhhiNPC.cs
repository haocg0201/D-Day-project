using System.Collections.Generic;
using UnityEngine;

public class AhhiNPC : MonoBehaviour
{
    public GameObject interactionUI;
    public WinterDialogManager dialogueManager;
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
        //     AudioManager.Instance.PlaySFX(AudioManager.Instance.campB);
        // }
    }

    void SetDialogs(){
        dialogueLines.Clear();
        if(!GameManager.Instance.isGetQuest){
            dialogueLines.Add("Chào Vì tinh tú tới từ phương xa, tôi là vị thần của mùa màng, tên tôi là Ahhi");
            dialogueLines.Add("Thật xin lỗi khi khách từ xa tới mà lại không thể tiếp đón chân trọng nhất");
            dialogueLines.Add("Nơi đây quanh năm chỉ toàn là gió và tuyết, người dân đã rời bỏ nó mà đi");
            dialogueLines.Add("Kể từ khi có một cánh cổng xuất hiện, nhưng con quái vật hung tợn từ đó đi ra");
            dialogueLines.Add("Vì tinh tú của quả cảm, bạn sẽ sẵn lòng giúp đỡ tôi đẩy lùi cuộc xâm lăng của bầy quái vật hung ác chứ");
        }else if(!GameManager.Instance.isQuestDone && GameManager.Instance.isGetQuest){
            dialogueLines.Add("Tuyết rơi ngày một dày thêm");
            dialogueLines.Add("Nhớ mặc áo ấm vào nhé, người bạn của tôi");
            Debug.Log("ShinobiDeser, is get quest: " + GameManager.Instance.isGetQuest);
        }else if(GameManager.Instance.isQuestDone){
            dialogueLines.Add("Thật sao, bạn đã đánh bại được bè lũ quái vật rồi sao?");
            dialogueLines.Add("Tôi và người dân nơi đây sẽ nhớ mãi hành động nghĩa hiệp của bạn");
            dialogueLines.Add("Để tỏ lòng thành, tôi có món quà muốn tặng cho bạn, hi vọng bạn sẽ thích nó");
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
