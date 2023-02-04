using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputAction moveAction;
    public float Speed = 2;
    public Vector2Int MoveAmount;

    public Vector2Int CurrentCoordinate = Vector2Int.zero;
    public bool IsMoving = false;
    public float MoveTime = 0.5f;

    public void Update()
    {
        Vector2 axis = moveAction.ReadValue<Vector2>();
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

        StartCoroutine(Move(nextTile));
        //verificar se o tile que eu quero ir é possível de ir.
        //pra isso, duas coisas:
        //1 - saber se EXISTE um tile, porque eu posso estar no limite da tela
        //2 - ver se o lugar que eu quero ir é pedra, se for, só posso se tiver na fase de quebrar pedra
    }

    private IEnumerator Move(TerrainTile tile)
    {
        IsMoving = true;
        Vector3 startpos = transform.position;
        Vector3 endpos = tile.transform.position;
        float count = 0;
        while (count <= MoveTime)
        {
            float i = count / MoveTime;
            transform.position = Vector3.Lerp(startpos, endpos, i);
            count += Time.deltaTime;
            yield return null;
        }
        transform.position = endpos;
        CurrentCoordinate = new Vector2Int(Mathf.RoundToInt(endpos.x), Mathf.RoundToInt(endpos.y));
        IsMoving = false;
        CheckTerrainMod(tile);
    }

    public void CheckTerrainMod(TerrainTile tile)
    {
        GameController game = GameController.Instance;
        if (game.CurrentMechanic == GameController.Mechanic.BreakRock && tile.MyType == TerrainTile.Type.Wall)
            tile.MyType = TerrainTile.Type.Dirt;
        else if (game.CurrentMechanic == GameController.Mechanic.Plow && tile.MyType == TerrainTile.Type.Dirt)
            tile.MyType = TerrainTile.Type.Plowed;
        else if (game.CurrentMechanic == GameController.Mechanic.Water && tile.MyType == TerrainTile.Type.Plowed)
            tile.MyType = TerrainTile.Type.PlowedAndWatered;
        else if (game.CurrentMechanic == GameController.Mechanic.Plant && tile.MyType == TerrainTile.Type.PlowedAndWatered)
            tile.MyType = TerrainTile.Type.Planted;
    }

    public void OnEnable()
    {
        Debug.Log("enabled");
        moveAction.Enable();
    }

    public void OnDisable()
    {
        Debug.Log("disabled");
        moveAction.Disable();
    }
}
