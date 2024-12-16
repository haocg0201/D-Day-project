using System.Collections;
using UnityEngine;

public class Lightning_Spawn : MonoBehaviour
{
    public GameObject player;
    public GameObject Effect_Lightning;

    public Vector3[] positions = new Vector3[3]; //vi tri xung quanh player
    private GameObject[] instances = new GameObject[3]; // luu tru 3 gameobjects

    private bool isCooldown = false;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && !isCooldown)
        {
            if (GameManager.Instance != null && GameManager.Instance.playerData.skill.skillB)
            {
                if (GameManager.Instance.Mana < 20)
                {
                    WorldWhisperManager.Instance.TextBayLen("Bạn không đủ mana");
                    return;
                }

                Player.Instance.UpdateMana(-20);
                StartCoroutine(CreateObjects());
                for (int i = 0; i < instances.Length; i++)
                {
                    if (instances[i] != null)
                        instances[i].transform.position = player.transform.position + positions[i];
                }
            }
            else
            {
                WorldWhisperManager.Instance.TextBayLen("Kĩ năng này chưa mở khóa");
            }


        }

    }


    private IEnumerator CreateObjects()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (instances[i] == null)
            {
                Vector3 spawnPosition = player.transform.position + positions[i];
                instances[i] = Instantiate(Effect_Lightning, spawnPosition, Quaternion.identity);
                Debug.Log("Lightning Ball active in 15 seconds!");
                StartCoroutine(DestroyAfterTime(instances[i], 5f));

            }
        }
        isCooldown = true;
        yield return new WaitForSeconds(10f);
        Debug.Log("Cooldown in 10 seconds!");
        isCooldown = false;
    }
    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);  // Hủy đối tượng
        Debug.Log("Lightning Ball expires.");
    }
}
