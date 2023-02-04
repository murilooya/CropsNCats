using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    public Player PlayerPrefab;

    public List<Player> Players = new List<Player>();

    public Color[] Colors;

    private IEnumerator Start()
    {
        yield return null;
        Debug.Log(Joystick.all.Count);
        foreach (Joystick j in Joystick.all)
        {
            Player p = Player.Instantiate(PlayerPrefab);
            Players.Add(p);
            p.Id = Players.Count - 1;
            p.CurrentCoordinate = TerrainSpawner.Instance.GetInitialCoordsForPlayerId(p.Id);
            p.transform.position = new Vector3(p.CurrentCoordinate.x, p.CurrentCoordinate.y, 0);
            p.MyJoystick = j;
            p.MyColor = Colors[p.Id];
        }
    }
}
