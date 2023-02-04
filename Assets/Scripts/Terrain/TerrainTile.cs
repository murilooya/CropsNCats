using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [System.Serializable]
    public class TerrainColor
    {
        public Type Type;
        public Sprite Sprite;
    }
    public enum Type
    {
        None = 0,
        Dirt = 1,
        Plowed = 2,
        PlowedAndWatered = 3,
        Wall = 4
    }

    private Type _type;
    public Type MyType
    {
        get
        {
            return _type;
        }
        set
        {
            _renderer.sprite = _dicTerrainColors[value];
            _type = value;
        }
    }
    public bool IsEdge = false;
    public TerrainColor[] TerrainColors;
    private Dictionary<Type, Sprite> _dicTerrainColors = new Dictionary<Type, Sprite>();
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        foreach (TerrainColor terrainColor in TerrainColors)
        {
            _dicTerrainColors.Add(terrainColor.Type, terrainColor.Sprite);
        }
        int length = System.Enum.GetValues(typeof(Type)).Length;
        int r = 0;
        while (r == 0 || r == 2 || r == 3)
        {
            if (IsEdge)
            {
                r = (int)Type.Dirt;
            }
            else
            {
                r = UnityEngine.Random.Range(1, length);
            }
        }
        MyType = (Type)r;
    }
}
