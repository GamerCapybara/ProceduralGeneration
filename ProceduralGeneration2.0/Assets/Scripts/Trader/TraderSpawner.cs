using UnityEngine;
using UnityEngine.Tilemaps;

public class TraderSpawner : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;
    [SerializeField] GameObject Trader;
    [SerializeField] float SpawnRate = 90f;
    [SerializeField] float radius = 30f;
    [SerializeField] Tilemap WaterTileMap;
    float time = 0f;

    public GameObject inventoryUI;
    public GameManager gameManager;
    public Inventory inventory;


    // Update is called once per frame
    void Update()
    {
        if (time >= SpawnRate)
        {
            time = 0f;
            SpawnTrader();
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    void SpawnTrader()
    {
        float angle = Random.Range(0f, Mathf.PI * 2); // Random angle in radians
        Vector2 spawnOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        Vector3 offset = new Vector3(spawnOffset.x, spawnOffset.y, 0);
        bool isWater = IsWater(PlayerPos.position + offset);
        if (isWater)
        {
            time = SpawnRate;
            return;
        }
        GameObject trader = Instantiate(Trader, PlayerPos.position + offset, Quaternion.identity);
        Destroy(trader, 20f);
        trader.GetComponent<Trader>().inventoryUI = inventoryUI;
        trader.GetComponent<Trader>().gameManager = gameManager;
        trader.GetComponent<Trader>().inventory = inventory;
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
