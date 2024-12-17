using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScript : MonoBehaviour
{
    private bool isFirebaseInitialized = false;
    void Start()
    {
        if (FirebaseManager.Instance != null)
        {
            FirebaseManager.Instance.OnFirebaseInitialized += () =>
            {
                isFirebaseInitialized = true;
                Debug.Log("Firebase is ready to use!");
                StartCoroutine(LoadLogin());
            };
        }
        else
        {
            Debug.Log("Firebase is not initialized yet!");
        }

        
    }

    private IEnumerator LoadLogin()
    {
        while (!isFirebaseInitialized) 
        { 
            yield return null;
        }
        if(AudioManager.Instance != null){
            AudioManager.Instance.PlaySFX(AudioManager.Instance.itemPickupSound);
        }
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1); 
    }
}
