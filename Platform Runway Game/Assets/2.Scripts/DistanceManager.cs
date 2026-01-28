using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    Text textDistance; // TextDistance UI ��� ����

    [SerializeField]
    Scroller scroller; // Scroller ���� (��� �̵� �ӵ� ��������)

    float totalDistance = 0f; // ���� �̵� �Ÿ� (����)

    void Start()
    {
        // �ʱ� UI ������Ʈ
        UpdateDistanceText();
    }

    void Update()
    {
        // ������ Play ������ ���� ����
        if (GameManager.Instance != null && GameManager.Instance.state == GameState.Play)
        {
            if (scroller != null)
            {
                // Scroller�� �ӵ��� ������ ����Ͽ� �̵� �Ÿ� ���
                // moveDr * speed * Time.deltaTime�� ����� �̵����̹Ƿ�
                float distanceThisFrame = scroller.MoveDr.magnitude * scroller.Speed * Time.deltaTime;

                // ���� �Ÿ��� �߰�
                totalDistance += distanceThisFrame;

                // �� �����Ӹ��� UI ������Ʈ
                UpdateDistanceText();
            }
        }
    }

    void UpdateDistanceText()
    {
        if (textDistance != null)
        {
            // �Ҽ��� 3�ڸ����� ǥ�� (0.000 ����)
            textDistance.text = $"M: {totalDistance:F1}";
        }
    }
}