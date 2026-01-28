using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리를 위해 추가

public class StageClearEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverText; // GameOver 텍스트 오브젝트

    [SerializeField]
    PlayerTime playerTime; // 시간 참조

    [SerializeField]
    private PlayerManager playerManager; // 플레이어 매니저 참조

    private Text GameOverText; // GameOver Text 컴포넌트

    private bool isGameOver = false;
    private bool isTimeOver = false; // 시간 초과 체크 중인지 확인

    private Coroutine colorEffectCoroutine;

    AsyncOperation m_AsyncOperation;

    private void Start()
    {
         GameOverText = gameOverText.GetComponent<Text>();
    }

    private void Update()
    {
        // 게임 오버 상태가 아닐 때만 체크
        if (!isGameOver && !isTimeOver)
        {
            // 시간 초과 체크: 시간이 0 이하가 되었을 때
            if (GameManager.Instance != null && GameManager.Instance.state == GameState.End)
            {
                Debug.Log("시간 초과 감지! 2초 후 GameOver 표시...");
                StartCoroutine(ShowTimeOverDefeatWithDelay());
                return; // 시간 초과했으면 더 이상 체크하지 않음
            }
        }
    }

    private IEnumerator ShowTimeOverDefeatWithDelay()
    {
        isTimeOver = true;

        // 2초 대기
        yield return new WaitForSeconds(2f);

        Debug.Log("시간 초과로 인한 GameOver 표시!");
        ShowDefeatEffect();
    }

    private void ShowDefeatEffect()
    {
        isGameOver = true;

        // Lose SFX 재생
        //if (AudioManager.Instance != null)
        //{
        //    AudioManager.Instance.PlaySFX(SFX.Lose);
        //    Debug.Log("Lose SFX 재생!");
        //}

        if (gameOverText != null)
        {
            Debug.Log("GameOver 텍스트 활성화");
            gameOverText.SetActive(true);

            // 기존 코루틴이 실행 중이면 중지
            if (colorEffectCoroutine != null)
            {
                StopCoroutine(colorEffectCoroutine);
            }

            // 검정-회색 색상 효과 적용
            if (GameOverText != null)
                colorEffectCoroutine = StartCoroutine(DefeatColorEffect());
        }

        // 2초 후 게임 일시정지
        StartCoroutine(PauseGameAfterDelay());
    }

    // 2초 후 게임과 BGM 일시정지
    private IEnumerator PauseGameAfterDelay()
    {
        // 2초 대기
        yield return new WaitForSeconds(2f);

        Debug.Log("게임 일시정지!");

        // 게임 일시정지
        Time.timeScale = 0f;

        // BGM 일시정지
        //if (AudioManager.Instance != null)
        //{
        //    AudioManager.Instance.PauseBGM();
        //    Debug.Log("BGM 일시정지!");
        //}
    }

    private IEnumerator DefeatColorEffect()
    {
        if (GameOverText == null) yield break;

        float time = 0f;
        Color black = new Color(0.1f, 0.1f, 0.1f); // 거의 검정
        Color gray = new Color(0.4f, 0.4f, 0.4f); // 회색

        while (gameOverText != null && gameOverText.activeSelf)
        {
            // 검정과 회색 사이를 왔다갔다
            float lerp = Mathf.PingPong(time, 1f);
            Color defeatColor = Color.Lerp(black, gray, lerp);
            GameOverText.color = defeatColor;

            // Time.timeScale에 영향받지 않도록 Time.unscaledDeltaTime 사용
            time += Time.unscaledDeltaTime * 1.5f; // 깜빡임 속도

            yield return null;
        }
    }

    // 스테이지가 시작될 때 호출하여 게임 오버 상태 리셋
    public void OnStageStart()
    {
        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }

        // 실행 중인 코루틴 중지
        if (colorEffectCoroutine != null)
        {
            StopCoroutine(colorEffectCoroutine);
            colorEffectCoroutine = null;
        }

        isGameOver = false;
        isTimeOver = false;
    }

    // 다시 플레이 버튼 클릭 시 호출 (GameOver 후)
    public void OnClickPlayAgain()
    {
        Debug.Log("게임 다시 시작!");

        // 게임 일시정지 해제
        Time.timeScale = 1f;

        // 현재 씬을 다시 로드하여 모든 것을 초기화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickBackToLobby()
    {
        // SceneLoader가 있다면 사용
        //m_AsyncOperation = SceneLoader.Instance.LoadSceneAsync(SceneType.Lobby);
    }
}
