using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DesertQuestDialogManager : MonoBehaviour
{
    public GameObject dialogueUI, questUI;
    public Image npcImage, playerImage;
    public TextMeshProUGUI dialogueText;
    public Button nextButton, btnClose, btnBackLater, btnOK, btnAward;

    private Queue<string> sentences;

    void Start()
    {
        
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(DisplayNextSentence);
        btnBackLater.onClick.AddListener(OnBackLater);
        btnOK.onClick.AddListener(OK);
        btnAward.onClick.AddListener(Award);
        btnClose.onClick.AddListener(Close);
        ResetButton(); 
    }

    void OnEnable()
    {
        if (GameManager.Instance.isQuestDone && GameManager.Instance.isGetQuest)
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
        EndDialogue();
        return;
    }

    void OK()
    {   
        GameManager.Instance.isGetQuest = true;
        questUI.SetActive(true);
        EndDialogue();  
    }

    void Award(){
        // nhận thưởng
        if(GameManager.Instance.isQuestDone){
            GameManager.Instance._mgCounter = 200;
            GameManager.Instance._rgCounter = 200;
            GameManager.Instance.UpdateMoonG();
            GameManager.Instance.UpdateRune();
            GameManager.Instance.ResetTheCounter();
        }
        nextButton.gameObject.SetActive(false);
        EndDialogue();
    }

    void Close(){
        EndDialogue();
    }

    public void DisplayNextSentence()
    {
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
    }

    public void EndDialogue()
    {
        ResetButton();
        Player.Instance.isConsume = true;
        dialogueUI.SetActive(false);
    }
}

