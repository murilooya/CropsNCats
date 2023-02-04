using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public static TerrainSpawner Instance;

    [SerializeField] private TerrainTile TilePrefab;
    public Vector2Int Size = new Vector2Int(20, 10);
    public TerrainTile[,] Terrains;

    private void Awake()
    {
        Instance = this;
        Terrains = new TerrainTile[Size.x, Size.y];
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Terrains[i, j] = TerrainTile.Instantiate(TilePrefab, new Vector3(i, j, 0), Quaternion.identity);
                Terrains[i, j].transform.parent = this.transform;
                bool isEdge = (i == 0 && (j == 0 || j == Size.y - 1)) || 
                    (j == 0 && (i==0 || i == Size.x -1)) ||
                    (i == Size.x - 1 && (j == 0 || j == Size.y - 1)) ||
                    (j == Size.y -1 && (i == 0 || i == Size.x - 1));
                Terrains[i, j].GetComponent<TerrainTile>().IsEdge = isEdge;
            }
        }
    }
}
