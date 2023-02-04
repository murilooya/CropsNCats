using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Vector2Int _size = new Vector2Int(18, 10);
    private GameObject[,] _terrainsRef;

    private void Awake()
    {
        _terrainsRef = new GameObject[_size.x, _size.y];
        for (int i = 0; i < _size.x; i++)
        {
            for (int j = 0; j < _size.y; j++)
            {
                _terrainsRef[i, j] = Instantiate(_sprite, new Vector3(i, j, 0), Quaternion.identity).gameObject;
                _terrainsRef[i, j].transform.parent = this.transform;
            }
        }
    }
}
