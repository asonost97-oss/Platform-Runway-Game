using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public Slider LoadingSlider;
    public TMP_Text LoadingProgressTxt;

    private AsyncOperation m_AsyncOperation;

    void Start()
    {
        // 필수 컴포넌트 체크
        if (LoadingSlider == null)
        {
            Debug.LogError("LoadingSlider가 할당되지 않았습니다!");
            return;
        }

        if (LoadingProgressTxt == null)
        {
            Debug.LogError("LoadingProgressTxt가 할당되지 않았습니다!");
            return;
        }

        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instance가 null입니다! SceneLoader가 씬에 있는지 확인하세요.");
            return;
        }

        StartCoroutine(LoadGameCo());
    }

    IEnumerator LoadGameCo()
    {
        // SceneLoader가 존재하는지 다시 한 번 확인
        if (SceneLoader.Instance == null)
        {
            Debug.LogError("SceneLoader.Instance가 null입니다!");
            yield break;
        }

        m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);

        if (m_AsyncOperation == null)
        {
            Debug.LogError("LoadSceneAsync가 null을 반환했습니다!");
            yield break; // 코루틴 강제 종료
        }

        m_AsyncOperation.allowSceneActivation = false; // 장면 활성화 x

        float minLoadingTime = 2f; // 최소 로딩 시간 (2초)
        float elapsedTime = 0f; // 경과 시간
        float displayProgress = 0f; // 화면에 표시할 진행률

        LoadingSlider.value = 0f;
        LoadingProgressTxt.text = "0%";

        while (elapsedTime < minLoadingTime || !m_AsyncOperation.isDone)
        {
            elapsedTime += Time.deltaTime;

            // 시간 기반 진행률 (최소 3초 동안 0 ~ 90%까지)
            float timeProgress = Mathf.Clamp01(elapsedTime / minLoadingTime) * 0.9f;

            // 실제 로딩 진행률
            float actualProgress = m_AsyncOperation.progress;

            // 둘 중 더 작은 값 사용 (시간 기반과 실제 진행률 중)
            float targetProgress = Mathf.Min(timeProgress, actualProgress);

            // 부드럽게 진행률 증가
            displayProgress = Mathf.Lerp(displayProgress, targetProgress, Time.deltaTime * 2f);

            // UI 업데이트
            LoadingSlider.value = displayProgress;
            LoadingProgressTxt.text = $"{(int)(displayProgress * 100)}%";

            // 실제 로딩이 완료되고 최소 시간도 지났을 때
            if (m_AsyncOperation.progress >= 0.9f && elapsedTime >= minLoadingTime)
            {
                // 100%까지 채우기
                while (displayProgress < 1f)
                {
                    displayProgress = Mathf.Lerp(displayProgress, 1f, Time.deltaTime * 5f);
                    LoadingSlider.value = displayProgress;
                    LoadingProgressTxt.text = $"{(int)(displayProgress * 100)}%";

                    if (displayProgress >= 0.99f)
                    {
                        LoadingSlider.value = 1f;
                        LoadingProgressTxt.text = "100%";
                        break;
                    }

                    yield return null;
                }

                m_AsyncOperation.allowSceneActivation = true;
                yield break;
            }

            yield return null;
        }
    }
}