using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    private AsyncOperation m_AsyncOperation;

    private void Start()
    {
        // Lobby BGM 재생
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayBGM(BGM.Lobby);
            Debug.Log("Lobby BGM 재생 시도");
        }
        else
        {
            Debug.LogError("AudioManager.Instance가 null입니다!");
        }
    }

    public void OnClickStart()
    {
        // Select SFX 재생
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(SFX.Select);
        }

        // 2초 후 씬 로드
        Invoke("LoadInGameScene", 0.5f);
    }

    private void LoadInGameScene()
    {
        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.InGame);
    }

    public void OnClickExitGame()
    {

#if UNITY_EDITOR
        // 에디터에서는 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID || UNITY_IOS
        // 모바일에서는 애플리케이션 종료
        Application.Quit();
#else
        // PC 빌드에서는 애플리케이션 종료
        Application.Quit();
#endif
    }
}