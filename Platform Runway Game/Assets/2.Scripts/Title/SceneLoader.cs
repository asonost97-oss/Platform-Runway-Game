using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneType
{
    Title,
    Lobby,
    InGame,
}

public class SceneLoader : SingletoneBehaviour<SceneLoader>
{
    public void LoadScene(SceneType sceneType)
    {
        Time.timeScale = 1f; //일시정지했을 때 타임스케일이 0이 될 수도 있고
        //게임 기획상 타임스케일이 1이 아닌 경우도 있을 수 있기 때문에 씬을 로딩했을 때 타임스케일을 초기화해줌
        SceneManager.LoadScene(sceneType.ToString());
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public AsyncOperation LoadSceneAsync(SceneType sceneType)
    {
        Time.timeScale = 1f;
        return SceneManager.LoadSceneAsync(sceneType.ToString());
    }
}
