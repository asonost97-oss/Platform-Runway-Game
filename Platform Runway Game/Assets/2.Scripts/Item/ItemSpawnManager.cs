using System.Collections;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField]
    int dScore = 2;

    PlayerManager playerManager;

    [SerializeField]
    GameObject[] itemPrefab;

    [SerializeField]
    float aliveTimeAfterSpawn = 5;
    bool allowCollect = true;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void Setup()
    {
        StartCoroutine(nameof(SpawnItemProcess));
    }

    IEnumerator SpawnItemProcess()
    {
        allowCollect = false;

        var rigid = gameObject.AddComponent<Rigidbody2D>();
        rigid.freezeRotation = true;

        allowCollect = true;
        GetComponent<Collider2D>().isTrigger = false;

        yield return new WaitForSeconds(aliveTimeAfterSpawn);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (allowCollect && collision.transform.CompareTag("Player"))
        {
            UpdateCollision(collision.transform);
            Destroy(gameObject);
        }
    }

    public abstract void UpdateCollision(Transform target);
}
