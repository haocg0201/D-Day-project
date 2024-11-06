using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_Text _notifMes;
    [SerializeField] private Button _login;
    [SerializeField] private Button _signup;
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
    }

    public void OnLoginButtonClicked()
    {
        if (!isFirebaseInitialized)
        {
            _notifMes.text = "Vui lòng chờ Firebase được khởi tạo!";
            return;
        }
        string username = _username.text;
        string password = _password.text;

        Login(username, password);
    }

    private async void Login(string username, string password)
    {
        int result = await FirebaseManager.Instance.Login(username, password);

        switch (result)
        {
            case 1:
                _notifMes.text = "Đăng nhập thành công!";
                break;
            case 0:
                _notifMes.text = "Lỗi mạng, không phản hồi";
                break;
            case -1:
                _notifMes.text = "Tài khoản mật khẩu không chính xác.";
                break;
            //case -2:
            //    _notifMes.text = "Lỗi truy xuất dữ liệu.";
            //    break;
            default:
                _notifMes.text = "Lỗi không xác định.";
                break;
        }
    }


    public void OpenSignupFormBySceneIndex()
    {
        LoadSignupFormBySceneIndex(2); // Signup Scene Index = 2
    }

    private void LoadSignupFormBySceneIndex(int index)
    {
        StartCoroutine(LoadSignupSceneAsync(index));
    }

    private IEnumerator LoadSignupSceneAsync(int index)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        while (!asyncOperation.isDone)
        {
            yield return null; // ở đây đợi khung hình tiếp theo, nếu vẫn chưa load xong thì vẫn đợi 
        } 
    }
}
