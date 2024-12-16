using System.Collections;
using UnityEngine;

public class Gospel_spawn : MonoBehaviour
{
    private bool isGospel = false;

    public GameObject Effect_Gospel;
    public Transform attactPoint;
    private GameObject instantiatedObject; //luu tru doi tuong da instance
    private bool isCooldown = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R) && !isCooldown)
        {
            if(GameManager.Instance != null && GameManager.Instance.playerData.skill.skillD){
            if(GameManager.Instance.Mana < 20){
                WorldWhisperManager.Instance.TextBayLen("Bạn không đủ mana");
                return;
            }
                Player.Instance.UpdateMana(-20);
                if (!isGospel)
                {
                    StartCoroutine(ActivateGospel());
                }
            }else{
                WorldWhisperManager.Instance.TextBayLen("Kĩ năng này chưa mở khóa");
            }
        }
        
    }

    private IEnumerator ActivateGospel()
    {
        isGospel = true; // Bật phúc âm
        GameManager.Instance.Survivability += 1f;
        if (Effect_Gospel != null && attactPoint != null)
        {
            instantiatedObject = Instantiate(Effect_Gospel, attactPoint.position, Quaternion.identity);
            instantiatedObject.transform.SetParent(attactPoint);

            instantiatedObject.transform.position = attactPoint.position;
        }
        Debug.Log("Survivability +1 for 30 seconds!");

        // Đợi trong 30 giây
        yield return new WaitForSeconds(5f);

        Debug.Log("Gospel expires.Cooldown in 10 seconds!");
        isGospel = false; // Tắt phúc âm sau 30 giây
        isCooldown = true;
        GameManager.Instance.Survivability -= 1f;
        Destroy(instantiatedObject);
        yield return new WaitForSeconds(10f);
        isCooldown = false;
    }
}
