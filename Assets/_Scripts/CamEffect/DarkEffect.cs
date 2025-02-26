using UnityEngine;

public class DarkEffect : MonoBehaviour
{
    [SerializeField] float reducePercentage = 0.2f; //(-20%)
    private int dmgRD = 0;
    void Start()
    {
        dmgRD = Mathf.RoundToInt(GameManager.Instance.Dmg * reducePercentage);
        Debug.Log("Dark Effect - rd dmg: " + dmgRD);
        GameManager.Instance.Dmg -= dmgRD;
    }

}
