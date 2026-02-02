using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    Slider backSlider;
    [SerializeField]
    Slider keySlider;
    [SerializeField]
    GameObject gameAudioUI;

    void Update()
    {
        // AudioManager? BGM / SFX ??? ????? ??
        if (AudioManager.Instance != null)
        {
            if (backSlider != null)
                AudioManager.Instance.SetBGMVolume(backSlider.value);
            if (keySlider != null)
                AudioManager.Instance.SetSFXVolume(keySlider.value);
        }

        if (gameAudioUI == null) return;

        if (gameAudioUI.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                OpenAudioUI();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                CloseAudioUI();
        }
    }

    public void OpenAudioUI()
    {
        if (gameAudioUI == null) return;
        Time.timeScale = 0f;
        gameAudioUI.SetActive(true);
        PauseMusic();
    }

    public void CloseAudioUI()
    {
        if (gameAudioUI == null) return;
        Time.timeScale = 1f;
        gameAudioUI.SetActive(false);
        ResumeMusic();
    }

    private void PauseMusic()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PauseBGM();
    }

    private void ResumeMusic()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.ResumeBGM();
    }
}
