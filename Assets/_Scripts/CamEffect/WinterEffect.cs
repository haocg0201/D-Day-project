using UnityEngine;

public class WinterEffect : MonoBehaviour
{
    private void Start() {
        GameManager.Instance.Survivability -= 1f;
    }
}
