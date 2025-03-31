using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;
    [SerializeField] GameObject[] EnemyPrefab;
    [SerializeField] float SpawnRate = 2f;
    [SerializeField] float radius = 30f;
    [SerializeField] Tilemap WaterTileMap;
    private float time = 0f;
    private int enemySpawnedCount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time >= SpawnRate)
        {
            time = 0f;
            SpawnEnemy();
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, EnemyPrefab.Length);
        float angle = Random.Range(0f, Mathf.PI * 2); // Random angle in radians
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        Vector3 offset = new Vector3(spawnOffset.x, spawnOffset.y, 0);
        bool isWater = IsWater(PlayerPos.position + offset);
        if (isWater)
        {
            time = SpawnRate;
            return;
        }
        GameObject enemy = Instantiate(EnemyPrefab[randomIndex], PlayerPos.position + offset, Quaternion.identity);
        enemySpawnedCount++;
        enemy.GetComponent<EnemyFollow>().player = PlayerPos;

        SpawnRate = 2 - .1f * (enemySpawnedCount.ToString().Length - 1);
    }

    bool IsWater(Vector3 position)
    {
        Vector3Int gridPosition = WaterTileMap.WorldToCell(position);
        TileBase tile = WaterTileMap.GetTile(gridPosition);
        if (tile != null)
        {
            return true;
        }
        return false;
    }
}
