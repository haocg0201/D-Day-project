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
        txtTextThongBao.enabled = true;
        txtTextThongBao.text = txt;
    }

    public void OffShowWhisper(){
        txtTextThongBao.enabled = false;
    }

    public void ResetWhisper(){
        txtTextThongBao.text = "";
        txtText1Lan.text = "";
        txtTextBayLen.text = "";
    }

    public void ShowMoveUpWhisper(String txt, float duration){
        txtText1Lan.text = txt;

    }

    private IEnumerator FadeOutText(TextMeshProUGUI txt, float duration)
    {
        float elapsedTime = 0f;
        Color originalColor = txt.color;

        while (elapsedTime < duration)
        {
            txt.transform.position += Vector3.up * Time.deltaTime; // Text di chuyển lên nèee
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            txt.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        txt.enabled = false;
    }
}
