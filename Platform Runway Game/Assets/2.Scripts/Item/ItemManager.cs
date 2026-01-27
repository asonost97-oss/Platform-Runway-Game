using UnityEngine;

public enum ItemType { Life, }

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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
