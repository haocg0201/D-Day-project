using System.Collections;
using UnityEngine;

public class immortal_spawn : MonoBehaviour
{

    private bool isImmune = false;
    public Player player;

    public GameObject Effect_Immortal;
    public Transform attactPoint;
    private GameObject instantiatedObject; //luu tru doi tuong da instance
    private bool isCooldown = false;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            if (!isImmune)
            {
                StartCoroutine(ActivateImmunity());
            }
            if (isImmune)
            {
                player.isTakeDamage = false;
                Debug.Log("Player is immune, no damage taken!");
                return;
            }
            
        }
    }
    private IEnumerator ActivateImmunity()
    {
        isImmune = true; // Bật chế độ miễn nhiễm

        if (Effect_Immortal != null && attactPoint != null)
        {
            instantiatedObject = Instantiate(Effect_Immortal, attactPoint.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(attactPoint);

            instantiatedObject.transform.position = attactPoint.position;
        }
        Debug.Log("Player is immune to damage for 5 seconds!");

        // Đợi trong 5 giây
        yield return new WaitForSeconds(5f);

        Debug.Log("Player is no longer immune to damage,Cooldown in 10 seconds!");
        isImmune = false; // Tắt chế độ miễn nhiễm sau 5 giây
        player.isTakeDamage = true;
        isCooldown = true;
        Destroy(instantiatedObject);
        yield return new WaitForSeconds(1f);
        Debug.Log("done");
        isCooldown = false;
        
    }
}
