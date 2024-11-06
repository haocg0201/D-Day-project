using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignUpMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField _username;
    [SerializeField] private TMP_InputField _password;
    [SerializeField] private TMP_InputField _scode;
    [SerializeField] private TMP_Text _notifMes;
    //[SerializeField] private Button _login;
    //[SerializeField] private Button _signup;
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

    public void OnSignupButtonCliked()
    {
        if (!isFirebaseInitialized)
        {
            _notifMes.text = "Vui lòng chờ firebase được khởi tạo";
        }
        string username = _username.text;
        string password = _password.text;
        int scode = int.Parse(_scode.text);

        Register(username, password, scode);
    }

    public async void Register(string username, string password, int scode)
    {
        int result = await FirebaseManager.Instance.Register(username, password, scode);

        switch (result)
        {
            case 1:
                _notifMes.text = "Đăng ký thành công!";
                break;
            case 0:
                _notifMes.text = "Lỗi mạng, không phản hồi";
                break;
            case -1:
                _notifMes.text = "Tài khoản đã tồn tại.";
                break;
            default:
                _notifMes.text = "Lỗi không xác định, có thể do chưa đóng tiền mạng hoặc firebase tính phí :((.";
                break;
        }
       
    }

    public void BackToLoginFormBySceneIndex()
    {
        LoadLoginFormBySceneIndex(1); // Signup Scene Index = 2
    }

    private void LoadLoginFormBySceneIndex(int index)
    {
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
