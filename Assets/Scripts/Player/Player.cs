using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    public int Id = -1;
    public Joystick MyJoystick;
    public Color MyColor
    {
        set
        {
            GetComponent<SpriteRenderer>().color = value;
        }
        get
        {
            return GetComponent<SpriteRenderer>().color;
        }
    }
    public float Speed = 2;
    public Vector2Int MoveAmount;

    public Vector2Int CurrentCoordinate = Vector2Int.zero;
    public bool IsMoving = false;
    public float MoveTime = 0.5f;
    public AnimationCurve Curve;
    public float HorizontalMoveModifier = 0.5f;
    public float VerticalMoveModifier = 0.2f;
    public TerrainTile NextTile = null;
    public float WaitTimeBetweenMovements = 0.05f;

    public Action<Player, TerrainTile, TerrainTile.Type> modifiedTerrain;

    public void Update()
    {
        Vector2 axis = MyJoystick.stick.ReadValue();
        MoveAmount = new Vector2Int(Mathf.RoundToInt(axis.x), Mathf.RoundToInt(axis.y));
        if (MoveAmount.x != 0)
        {
            MoveAmount.y = 0;
        }
        if (MoveAmount != Vector2.zero)
        {
            CheckMove(MoveAmount);
        }
    }

    public void CheckMove(Vector2Int dir)
    {
        if (IsMoving)
            return;
        if (GameController.Instance.CurrentMechanic == GameController.Mechanic.None)
            return;
        Vector2Int nextCoords = CurrentCoordinate + dir;
        if (nextCoords.x >= TerrainSpawner.Instance.Size.x || nextCoords.x < 0)
        {
            return;
        }
        if (nextCoords.y >= TerrainSpawner.Instance.Size.y || nextCoords.y < 0)
        {
            return;
        }
        NextTile = TerrainSpawner.Instance.Terrains[nextCoords.x, nextCoords.y];
        foreach (Player p in PlayerSpawner.Instance.Players)
        {
            if (NextTile == p.NextTile && p != this || p.CurrentCoordinate == NextTile.Coords)
            {
                NextTile = null;
                return;
            }
        }
        if (NextTile.MyType == TerrainTile.Type.Wall && GameController.Instance.CurrentMechanic != GameController.Mechanic.BreakRock)
        {
            return;
        }

        StartCoroutine(Move(NextTile, dir));
    }

    private IEnumerator Move(TerrainTile tile, Vector2Int dir)
    {
        GameController.Mechanic mechanic = GameController.Instance.CurrentMechanic;
        IsMoving = true;
        Vector3 startpos = transform.position;
        Vector3 endpos = tile.transform.position;
        float count = 0;
        Vector3 baseScale = transform.localScale;
        bool willModifyTerrain = WillModifyNextTerrain(tile, mechanic);

        if (willModifyTerrain)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger(GetAnimationName(mechanic));
        }
        while (count <= MoveTime)
        {
            float i = count / MoveTime;
            Vector3 jump = dir.x != 0 ? new Vector3(0, Curve.Evaluate(i) * HorizontalMoveModifier, 0) : Vector3.zero;

            transform.position = Vector3.Lerp(startpos, endpos, i) + jump;
            if (dir.y != 0)
            {
                transform.localScale = baseScale + new Vector3(1, 1, 0) * Curve.Evaluate(i) * VerticalMoveModifier;
            }
            count += Time.deltaTime;
            yield return null;
        }
        transform.position = endpos;
        transform.localScale = baseScale;
        CurrentCoordinate = new Vector2Int(Mathf.RoundToInt(endpos.x), Mathf.RoundToInt(endpos.y));

        CheckTerrainMod(tile, mechanic);
        if (willModifyTerrain)
            yield return new WaitForSeconds(WaitTimeBetweenMovements);
        IsMoving = false;
        NextTile = null;
    }

    private string GetAnimationName(GameController.Mechanic mechanic)
    {
        switch (mechanic)
        {
            case GameController.Mechanic.BreakRock:
                return "pickaxe";
            case GameController.Mechanic.Plow:
                return "hoe";
            case GameController.Mechanic.Water:
                return "sprinkler";
            case GameController.Mechanic.Plant:
                return "seed";
            default:
                return "";
        }
    }

    public void CheckTerrainMod(TerrainTile tile, GameController.Mechanic mechanic)
    {
        if (mechanic == GameController.Mechanic.BreakRock && tile.MyType == TerrainTile.Type.Wall)
        {
            tile.MyType = TerrainTile.Type.Dirt;
            modifiedTerrain?.Invoke(this, tile, tile.MyType);
        }
        else if (mechanic == GameController.Mechanic.Plow && tile.MyType == TerrainTile.Type.Dirt)
        {
            tile.MyType = TerrainTile.Type.Plowed;
            modifiedTerrain?.Invoke(this, tile, tile.MyType);
        }
        else if (mechanic == GameController.Mechanic.Water && tile.MyType == TerrainTile.Type.Plowed)
        {
            tile.MyType = TerrainTile.Type.PlowedAndWatered;
            modifiedTerrain?.Invoke(this, tile, tile.MyType);
        }
        else if (mechanic == GameController.Mechanic.Plant && tile.MyType == TerrainTile.Type.PlowedAndWatered)
        {
            tile.MyType = TerrainTile.Type.Planted;
            tile.PlayerId = this.Id;
            modifiedTerrain?.Invoke(this, tile, tile.MyType);
        }
    }

    public bool WillModifyNextTerrain(TerrainTile tile, GameController.Mechanic mechanic)
    {
        return mechanic == GameController.Mechanic.BreakRock && tile.MyType == TerrainTile.Type.Wall ||
            mechanic == GameController.Mechanic.Plow && tile.MyType == TerrainTile.Type.Dirt ||
            mechanic == GameController.Mechanic.Water && tile.MyType == TerrainTile.Type.Plowed ||
            mechanic == GameController.Mechanic.Plant && tile.MyType == TerrainTile.Type.PlowedAndWatered;
    }
}
