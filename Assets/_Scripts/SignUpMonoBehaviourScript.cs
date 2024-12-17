using System.Collections;
using System.Text.RegularExpressions;
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
        if(AudioManager.Instance != null){
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
        }
        if (!isFirebaseInitialized)
        {
            _notifMes.text = "Vui lòng chờ firebase được khởi tạo";
        }
        string username = _username.text;
        string password = _password.text;
        string scode = _scode.text;

        ValidateAccount(username, password, scode);
        //Register(username, password, );
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

    public void ValidateAccount(string username, string password, string scode)
    {
        // Kiểm tra tài khoản có rỗng hoặc chứa khoảng trắng không
        if (string.IsNullOrWhiteSpace(username))
        {
            _notifMes.text = "Tài khoản không được để trống hoặc chứa khoảng trắng.";
            return;
        }

        // Kiểm tra tài khoản có ký tự đặc biệt không
        if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]{5,16}$"))
        {
            _notifMes.text = "Tài khoản chỉ được chứa chữ cái và số, từ 5 đến 16 ký tự.";
            return;
        }

        // Kiểm tra mật khẩu có rỗng không
        if (string.IsNullOrWhiteSpace(password))
        {
            _notifMes.text = "Mật khẩu không được để trống.";
            return;
        }

        // Kiểm tra độ dài của mật khẩu
        if (password.Length < 5 || password.Length > 16)
        {
            _notifMes.text = "Mật khẩu phải từ 5 đến 16 ký tự.";
            return;
        }

        string pin = _scode.text;
        if (pin.Length != 4)
        {
            _notifMes.text = "PIN không hợp lệ. Vui lòng nhập 4 số.";
            return;
        }

        Register(username, password, int.Parse(pin));
    }

    public void BackToLoginFormBySceneIndex()
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
