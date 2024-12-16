using System.Collections;
using UnityEngine;

public class SwampEffect : MonoBehaviour
{
    private float reducePercentage = 0.05f;
    private bool isEffectActive = false;
    int maxMana = 0;

    private void Start()
    {
        maxMana = GameManager.Instance.MaxMana; ;
    }

    private void Update()
    {
        if(!isEffectActive){
            StartCoroutine(ReduceMana());
        }
        
    }

    IEnumerator ReduceMana()
    {
        isEffectActive = true;
        GameManager.Instance.Mana -= (int)(maxMana * reducePercentage);
        yield return new WaitForSeconds(10f);
        isEffectActive = false;
    }
}
