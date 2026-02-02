using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    [SerializeField]
    Text textCoin; // TextCoin UI 요소 참조

    [SerializeField]
    float moveSpeed = 10f; // 골드가 이동하는 속도

    [SerializeField]
    float addTime = 3f;

    [SerializeField]
    GameObject goldPrefab;

    static int coinCount = 0; // 정적 변수로 코인 개수 관리 (모든 Gold 오브젝트가 공유)
    static Text staticTextCoin; // 정적 변수로 TextCoin 참조 저장

    public PlayerManager playerManager;

    void Start()
    {
        // 정적 변수에 저장 (다른 Gold 오브젝트에서도 사용 가능)
        if (textCoin != null && staticTextCoin == null)
        {
            staticTextCoin = textCoin;
        }

        // 초기 UI 업데이트
        UpdateCoinText();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerManager = collision.gameObject.GetComponentInParent<PlayerManager>();
            StartCoroutine(MoveToCoinUI());
        }
    }

    IEnumerator MoveToCoinUI()
    {
        // Collider 비활성화 (중복 충돌 방지)
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // TextCoin의 월드 좌표 가져오기
        Vector3 targetPosition = GetTextCoinWorldPosition();

        // 골드가 TextCoin 위치로 이동
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // 이동 완료 후 코인 개수 증가
        coinCount++;
        UpdateCoinText();

        // 골드 수집 시 남은 시간 3초 증가
        if (playerManager != null)
            playerManager.AddTime(addTime);

        Destroy(gameObject);
    }

    Vector3 GetTextCoinWorldPosition()
    {
        if (staticTextCoin != null)
        {
            // Canvas의 Render Mode에 따라 다르게 처리
            Canvas canvas = staticTextCoin.GetComponentInParent<Canvas>();

            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                // Screen Space Overlay인 경우
                RectTransform rectTransform = staticTextCoin.rectTransform;
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rectTransform.position);

                // UI 좌표를 월드 좌표로 변환
                Camera mainCamera = Camera.main;
                if (mainCamera != null)
                {
                    Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, mainCamera.nearClipPlane + 1f));
                    worldPos.z = transform.position.z; // Z 좌표는 골드의 Z 좌표 유지
                    return worldPos;
                }
            }
            else
            {
                // Screen Space Camera 또는 World Space인 경우
                return staticTextCoin.transform.position;
            }
        }

        // TextCoin을 찾을 수 없는 경우 현재 위치 반환
        return transform.position;
    }

    void UpdateCoinText()
    {
        if (staticTextCoin != null)
        {
            staticTextCoin.text = $"x {coinCount}";
        }
        else if (textCoin != null)
        {
            textCoin.text = $"x {coinCount}";
        }
    }

    // 게임 재시작 시 코인 개수 초기화 (필요한 경우)
    public static void ResetCoinCount()
    {
        coinCount = 0;
        if (staticTextCoin != null)
        {
            staticTextCoin.text = "x 0";
        }
    }
}