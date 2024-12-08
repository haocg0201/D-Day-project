using UnityEngine;

public class LifeTimeTerminator : MonoBehaviour
{
    public float lifeTime = 1.5f;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
