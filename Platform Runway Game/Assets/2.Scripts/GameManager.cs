using UnityEngine;

public enum GameState
{
    Intro,
    Play,
    Dead,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.Intro;

    public GameObject startBtn;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            state = GameState.Play;

            startBtn.SetActive(false);
        }
    }
}
