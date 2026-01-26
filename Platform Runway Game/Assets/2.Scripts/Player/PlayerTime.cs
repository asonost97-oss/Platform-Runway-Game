using UnityEngine;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerTime;
    [SerializeField]
    Slider playerTimeSlider;
    [SerializeField]
    Text timeText;
    void Start()
    {
        if (playerTimeSlider == null)
            playerTimeSlider = GetComponent<Slider>();

        if (playerTimeSlider != null && playerTime != null)
        {
            playerTimeSlider.maxValue = playerTime.MaxTime;
            playerTimeSlider.value = playerTime.CurrTime;
        }

        if (timeText != null && playerTime != null)
            timeText.text = FormatTime(playerTime.CurrTime);
    }

    void Update()
    {
        if (playerTimeSlider != null && playerTime != null)
        {
            playerTimeSlider.value = playerTime.CurrTime; // 빼지 말고 "세팅"
        }

        if (timeText != null && playerTime != null)
            timeText.text = FormatTime(playerTime.CurrTime); // 180 -> 179... 표시
    }

    string FormatTime(float seconds)
    {
        int s = Mathf.CeilToInt(seconds);
        int m = s / 60;
        int r = s % 60;
        return $"Time: {m:00}:{r:00}";
        // 초로만 보고 싶으면: return $"Time: {s}s";
    }
}
