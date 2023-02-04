using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }
    public float Speed = 2;
    public Vector2Int MoveAmount;

    public Vector2Int CurrentCoordinate = Vector2Int.zero;
    public bool IsMoving = false;
    public float MoveTime = 0.5f;
    public AnimationCurve Curve;
    public float HorizontalMoveModifier = 0.5f;
    public float VerticalMoveModifier = 0.2f;

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
        TerrainTile nextTile = TerrainSpawner.Instance.Terrains[nextCoords.x, nextCoords.y];
        if (nextTile.MyType == TerrainTile.Type.Wall && GameController.Instance.CurrentMechanic != GameController.Mechanic.BreakRock)
        {
            return;
        }

        StartCoroutine(Move(nextTile, dir));
    }

    private IEnumerator Move(TerrainTile tile, Vector2Int dir)
    {
        GameController.Mechanic mechanic = GameController.Instance.CurrentMechanic;
        IsMoving = true;
        Vector3 startpos = transform.position;
        Vector3 endpos = tile.transform.position;
        float count = 0;
        Vector3 baseScale = transform.localScale;
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
        IsMoving = false;
        CheckTerrainMod(tile, mechanic);
    }

    public void CheckTerrainMod(TerrainTile tile, GameController.Mechanic mechanic)
    {
        if (mechanic == GameController.Mechanic.BreakRock && tile.MyType == TerrainTile.Type.Wall)
            tile.MyType = TerrainTile.Type.Dirt;
        else if (mechanic == GameController.Mechanic.Plow && tile.MyType == TerrainTile.Type.Dirt)
            tile.MyType = TerrainTile.Type.Plowed;
        else if (mechanic == GameController.Mechanic.Water && tile.MyType == TerrainTile.Type.Plowed)
            tile.MyType = TerrainTile.Type.PlowedAndWatered;
        else if (mechanic == GameController.Mechanic.Plant && tile.MyType == TerrainTile.Type.PlowedAndWatered)
            tile.MyType = TerrainTile.Type.Planted;
    }
}
