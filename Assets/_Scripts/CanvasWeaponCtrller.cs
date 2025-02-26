using UnityEngine;

public class CanvasWeaponCtrller : MonoBehaviour
{
    public void HideCanvas(){
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void ShowCanvas(){
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}
