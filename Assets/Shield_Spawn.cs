using System.Collections;
using UnityEngine;

public class Shield_Spawn : MonoBehaviour
{
    public GameObject player;
    public GameObject Effect_Shield;

    private GameObject protectionArea;


    private bool isCooldown = false;
    


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isCooldown)
        {
            StartCoroutine(CreateObjects());
            
        }
    }

    private IEnumerator CreateObjects()
    {
        protectionArea = Instantiate(Effect_Shield, player.transform.position, Quaternion.identity);
        protectionArea.transform.SetParent(player.transform);

        protectionArea.transform.position = player.transform.position;

        Debug.Log("Shield active in 5 seconds!");
        StartCoroutine(DestroyAfterTime(protectionArea, 5f));


        Debug.Log("Shield expires,Cooldown in 10 seconds!");

        isCooldown = true;
        yield return new WaitForSeconds(1f);
        isCooldown = false;
    }
    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);  // Hủy đối tượng
        Debug.Log("Shield expires.");
    }
}
