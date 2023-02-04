using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TerrainSpawner : MonoBehaviour
{
    public static TerrainSpawner Instance;

    [SerializeField] private TerrainTile TilePrefab;
    public Vector2Int Size = new Vector2Int(20, 10);
    public TerrainTile[,] Terrains;

    public Action<TerrainTile> flowerBlossom;

    private void Awake()
    {
        Instance = this;
        Terrains = new TerrainTile[Size.x, Size.y];
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Terrains[i, j] = TerrainTile.Instantiate(TilePrefab, new Vector3(i, j, 0), Quaternion.identity);
                Terrains[i, j].Coords = new Vector2Int(i, j);
                Terrains[i, j].transform.parent = this.transform;
                bool isEdge = (i == 0 && (j == 0 || j == Size.y - 1)) || 
                    (j == 0 && (i==0 || i == Size.x -1)) ||
                    (i == Size.x - 1 && (j == 0 || j == Size.y - 1)) ||
                    (j == Size.y -1 && (i == 0 || i == Size.x - 1));
                Terrains[i, j].GetComponent<TerrainTile>().IsEdge = isEdge;
            }
        }
    }

    public Vector2Int GetInitialCoordsForPlayerId(int id)
    {
        switch (id)
        {
            case 0:
                return Vector2Int.zero;
            case 1:
                return new Vector2Int(Size.x - 1, Size.y - 1);
            case 2:
                return new Vector2Int(0, Size.y - 1);
            case 3:
                return new Vector2Int(Size.x - 1, 0);
            default:
                return -Vector2Int.one;
        }       
    }
    public IEnumerator BlossomFlowers()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                if (Terrains[i,j].MyType != TerrainTile.Type.Planted)
                {
                    continue;
                }
                Terrains[i, j].Blossom();
                flowerBlossom?.Invoke(Terrains[i, j]);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
