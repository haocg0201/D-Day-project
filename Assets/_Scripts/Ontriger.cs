using UnityEngine;

public class Ontriger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // ------------------VA CHAM VOI QUAI-------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            monster.TakeDamage(1000, Color.red);
            //Destroy(collision.gameObject);
        }
    }
}
