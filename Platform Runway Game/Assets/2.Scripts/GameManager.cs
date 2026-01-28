using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Intro,
    Play,
    Dead,
    End,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.Intro;

    public GameState stateEnd = GameState.End;

    public GameObject startBtn;

    [SerializeField]
    GameObject startTimer;
    [SerializeField]
    GameObject startDistance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        startBtn.SetActive(true);
        startTimer.SetActive(true);
        startDistance.SetActive(true);

        // 게임 시작 전 멈춤
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if(state == GameState.Intro)
        {
            state = GameState.Play;
            startBtn.SetActive(false);
            // 게임 재개
            Time.timeScale = 1f;
        }
    }


}
