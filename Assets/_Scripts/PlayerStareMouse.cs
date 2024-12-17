using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    
    // private SpriteRenderer spriteRenderer;

    // void Start()
    // {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    // }
    void Update()
    {
        if(Player.Instance.isConsume){
            PStareM();
        }
        
    }

    void PStareM(){
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > transform.position.x && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (mousePosition.x < transform.position.x && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
