using UnityEngine;

public class InGameAudio : MonoBehaviour
{
    public void PlayInGameBGM()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(BGM.InGame);
        }
    }
}
