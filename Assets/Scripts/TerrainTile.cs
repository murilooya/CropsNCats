using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [System.Serializable]
    public class TerrainColor
    {
        public Type Type;
        public Color Color;
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
            _renderer.color = _dicTerrainColors[value];
            _type = value;
        }
    }

    public TerrainColor[] TerrainColors;
    private Dictionary<Type, Color> _dicTerrainColors = new Dictionary<Type, Color>();
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        foreach (TerrainColor terrainColor in TerrainColors)
        {
            _dicTerrainColors.Add(terrainColor.Type, terrainColor.Color);
        }
        int length = System.Enum.GetValues(typeof(Type)).Length;
        MyType = (Type)UnityEngine.Random.Range(1, length);
    }
}
