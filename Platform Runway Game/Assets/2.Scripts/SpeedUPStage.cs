using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUPStage : MonoBehaviour
{
    [SerializeField]
    Scroller scroller;
    [SerializeField]
    GameObject speedUpTextObject;
    [SerializeField]
    float displayDuration = 2f;
    [SerializeField]
    float blinkSpeed = 1.5f;

    Text speedUpText;
    Scroller.Level lastLevel;
    Coroutine blinkCoroutine;

    void Start()
    {
        if (speedUpTextObject != null)
        {
            speedUpText = speedUpTextObject.GetComponent<Text>();
            speedUpTextObject.SetActive(false);
        }

        if (scroller != null)
            lastLevel = scroller.CurrentLevel;
    }

    void Update()
    {
        if (scroller == null || speedUpTextObject == null)
            return;

        if (scroller.CurrentLevel != lastLevel)
        {
            lastLevel = scroller.CurrentLevel;
            ShowSpeedUpText();
        }
    }

    void ShowSpeedUpText()
    {
        speedUpTextObject.SetActive(true);

        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);

        blinkCoroutine = StartCoroutine(BlinkAndHide());
    }

    IEnumerator BlinkAndHide()
    {
        float elapsed = 0f;

        while (speedUpTextObject != null && speedUpTextObject.activeSelf && elapsed < displayDuration)
        {
            if (speedUpText != null)
            {
                Color green = Color.green;
                Color white = Color.white;
                float lerp = Mathf.PingPong(elapsed * blinkSpeed, 1f);
                speedUpText.color = Color.Lerp(green, white, lerp);
            }

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        if (speedUpTextObject != null)
            speedUpTextObject.SetActive(false);

        blinkCoroutine = null;
    }
}
