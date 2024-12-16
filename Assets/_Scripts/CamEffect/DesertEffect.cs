using System.Collections;
using UnityEngine;

public class DesertEffect : MonoBehaviour
{
    private float reducePercentage = 0.05f;
    private bool isEffectActive = false;

    void Update()
    {
        if (!isEffectActive){
            StartCoroutine(ReduceHP());
        }
        
    }
    IEnumerator ReduceHP()
    {
        isEffectActive = true;
        GameManager.Instance.Health -= (int)(GameManager.Instance.MaxHealth * reducePercentage);
        yield return new WaitForSeconds(10f);
        isEffectActive = false;
    }
}
