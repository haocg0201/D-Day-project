using UnityEngine;

public class DarkEffect : MonoBehaviour
{

void Start()
{
    GameManager.Instance.TenPercentRDDmg();
}

private void OnDestroy() 
{
    GameManager.Instance.SetRealDame();
}

}
