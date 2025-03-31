using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class TilemapGeneration : MonoBehaviour
{
    public Tilemap terrainTilemap;
    public Tilemap waterTilemap;
    public Tilemap resourcesTilemap;
    public TileBase grassTile;
    public TileBase waterTile;
    public TileBase resourcesTile;
    public int chunkSize = 20;
    public int renderDistance = 2; // 1 = 3 x 3

    public int seed;
    public bool useRandomSeed = true;
    
    private Vector2Int _currentChunk;
    private HashSet<Vector2Int> _activeChunks = new();
    
    private int[,] _pathfindingGrid;

    public Transform player;

    private void Awake()
    {
        if (useRandomSeed)
        {
            seed = Random.Range(0, 1_000_000);
        }
        
        Random.InitState(seed);
        
        int gridSize = chunkSize * (2 * renderDistance + 1);
        _pathfindingGrid = new int[gridSize, gridSize];
        
        UpdateMap();
    }

    void Update()
    {
        Vector2Int playerChunk = GetChunk(player.position);

        if (playerChunk != _currentChunk)
        {
            _currentChunk = playerChunk;
            
            ClearPathfindingGrid();
            UpdatePathfindingGrid();
            
            UpdateMap();
        }
    }
    
    Vector2Int GetChunk(Vector3 position)
    {
        int chunkX = Mathf.FloorToInt(position.x / chunkSize);
        int chunkY = Mathf.FloorToInt(position.y / chunkSize);
        return new Vector2Int(chunkX, chunkY);
    }

    void UpdateMap()
    {
        HashSet<Vector2Int> chunksToKeep = new HashSet<Vector2Int>();

        // Generate chunks
        for (int x = _currentChunk.x - renderDistance; x <= _currentChunk.x + renderDistance; x++)
        {
            for (int y = _currentChunk.y - renderDistance; y <= _currentChunk.y + renderDistance; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(x, y);
                chunksToKeep.Add(chunkCoord);

                if (!_activeChunks.Contains(chunkCoord))
                {
                    GenerateChunk(chunkCoord);
                }
            }
        }

        // Unload chunks
        List<Vector2Int> chunksToUnload = new List<Vector2Int>();
        foreach (Vector2Int chunk in _activeChunks)
        {
            if (!chunksToKeep.Contains(chunk))
            {
                chunksToUnload.Add(chunk);
            }
        }

        foreach (Vector2Int chunk in chunksToUnload)
        {
            UnloadChunk(chunk);
        }

        //
        _activeChunks = chunksToKeep;
    }

    void GenerateChunk(Vector2Int chunkCoord)
    {
        StartCoroutine(GenerateChunkCoroutine(chunkCoord));
    }

    IEnumerator GenerateChunkCoroutine(Vector2Int chunkCoord)
    {
        int startX = chunkCoord.x * chunkSize;
        int startY = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                float noise = Mathf.PerlinNoise(
                    (startX + x + seed) * 0.1f, 
                    (startY + y + seed) * 0.1f
                );
                
                Vector3Int tilePos = new Vector3Int(startX + x, startY + y, 0);
                
                terrainTilemap.SetTile(tilePos, null);
                waterTilemap.SetTile(tilePos, null);
                resourcesTilemap.SetTile(tilePos, null);

                if (noise > 0.25f)
                {
                    terrainTilemap.SetTile(tilePos, grassTile);
                    UpdatePathfindingGrid(tilePos, 1);

                    if(Random.value < .01f)
                    {
                        resourcesTilemap.SetTile(tilePos, resourcesTile);
                    }
                }
                else
                {
                    waterTilemap.SetTile(tilePos, waterTile);
                    UpdatePathfindingGrid(tilePos, 0);
                }
                
                // Spread updates over multiple frames for smoother performance
                if ((x * chunkSize + y) % 100 == 0)
                {
                    yield return null;
                }
            }
        }

        _activeChunks.Add(chunkCoord);
    }

    void UnloadChunk(Vector2Int chunkCoord)
    {
        int startX = chunkCoord.x * chunkSize;
        int startY = chunkCoord.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(startX + x, startY + y, 0);
                terrainTilemap.SetTile(tilePos, null);
                waterTilemap.SetTile(tilePos, null);
                UpdatePathfindingGrid(tilePos, -1);
            }
        }

        _activeChunks.Remove(chunkCoord);
    }
    private void ClearPathfindingGrid()
    {
        for (int x = 0; x < _pathfindingGrid.GetLength(0); x++)
        {
            for (int y = 0; y < _pathfindingGrid.GetLength(1); y++)
            {
                _pathfindingGrid[x, y] = -1; // Reset to unloaded state
            }
        }
    }

    private void UpdatePathfindingGrid(Vector3Int tilePos, int value)
    {
        int gridX = tilePos.x - (_currentChunk.x - renderDistance) * chunkSize;
        int gridY = tilePos.y - (_currentChunk.y - renderDistance) * chunkSize;
    
        if (gridX >= 0 && gridX < _pathfindingGrid.GetLength(0) &&
            gridY >= 0 && gridY < _pathfindingGrid.GetLength(1))
        {
            _pathfindingGrid[gridX, gridY] = value;
        }
    }
    
    private void UpdatePathfindingGrid()
    {
        for (int x = _currentChunk.x - renderDistance; x <= _currentChunk.x + renderDistance; x++)
        {
            for (int y = _currentChunk.y - renderDistance; y <= _currentChunk.y + renderDistance; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(x, y);

                // Process each tile in the chunk
                int startX = chunkCoord.x * chunkSize;
                int startY = chunkCoord.y * chunkSize;

                for (int i = 0; i < chunkSize; i++)
                {
                    for (int j = 0; j < chunkSize; j++)
                    {
                        Vector3Int tilePos = new Vector3Int(startX + i, startY + j, 0);

                        // Check only waterTilemap for obstacles
                        if (waterTilemap.HasTile(tilePos))
                        {
                            UpdatePathfindingGrid(tilePos, 0); // Non-walkable (water)
                        }
                        else
                        {
                            UpdatePathfindingGrid(tilePos, 1); // Walkable (not water)
                        }
                    }
                }
            }
        }
    }



    public int[,] GetPathfindingGrid()
    {
        return _pathfindingGrid;
    }
    
}
