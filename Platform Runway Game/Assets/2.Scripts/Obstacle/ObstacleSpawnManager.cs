using System.Collections;
using UnityEngine;

public class ObstacleSpawnManager : MonoBehaviour
{
    [SerializeField]
    MapData mapData;
    [SerializeField]
    GameObject[] enemyPrefabs;
    [SerializeField]
    float spawn;

    [SerializeField]
    GameObject alertPrefabs;

    [SerializeField]
    RectTransform canvasTransform;    

    void Start()
    {       
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            float positionX = Random.Range(mapData.minDr.x, mapData.maxDr.x);

            GameObject alert = Instantiate(alertPrefabs, new Vector3(positionX, mapData.maxDr.y, 0), Quaternion.identity);

            yield return new WaitForSeconds(0.5f);

            Destroy(alert);

            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyprefab = Instantiate(enemyPrefabs[randomIndex], new Vector3(positionX, mapData.maxDr.y + 1f, 0f), Quaternion.identity);
                       
            yield return new WaitForSeconds(spawn);
        }
    }
}
