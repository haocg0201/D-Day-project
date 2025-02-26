using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DarkBrokenDialogQuestManager : MonoBehaviour
{
    
    public GameObject dialogueUI, questUI;
    public Image npcImage, playerImage;
    public TextMeshProUGUI dialogueText;
    public Button nextButton, btnClose, btnBackLater, btnOK, btnAward;
    public Transform pA, pB, pC;
    public GameObject cookie;
    public GameObject monsterGate;
    GameObject cookieNow;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(DisplayNextSentence);
        btnBackLater.onClick.AddListener(OnBackLater);
        btnOK.onClick.AddListener(OK);
        btnAward.onClick.AddListener(Award);
        btnClose.onClick.AddListener(Close);
        ResetButton(); monsterGate.SetActive(false);
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
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
        GameManager.Instance.isGetQuest = true;
        questUI.SetActive(true);
        if(cookieNow == null){
            SpawnRandomCookie();
            monsterGate.SetActive(true);
            monsterGate.transform.position = SpawnRandomMSP();
        }

        SceneLoader.Instance.StartCounting();
        EndDialogue();  
    }

    void Award(){
        // nhận thưởng
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(GameManager.Instance.isHalfQuest && GameManager.Instance.isQuestDone){
            SceneLoader.Instance.SetElapsedTime();
            GameManager.Instance.TraningTime("mapB");
            SceneLoader.Instance.StopCounting();
            GameManager.Instance.SkillAward("mapB");
            GameManager.Instance._mgCounter = 200;
            GameManager.Instance._rgCounter = 200;
            GameManager.Instance.UpdateMoonG();
            GameManager.Instance.UpdateRune();
            GameManager.Instance.ResetTheCounter();
            
            ResetCookie();
            questUI.SetActive(false);
            nextButton.gameObject.SetActive(false);
            EndDialogue();
        } 
    }

    void Close(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        EndDialogue();
    }

    void ResetCookie(){
        Destroy(cookieNow);
        cookieNow = null;
        monsterGate.SetActive(false);
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

    public void SpawnRandomCookie()
    {
        Transform[] positions = { pA, pB, pC };
        Transform randomPosition = positions[Random.Range(0, positions.Length)];
        cookieNow = Instantiate(cookie, randomPosition.position, Quaternion.identity);
    }

    public Vector3 SpawnRandomMSP()
    {
        Vector3 p1 = new(62f, 3.7f,0);
        Vector3 p2 = new(46.5f, -11f,0);
        Vector3 p3 = new(75f,-19f,0);
        Vector3[] positions = { p1, p2, p3 };
        Vector3 randomPosition = positions[Random.Range(0, positions.Length)];
        return randomPosition;
    }
}
