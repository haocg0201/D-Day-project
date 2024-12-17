using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinterDialogManager : MonoBehaviour
{
    public GameObject dialogueUI, questUI;
    public Image npcImage, playerImage;
    public TextMeshProUGUI dialogueText;
    public Button nextButton, btnClose, btnBackLater, btnOK, btnAward;
    public bool deathFlag = false;

    private Queue<string> sentences;
    public GameObject winterGate;

    void Start()
    {
        
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(DisplayNextSentence);
        btnBackLater.onClick.AddListener(OnBackLater);
        btnOK.onClick.AddListener(OK);
        btnAward.onClick.AddListener(Award);
        btnClose.onClick.AddListener(Close);
        ResetButton(); 
        winterGate.SetActive(false);
    }

    void Update()
    {
        if(deathFlag) return;
        if(GameManager.Instance.Health == 0 || GameManager.Instance.killCountBoss == 1){
            GameManager.Instance.ResetTheCounter();
            questUI.SetActive(false);
            winterGate.SetActive(false);
            deathFlag = true;
        } 
        
    }

    void OnEnable()
    {
        if (GameManager.Instance.isQuestDone)
        {
            nextButton.gameObject.SetActive(true);
        }
        btnClose.gameObject.SetActive(true);
    }

    public void StartDialogue(Sprite npcSprite, List<string> dialogue)
    {
        if(Player.Instance != null){
            playerImage.sprite = Player.Instance.GetComponent<SpriteRenderer>().sprite;
            Player.Instance.isConsume = false;
        }
        SetTransparency(0.3f,playerImage);
        dialogueUI.SetActive(true);
        npcImage.sprite = npcSprite;
        sentences.Clear();

        foreach (string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void SetTransparency(float alpha,Image img) { 
        Color color = img.color; 
        color.a = Mathf.Clamp01(alpha); 
        img.color = color; 
    }

    public void ResetButton(){
        btnBackLater.gameObject.SetActive(false);
        btnOK.gameObject.SetActive(false);
        btnAward.gameObject.SetActive(false);
    }

    void OnBackLater()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        EndDialogue();
        return;
    }

    void OK()
    {   
        deathFlag = false;
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        GameManager.Instance.isGetQuest = true;
        questUI.SetActive(true);
        winterGate.SetActive(true);
        EndDialogue();  
        WorldWhisperManager.Instance.TextBayLen("Cảnh báo, quái triều đang đổ bộ !!!");
    }

    void Award(){
        // nhận thưởng
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(GameManager.Instance.isHalfQuest && GameManager.Instance.isQuestDone){
            SceneLoader.Instance.SetElapsedTime();
            GameManager.Instance.TraningTime("mapC");
            SceneLoader.Instance.StopCounting();
            GameManager.Instance.SkillAward("mapC");
            GameManager.Instance._mgCounter = 200;
            GameManager.Instance._rgCounter = 200;
            GameManager.Instance.UpdateMoonG();
            GameManager.Instance.UpdateRune();
            GameManager.Instance.ResetTheCounter();
            
            ResetInfo();
            questUI.SetActive(false);
            nextButton.gameObject.SetActive(false);
            EndDialogue();
        }
    }

    void Close(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        EndDialogue();
    }

    void ResetInfo(){
        winterGate.SetActive(false);
    }


    public void DisplayNextSentence()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if (sentences.Count == 0)
        {
            nextButton.gameObject.SetActive(false);
            SetTransparency(1f,playerImage);
            SetTransparency(0.3f,npcImage);
            if(GameManager.Instance.isQuestDone){
                btnAward.gameObject.SetActive(true);
            }else{
                btnBackLater.gameObject.SetActive(true);
                btnOK.gameObject.SetActive(true);
            }
            return;
            
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        nextButton.gameObject.SetActive(true);
    }

    public void EndDialogue()
    {
        ResetButton();
        Player.Instance.isConsume = true;
        dialogueUI.SetActive(false);
    }
}
