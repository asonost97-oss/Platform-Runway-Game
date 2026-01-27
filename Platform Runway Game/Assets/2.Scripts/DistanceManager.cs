using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    Text textDistance; // TextDistance UI 요소 참조

    [SerializeField]
    Scroller scroller; // Scroller 참조 (배경 이동 속도 가져오기)

    float totalDistance = 0f; // 누적 이동 거리 (미터)

    void Start()
    {
        // 초기 UI 업데이트
        UpdateDistanceText();
    }

    void Update()
    {
        // 게임이 Play 상태일 때만 동작
        if (GameManager.Instance != null && GameManager.Instance.state == GameState.Play)
        {
            if (scroller != null)
            {
                // Scroller의 속도와 방향을 사용하여 이동 거리 계산
                // moveDr * speed * Time.deltaTime가 배경의 이동량이므로
                float distanceThisFrame = scroller.MoveDr.magnitude * scroller.Speed * Time.deltaTime;

                // 누적 거리에 추가
                totalDistance += distanceThisFrame;

                // 매 프레임마다 UI 업데이트
                UpdateDistanceText();
            }
        }
    }

    void UpdateDistanceText()
    {
        if (textDistance != null)
        {
            // 소수점 3자리까지 표시 (0.000 형식)
            textDistance.text = $"M: {totalDistance:F1}";
        }
    }
}