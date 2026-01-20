using UnityEngine;

public enum ItemType { PowerUp = 0, Skill, }

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    ItemType itemType;
    SpeedManager moveController;

    [SerializeField]
    MapData mapData;

    private void Start()
    {
        moveController = GetComponent<SpeedManager>();
    }

    private void Update()
    {
        DontoutMap();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void DontoutMap()
    {
        //플레이어 화면 범위 밖으로 나가지 못하게 함
        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, mapData.minDr.x, mapData.maxDr.x),
            Mathf.Clamp(transform.position.y, mapData.minDr.y, mapData.maxDr.y));
    }
}
