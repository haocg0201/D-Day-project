using UnityEngine;

public class WinterEffect : MonoBehaviour
{
    private void Start() {
        GameManager.Instance.Survivability -= 1f;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        GameManager.Instance.Survivability += 1f;
    }
}
