using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button btnMuteMusic;
    public Button btnMuteSFX;

    void Start()
    {
        btnMuteMusic.onClick.AddListener(OnMuteMusic);
        btnMuteSFX.onClick.AddListener(OnMuteSfx);
        if (AudioManager.Instance != null)
        {
            musicSlider.value = AudioManager.Instance.musicSource.volume;
            sfxSlider.value = AudioManager.Instance.sfxSource.volume;

            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }

    void OnMuteMusic(){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
        AudioManager.Instance.MuteMusic();
        musicSlider.value = 0f;
    }

    void OnMuteSfx(){
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClickSound);
        AudioManager.Instance.MuteSFX();
        sfxSlider.value = 0f;
    }
}

