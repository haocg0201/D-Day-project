using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Unity.VisualScripting;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }
    public DatabaseReference DbReference { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // giu FirebaseManager khi chuyen scene nhe
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DbReference = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase Database initialized !");
            }
            else
            {
                Debug.Log("Could not resolve all Firebase dependencies: " + task.Exception);
            }
        });
    }
}
