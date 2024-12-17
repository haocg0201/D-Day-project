using UnityEngine;

public class Bullet : MonoBehaviour 
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(20f, rb.linearVelocity.y);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
