using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField]
    int scorePoint = 100;

    int scoreRepeat = 1000;

    PlayerManager playerManager;

    [SerializeField]
    GameObject[] itemPrefab;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    public void SpawnItem()
    {
        //playerManager.Score += scorePoint;

        int spawnItem = Random.Range(0, 100);

        if (scoreRepeat != null)
        {
            Instantiate(itemPrefab[0], transform.position, Quaternion.identity);
        }
    }
}
