using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WorldWhisperManager : MonoBehaviour
{
    public static WorldWhisperManager Instance {get; private set;}

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
            return;
        }
    }

    public TextMeshProUGUI txtText1Lan, txtTextBayLen, txtTextThongBao;

    public void ShowWhisper(String txt){
        txtTextThongBao.gameObject.SetActive(true);
        txtTextThongBao.text = txt;
    }

    public void OffShowWhisper(){
        txtTextThongBao.gameObject.SetActive(false);
    }

    public void ResetWhisper(){
        txtTextThongBao.text = "";
        txtText1Lan.text = "";
        txtTextBayLen.text = "";
        txtTextThongBao.gameObject.SetActive(false);
        txtTextBayLen.gameObject.SetActive(false);
        txtText1Lan.gameObject.SetActive(false);
    }

    // public void ShowMoveUpWhisper(String txt, float duration){
    //     txtText1Lan.gameObject.SetActive(true);
    //     txtText1Lan.text = txt;

    // }

    public void TextBayLen(string text){
        txtTextBayLen.gameObject.SetActive(true);
        txtTextBayLen.text = text;
        StartCoroutine(FadeOutText(txtTextBayLen, 0.5f));
    }



    private IEnumerator FadeOutText(TextMeshProUGUI txt, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = txt.color;

        while (elapsedTime < duration)
        {
            txt.transform.position += Vector3.up * Time.deltaTime * 10f; // Text di chuyển lên nèee
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            txt.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }
        txt.color = originalColor;
        txt.gameObject.SetActive(false);
    }
}
