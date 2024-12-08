using System.Collections;
using UnityEngine;

public class WorldWhisperManager : MonoBehaviour
{
    public static WorldWhisperManager _instance {get; private set;}
    public static WorldWhisperManager Instance{
        get{
            if(_instance == null){
                Debug.LogError("WorldWhisperManager is null");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if(_instance == null){
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    // private IEnumerator FadeOutText(float duration)
    // {
    //     float elapsedTime = 0f;
    //     Color originalColor = damageText.color;

    //     while (elapsedTime < duration)
    //     {
    //         damageTextTransform.position += Vector3.up * Time.deltaTime; // Text di chuyển lên nèee
    //         elapsedTime += Time.deltaTime;
    //         float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
    //         damageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    //         yield return null;
    //     }

    //     damageText.enabled = false;
    // }
}
