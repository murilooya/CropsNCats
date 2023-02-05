using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTile : MonoBehaviour
{
    [System.Serializable]
    public class TerrainSprite
    {
        public Type Type;
        public Sprite[] Sprites;
    }
    public enum Type
    {
        None = 0,
        Dirt = 1,
        Plowed = 2,
        PlowedAndWatered = 3,
        Wall = 4,
        Planted = 5
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
            Sprite spr = null;
            if (_dicTerrainSprites.ContainsKey(value) && _dicTerrainSprites[value] != null && _dicTerrainSprites[value].Length > 0)
            {
                if (value != Type.PlowedAndWatered && value != Type.Planted)
                {
                    randomIndex = UnityEngine.Random.Range(0, _dicTerrainSprites[value].Length);
                }
                spr = _dicTerrainSprites[value][randomIndex];
            }
            _renderer.sprite = spr;
            _type = value;
        }
    }
    private int randomIndex = 0;

    public bool IsEdge = false;
    public TerrainSprite[] TerrainSprites;
    public Sprite[] Flowers;
    private Dictionary<Type, Sprite[]> _dicTerrainSprites = new Dictionary<Type, Sprite[]>();
    private SpriteRenderer _renderer;
    public Vector2Int Coords;
    public int PlayerId;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        foreach (TerrainSprite terrainColor in TerrainSprites)
        {
            _dicTerrainSprites.Add(terrainColor.Type, terrainColor.Sprites);
        }
        int length = System.Enum.GetValues(typeof(Type)).Length;
        int r = 0;
        while (r == 0 || r == 2 || r == 3 || r == 5)
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

    public void Blossom()
    {
        _renderer.sprite = null;
        SpriteRenderer spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spr.sprite = Flowers[PlayerId];
        spr.flipX = UnityEngine.Random.value > 0.5f;
    }
}
