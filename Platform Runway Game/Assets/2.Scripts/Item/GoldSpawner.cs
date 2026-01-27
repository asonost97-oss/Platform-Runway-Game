using System.Collections;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField]
    MapData mapData;

    [SerializeField]
    GameObject[] itemPrefab; // [0]: °ñµå, [1]: ÇÏÆ® µî

    [SerializeField]
    float goldspawnInterval = 5f;

    [SerializeField]
    float heartspawnInterval = 20f;

    [SerializeField]
    float goldSpacing = 0.7f;

    void Start()
    {
        StartCoroutine(SpawnGold());
        StartCoroutine(SpawnHeart());
    }

    IEnumerator SpawnGold()
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.state == GameState.Play)
            {
                if (itemPrefab != null && itemPrefab.Length > 0 && itemPrefab[0] != null)
                {
                    float randomX = Random.Range(mapData.minDr.x, mapData.maxDr.x);
                    float spawnY = Random.Range(mapData.minDr.y, mapData.maxDr.y);

                    for (int i = 0; i < 3; i++)
                    {
                        float goldX = randomX + (i - 1) * goldSpacing;
                        Instantiate(itemPrefab[0], new Vector3(goldX, spawnY, 0), Quaternion.identity);
                    }
                }
            }

            yield return new WaitForSeconds(goldspawnInterval);
        }
    }

    IEnumerator SpawnHeart()
    {
        while (true)
        {
            if (GameManager.Instance != null && GameManager.Instance.state == GameState.Play)
            {
                if (itemPrefab != null && itemPrefab.Length > 1 && itemPrefab[1] != null)
                {
                    float randomX = Random.Range(mapData.minDr.x, mapData.maxDr.x);
                    float spawnY = Random.Range(mapData.minDr.y, mapData.maxDr.y);
                    Instantiate(itemPrefab[1], new Vector3(randomX, spawnY, 0), Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(heartspawnInterval);
        }
    }
}