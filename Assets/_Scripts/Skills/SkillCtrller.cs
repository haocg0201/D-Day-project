using UnityEngine;
using UnityEngine.UI;

public class SkillCtrller : MonoBehaviour
{
    [Header("Skill Buttons")]
    public Button skillButtonQ;
    public Button skillButtonE;
    public Button skillButtonR;
    public Button skillButtonT;
    public Button skillButtonY;

    [Header("Skill Lock Images")]
    public Image lockImageA;
    public Image lockImageB;
    public Image lockImageC;
    public Image lockImageD;
    public Image lockImageE;

    private void Start()
    {
        CheckSkills();
    }

    private void Update() {
        CheckSkills();
    }

    public void CheckSkills()
    {
        if (GameManager.Instance == null || GameManager.Instance.playerData == null)
        {
            Debug.LogError("GameManager or playerData is null!");
            return;
        }

        var skills = GameManager.Instance.playerData.skill;

        // Kiểm tra từng skill và ẩn/hiện ảnh khóa tương ứng
        lockImageA.gameObject.SetActive(!skills.skillA);
        lockImageB.gameObject.SetActive(!skills.skillB);
        lockImageC.gameObject.SetActive(!skills.skillC);
        lockImageD.gameObject.SetActive(!skills.skillD);
        lockImageE.gameObject.SetActive(!skills.skillE);
    }
}
