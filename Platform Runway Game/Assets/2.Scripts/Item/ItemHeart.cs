using UnityEngine;

public class ItemHeart : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerManager player = collision.GetComponent<PlayerManager>();
        if (player == null)
            return;

        player.AddHP(1); // 최대 3은 PlayerManager.AddHP 안에서 처리
        Destroy(gameObject);
    }
}