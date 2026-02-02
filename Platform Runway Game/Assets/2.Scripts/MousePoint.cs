using UnityEngine;
using UnityEngine.EventSystems;

public class MousePoint : MonoBehaviour
{
    Camera mainCamera;
    Ray ray;
    RaycastHit hit;

    Transform hitTransform = null;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hitTransform = hit.transform;

                // Select SFX Àç»ý
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlaySFX(SFX.Select);
                }
            }
        }
    }
}
