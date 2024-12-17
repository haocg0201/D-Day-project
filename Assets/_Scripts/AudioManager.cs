using UnityEngine;

public class AudioManager : MonoBehaviour
{
     public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;  

    [Header("Audio Clips - Campaingn + svv")]
    public AudioClip campA;
    public AudioClip campB;
    public AudioClip campC;
    public AudioClip campD;
    public AudioClip svv;

    [Header("Audio Clips - Music")]
    public AudioClip backgroundMusic;
    public AudioClip bossFightMusic;

    [Header("Audio Clips - Player SFX")]
    public AudioClip playerAttackSound;
    public AudioClip playerUpgradeSound;
    public AudioClip playerHurtSound;
    public AudioClip playerExhaustedSound;
    public AudioClip playerStepSound;


    [Header("Audio Clips - Enemy SFX")]
    public AudioClip enemyAttackSound;
    public AudioClip enemyDeathSound;
    public AudioClip bossDeathSound;

    [Header("Audio Clips - UI SFX")]
    public AudioClip buttonClickSound;
    public AudioClip itemPickupSound;

    [Header("Audio Clips - Weapon shoot")]
    public AudioClip weaponShootSound;
    public AudioClip bulletSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        musicSource.volume = 0.5f;
        sfxSource.volume = 0.5f;
    }

    public void MuteMusic(){
        musicSource.volume = 0;
    }

    public void MuteSFX(){
        sfxSource.volume = 0;
    }
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.isPlaying)
            musicSource.Stop();  // Dừng nhạc cũ nếu có

        musicSource.clip = clip;  // Cập nhật clip mới
        musicSource.Play();       // Phát nhạc nền
    }

    // Phát hiệu ứng âm thanh
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);  // Phát hiệu ứng âm thanh
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("SetSFXVolume called with: " + volume);
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}
