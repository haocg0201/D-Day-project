using System.Collections.Generic;
using UnityEngine;

public class SwampNpc : MonoBehaviour
{
    public GameObject interactionUI;
    public SwamDialogManager dialogueManager;
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
        //     AudioManager.Instance.PlaySFX(AudioManager.Instance.campD);
        // }
    }

    void SetDialogs(){
        dialogueLines.Clear();
        if(!GameManager.Instance.isGetQuest){
            dialogueLines.Add("Ồ một vị khách không mời");
            dialogueLines.Add("Trông nhà người có vẻ rất thích phiêu lưu nhỉ");
            dialogueLines.Add("Vùng đất này được cai trị bởi một tên hiền triết được gọi là King of esclipse: The dead present");
            dialogueLines.Add("Khi còn sống hắn là 1 nhà hiền triết đại tài");
            dialogueLines.Add("Gương mặt phân hủy với da căng mỏng, nứt nẻ, và xương hàm lộ ra đôi mắt trắng dã với ánh sáng đỏ xanh yếu ớt.");
            dialogueLines.Add("Tóc bạc xám lưa thưa, rối bù, cứng lại do bụi bẩn và máu khô, hắn mặc bộ áo giáp gỉ sét, nứt vỡ, bên dưới là áo chùng rách rưới.");
            dialogueLines.Add("Cơ thể cao lớn, gầy guộc, nhưng mạnh mẽ với đôi tay dài và móng vuốt sắc, là undead, cơ thể không bị giới hạn bởi nỗi đau hay mệt mỏi.");
            dialogueLines.Add("Mặt và cổ có dấu hiệu phân hủy sâu, vết nứt lớn.");
            dialogueLines.Add("Trên ngực áo giáp có khắc biểu tượng mờ của vương quốc đã suy tàn.Chiếc mắt xích sắt quanh cổ tay, trói buộc linh hồn bởi lời nguyền cổ xưa.");
            dialogueLines.Add("Sao ngươi có hứng thú với hắn chứ? Nếu ngươi đánh bại được hắn, ta sẽ cược quyền năng của ta cho ngươi");
        }else if(!GameManager.Instance.isQuestDone && GameManager.Instance.isGetQuest){
            dialogueLines.Add("Khặc hặc khặc hặc, thật thú vị");
            dialogueLines.Add("Nếu ngươi không đánh bại hắn, vậy thì ngươi sẽ phải trả giá đấy");
        }else if(GameManager.Instance.isQuestDone){
            dialogueLines.Add("Ồ tốt lắm, ngươi khá hơn ta tưởng đấy");
            dialogueLines.Add("Khặc khặc, ta sẽ không nuốt lời đâu, đây là thứ ta sẽ cho ngươi");
            dialogueLines.Add("Đừng để nó bị lãng quên, như ta vậy");
            dialogueLines.Add(":(");
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
