using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;
    public Player PlayerPrefab;

    public List<Player> Players = new List<Player>();

    public Color[] Colors;
    public Action<Player> createdPlayer;

    private void Awake()
    {
        Instance = this;
    }

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
            createdPlayer?.Invoke(p);
        }
    }

    public Player get_player_by_id(int id)
    {
        foreach (Player player in Players)
        {
            if (player.Id == id)
            {
                return player;
            }
        }
        return null;
    }
}
