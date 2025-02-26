using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgetPassword : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_Text _notifMes;
    public Button _btnGetPW;
    public Button _btnBackLogin;
    private bool isFirebaseInitialized = false;
    void Start()
    {
        if (FirebaseManager.Instance != null)
        {
            isFirebaseInitialized = true;
            //Debug.Log("Firebase is ready to use!");
        }
        else
        {
            Debug.Log("Firebase is not initialized yet!");
        }
        _btnBackLogin.onClick.AddListener(OnBackLogin);
        _btnGetPW.onClick.AddListener(OnGetPW);


    }

    public async void FGPW(string username,  int scode)
    {
        string result = await FirebaseManager.Instance.ForgotPassword(username, scode);

        if(result == "scode" || result == "username"){
            _notifMes.text = "Sai thông tin tài khoản";
        }else if(result == "error"){
            _notifMes.text = "Lỗi mạng, lấy dữ liệu người dùng thất bại";
        }else{
            _notifMes.text = $"Mật khẩu của bạn: {result}";
        }      
    }

    public void OnGetPW(){
        string username = _username.text;
        int scode = int.Parse(_password.text);
        if(AudioManager.Instance != null){
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
        }
        FGPW(username,scode);
    }

    public void OnBackLogin()
    {
        LoadLoginFormBySceneIndex(1); // Signup Scene Index = 2
    }

    private void LoadLoginFormBySceneIndex(int index)
    {
        if(AudioManager.Instance != null){
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
        }
        StartCoroutine(LoadLoginSceneAsync(index));
    }

    private IEnumerator LoadLoginSceneAsync(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        while (!asyncOperation.isDone)
        {
            yield return null; // ở đây đợi khung hình tiếp theo, nếu vẫn chưa load xong thì vẫn đợi 
        }
    }
}
