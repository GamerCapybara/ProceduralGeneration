using UnityEngine;

using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform player; // Reference to the player
    public TilemapGeneration tilemapGeneration; // Reference to TilemapGeneration
    public int maxHorizontalChecks = 50; // Maximum tiles to check vertically for a walkable position
    
    void Start()
    {
        
    }
}
