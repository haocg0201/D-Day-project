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
    public GameObject missedItem;

    public GameObject desertMGate;

    void Start()
    {
        sentences = new Queue<string>();
        nextButton.onClick.AddListener(DisplayNextSentence);
        btnBackLater.onClick.AddListener(OnBackLater);
        btnOK.onClick.AddListener(OK);
        btnAward.onClick.AddListener(Award);
        btnClose.onClick.AddListener(Close);
        ResetButton(); 
        desertMGate.SetActive(false);
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
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        GameManager.Instance.isGetQuest = true;
        SceneLoader.Instance.StartCounting();
        questUI.SetActive(true);
        spawnPaperScroll();
        desertMGate.SetActive(true);
        desertMGate.transform.position = SpawnRandomMSP();
        EndDialogue();  
    }

    void spawnPaperScroll(){
        List<Vector3> spawnPositions = SpawnRandomPaperScrolls();
        foreach (Vector3 pos in spawnPositions)
        {
            Instantiate(missedItem, pos, Quaternion.identity);
        }
    }

    void Award(){
        // nhận thưởng
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        if(GameManager.Instance.isHalfQuest && GameManager.Instance.isQuestDone){
            SceneLoader.Instance.StopCounting();
            GameManager.Instance.TraningTime("mapA");
            GameManager.Instance.SkillAward("mapA");
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

    void ResetInfo(){
        desertMGate.SetActive(false);
    }

    public List<Vector3> SpawnRandomPaperScrolls()
    {
        Vector3 p1 = new(-3f, -5f, 0);
        Vector3 p2 = new(8.77f, -1.84f, 0);
        Vector3 p3 = new(1.79f, 2.48f, 0);
        Vector3 p4 = new(-6.15f, 2.53f, 0);
        Vector3 p5 = new(9.66f, 1.67f, 0);
        Vector3 p6 = new(21.51f, 7.58f, 0);
        Vector3 p7 = new(27.51f, 8.87f, 0);
        Vector3 p8 = new(51.52f, 0.8f, 0);

        Vector3[] positions = { p1, p2, p3, p4, p5, p6, p7, p8 };

        List<Vector3> selectedPositions = new();

        List<Vector3> availablePositions = new(positions);

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availablePositions.Count);
            selectedPositions.Add(availablePositions[randomIndex]);
            availablePositions.RemoveAt(randomIndex); 
        }

        return selectedPositions;
    }


    public Vector3 SpawnRandomMSP()
    {
        Vector3 p1 = new(50f, -13f,0);
        Vector3 p2 = new(34f, 0,0);
        Vector3 p3 = new(16f,-18.5f,0);
        Vector3[] positions = { p1, p2, p3 };
        Vector3 randomPosition = positions[Random.Range(0, positions.Length)];
        return randomPosition;
    }
}

