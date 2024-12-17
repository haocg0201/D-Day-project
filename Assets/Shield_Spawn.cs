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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isCooldown)
            {
                WorldWhisperManager.Instance.TextBayLen("Kỹ năng đang hồi");
            }
            else
            {
                if (GameManager.Instance != null && GameManager.Instance.playerData.skill.skillA)
                {
                    if (GameManager.Instance.Mana < 20)
                    {
                        WorldWhisperManager.Instance.TextBayLen("Bạn không đủ mana");
                        return;
                    }

                    Player.Instance.UpdateMana(-20);
                    StartCoroutine(CreateObjects());

                }
                else
                {
                    WorldWhisperManager.Instance.TextBayLen("Kĩ năng này chưa mở khóa");
                }
            }

        }
    }

    private IEnumerator CreateObjects()
    {
        protectionArea = Instantiate(Effect_Shield, player.transform.position, Quaternion.identity);
        protectionArea.transform.SetParent(player.transform);

        protectionArea.transform.position = player.transform.position;

        Debug.Log("Shield active in 5 seconds!");
        int def = GameManager.Instance.Def;
        GameManager.Instance.Def += 1000;
        StartCoroutine(DestroyAfterTime(protectionArea, 5f));
        GameManager.Instance.Def = def;

        Debug.Log("Shield expires,Cooldown in 10 seconds!");

        isCooldown = true;
        yield return new WaitForSeconds(10f);
        isCooldown = false;
    }
    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);  // Hủy đối tượng
        Debug.Log("Shield expires.");
    }
}
