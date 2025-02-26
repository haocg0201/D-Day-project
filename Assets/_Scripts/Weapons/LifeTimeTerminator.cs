using UnityEngine;

public class LifeTimeTerminator : MonoBehaviour
{
    public float lifeTime = 0.5f;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
