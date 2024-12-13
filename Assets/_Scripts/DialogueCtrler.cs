using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;

public class DialogueCtrler : MonoBehaviour
{
    public Canvas mainCanvas;
    public TextMeshProUGUI dialogueText;
    public Image panelImage;
    public float fadeDuration = 2f; // Thời gian để mờ dần khoảng này là ổn
    public string[] dialogueLines = new string[]
    {
        "Hmm... Tôi không biết sao mình lại ở đây nữa?",
        "Như là một giấc mơ... nhưng mà sao nó lại thật đến vậy?",
        "Có điều gì đó... đang chờ tôi ở đây sao.",
        "Lần cuối cùng tôi còn nhớ, tôi đã làm việc kiệt sức ở văn phòng tới mức phải nhập viện.",
        "Không... Tôi phải thức dậy ngay!",
        "Những ký ức... làm sao để nó trở lại...",
        "Đây là... ngày bắt đầu hành trình mới của tôi.",
        "Hôm nay, tôi không còn đi làm nữa.",
        "..."
    };
    private int dialogueIndex = 0;
    public float typingSpeed;

    void Start()
    {
        typingSpeed = 0.05f;
        if (PlayerPrefs.GetInt("level") <= 1)
        {
            mainCanvas.gameObject.SetActive(true); 
            StartCoroutine(StartDialogue());
            if(GameManager.Instance != null){
                GameManager.Instance.playerData.stat.level = 2;
                PlayerPrefs.SetInt("level", 2);
            }
        }
        else
        {
            mainCanvas.gameObject.SetActive(false);
        }
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);  // 
        while (dialogueIndex < dialogueLines.Length)
        {
            yield return TypeDialogue(dialogueLines[dialogueIndex]);
            dialogueIndex++;
            yield return new WaitForSeconds(0.5f);  // Dừng tí
        }

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOutPanel());
    }

    IEnumerator TypeDialogue(string line)
    {
        dialogueText.text = "";
        foreach (Char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator FadeOutPanel()
    {
        Color panelColor = panelImage.color;
        float startAlpha = panelColor.a;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, Mathf.Lerp(startAlpha, 0, normalizedTime));
            yield return null;
        }

        panelImage.color = new Color(panelColor.r, panelColor.g, panelColor.b, 0);  // cho nó trong suất
        mainCanvas.gameObject.SetActive(false); 
    } // ae đừng hỏi tôi huhu
}
