using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    MapData mapData;

    float destroyW = 15f;

    private void LateUpdate()
    {
        if (transform.position.y < mapData.minDr.y - destroyW ||
            transform.position.y > mapData.maxDr.y + destroyW ||
            transform.position.x < mapData.minDr.x - destroyW ||
            transform.position.x > mapData.maxDr.x + destroyW)
        {
            Destroy(gameObject);
        }
    }
}
