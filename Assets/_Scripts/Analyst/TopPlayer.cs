using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro; // Thêm để sử dụng OrderBy và Take

public class TopLevelPlayer : MonoBehaviour
{
    public UnityEngine.UI.Button btnLevel, btnWealth, btnCampaign, btnSvv, btnClose;
    public ScrollRect _scrollView;
    public GameObject goLvl, goWealth, goCampaign, goSvv;
    public GameObject goLvlTitle, goWealthTitle, goCampaignTitle, goSvvTitle;
    RectTransform content;
    [SerializeField]private List<PlayerData> lvls, wealths, campaigns, svvs;
    public GameObject panelTop;

    async void Start()
    {
        var allPlayers = await FirebaseManager.Instance.GetAllPlayers(); 

        lvls = GetTop20PlayersByLevel(allPlayers);
        wealths = GetTop20PlayersByWealth(allPlayers);
        campaigns = GetTop20PlayersByCampaign(allPlayers);
        svvs = GetTop20PlayersBySurvival(allPlayers);

        _scrollView.vertical = true;
        content = _scrollView.content;

        OnLoad();

        btnLevel.onClick.AddListener(() => 
        {
            SetActiveTitle("level");
            DisplayPlayersBy("level");
        });

        btnWealth.onClick.AddListener(() => 
        {
            SetActiveTitle("wealth");
            DisplayPlayersBy("wealth");
        });

        btnCampaign.onClick.AddListener(() => 
        {
            SetActiveTitle("campaign");
            DisplayPlayersBy("campaign");
        });

        btnSvv.onClick.AddListener(() => 
        {
            SetActiveTitle("svv");
            DisplayPlayersBy("svv");
        });

        // btnClose.onClick.AddListener(() =>{
        //     OnClose();
        // });
    }

    private void OnLoad()
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, 0);
        goLvl.SetActive(true);
        goWealth.SetActive(true);
        goCampaign.SetActive(true);
        goSvv.SetActive(true);
        goLvlTitle.SetActive(true);
        DisplayPlayersBy("level");
        if(Player.Instance != null){
            Player.Instance.isConsume = false;
        }
    }

    void OnClose(){
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        Player.Instance.isConsume = true;
        panelTop.SetActive(false);
    }

    public List<PlayerData> GetTop20PlayersByLevel(List<PlayerData> allPlayers)
    {
        var topPlayers = allPlayers.OrderByDescending(player => player.stat.level).Take(20).ToList();
        return topPlayers;
    }

    public List<PlayerData> GetTop20PlayersByWealth(List<PlayerData> allPlayers)
    {
        var topPlayers = allPlayers.OrderByDescending(player => player.stat.gem + player.stat.rune).Take(20).ToList();
        return topPlayers;
    }

    public List<PlayerData> GetTop20PlayersByCampaign(List<PlayerData> allPlayers)
    {
        var topPlayers = allPlayers.OrderByDescending(player => player.campaign.mapA + player.campaign.mapB + player.campaign.mapC).Take(20).ToList();
        return topPlayers;
    }

    public List<PlayerData> GetTop20PlayersBySurvival(List<PlayerData> allPlayers)
    {
        var topPlayers = allPlayers.OrderByDescending(player => player.survival).Take(20).ToList();
        return topPlayers;
    }

    // Hàm hiển thị người chơi theo category
    private void DisplayPlayersBy(string category)
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.buttonClickSound);
        // Xóa các item cũ
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // Điều chỉnh kích thước content theo số lượng players @@ vì số lượng lấy là top20 nên list nào cũng được
        content.sizeDelta = new Vector2(content.sizeDelta.x, lvls.Count * 120); // CC = 120

        // Tạo item mới
        GameObject selectedPrefab = null;
        List<PlayerData> players = null;
        switch (category)
        {
            case "level":
                selectedPrefab = goLvl;
                players = lvls;
                break;
            case "wealth":
                selectedPrefab = goWealth;
                players = wealths;
                break;
            case "campaign":
                selectedPrefab = goCampaign;
                players = campaigns;
                break;
            case "svv":
                selectedPrefab = goSvv;
                players = svvs;
                break;
        }

        int count = 0;
        // Spawn các prefab và gán dữ liệu
        foreach (var player in players)
        {
            
            var item = Instantiate(selectedPrefab, content); // Spawn prefab vào content
            item.transform.localScale = Vector3.one;

            // Gán dữ liệu vào các thành phần trong prefab
            var texts = item.GetComponentsInChildren<TextMeshProUGUI>();
            if (category == "level")
            {
                texts[0].text = $"{player.username}";
                texts[1].text = $"{player.stat.level}";
                SetTitleAndColor(texts,2,count,"Thần linh","Thánh linh","Sứ thần");
                // if (count <= 5){
                //     texts[2].text = "Thần linh";
                //     texts[2].color = new(1f, 0f, 0f);
                // }else if(count > 5 && count <= 10){
                //     texts[2].text = "Thánh linh";
                //     texts[2].color = new(0f, 0f, 1f);
                // }else {
                //     texts[2].text = "Sứ thần";
                //     texts[2].color = new(0.5f, 0f, 1f);
                // }
            }
            else if (category == "wealth")
            { 
                texts[0].text = $"{player.username}";
                texts[1].text = $"{player.stat.gem}";
                texts[2].text = $"{player.stat.rune}";
                SetTitleAndColor(texts,3,count,"Thần Tài","Đại gia","Phú nhị đại");
            }
            else if (category == "campaign")
            {
                texts[0].text = $"{player.username}";
                texts[1].text = $"{player.campaign.mapA}";
                texts[2].text = $"{player.campaign.mapB}";
                texts[3].text = $"{player.campaign.mapC}";
            }
            else if (category == "svv")
            {
                texts[0].text = $"{player.username}";
                texts[1].text = $"{player.survival}";
                SetTitleAndColor(texts,2,count,"Truyền thuyết","Sử thi","Huyền thoại");
            }
            count++;
        }
    }

    private void SetTitleAndColor(TextMeshProUGUI[] texts, int index, int count, string txt1, string txt2, string txt3){
        if (count <= 5){
            texts[index].text = txt1;
            texts[index].color = new(1f, 0f, 0f);
        }else if(count > 5 && count <= 10){
            texts[index].text = txt2;
            texts[index].color = new(0f, 0f, 1f);
        }else {
            texts[index].text = txt3;
            texts[index].color = new(0.5f, 0f, 1f);
        }
    }

    private void SetActiveTitle(string category)
{
    // Tắt tất cả tiêu đề trước
    goLvlTitle.SetActive(false);
    goWealthTitle.SetActive(false);
    goCampaignTitle.SetActive(false);
    goSvvTitle.SetActive(false);

    // Bật tiêu đề phù hợp với danh mục đã chọn
    switch (category)
    {
        case "level":
            goLvlTitle.SetActive(true);
            break;
        case "wealth":
            goWealthTitle.SetActive(true);
            break;
        case "campaign":
            goCampaignTitle.SetActive(true);
            break;
        case "svv":
            goSvvTitle.SetActive(true);
            break;
    }
}
}
