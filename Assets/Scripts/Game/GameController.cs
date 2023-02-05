using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float[] mechanicTime;
    public float count;

    public System.Action<int> playerIncreasedScore;
    public System.Action<int> playerWon;

    public UILeaderBoard leaderBoard;

    public enum Mechanic
    {
        None = 0,
        BreakRock = 1,
        Plow = 2,
        Water = 3,
        Plant = 4
    }

    public Mechanic CurrentMechanic = Mechanic.None;
    public Dictionary<int, Player> DicPlayer = new Dictionary<int, Player>();
    public Dictionary<int, int> DicScore = new Dictionary<int, int>();

    private void Awake()
    {
        Instance = this;
        StartCoroutine(GameBegin());
    }

    private void Start()
    {
        leaderBoard.gameObject.SetActive(false);
        PlayerSpawner.Instance.createdPlayer += OnCreatedPlayer;
    }

    private void OnCreatedPlayer(Player p)
    {
        DicPlayer.Add(p.Id, p);
        DicScore.Add(p.Id, 0);
        p.modifiedTerrain += OnPlayerModifiedTerrain;
    }

    private void OnPlayerModifiedTerrain(Player p, TerrainTile t, TerrainTile.Type type)
    {
        DicScore[p.Id] += GetPointsByTileType(type);
        playerIncreasedScore?.Invoke(p.Id);
    }

    private int GetPointsByTileType(TerrainTile.Type type)
    {
        switch (type)
        {
            case TerrainTile.Type.Dirt:
                return 10;
            case TerrainTile.Type.Plowed:
                return 15;
            case TerrainTile.Type.PlowedAndWatered:
                return 20;
            case TerrainTile.Type.Planted:
                return 100;
            default:
                return 0;
        }
    }

    private IEnumerator GameBegin()
    {
        //yield return new WaitForSeconds(3);
        //Debug.Log("JOGO COME�OU");
        //CurrentMechanic = Mechanic.BreakRock;
        leaderBoard.gameObject.SetActive(false);
        int i = 0;
        count = mechanicTime[0];
        while (i < mechanicTime.Length - 1)
        {
            //Debug.Log(count);
            if (count > 0)
            {
                yield return new WaitForEndOfFrame();
                count -= Time.deltaTime;
            }
            else
            {
                i++;
                count = mechanicTime[i];
                ChangeMechanic();
            }
        }
        //hora das plantinhas!
        yield return TerrainSpawner.Instance.BlossomFlowers();
        int winnerId = 0;
        for (i = 1; i < PlayerSpawner.Instance.Players.Count; i++) {
            if (DicScore[winnerId] < DicScore[i])
            {
                winnerId = i;
            }
        }
        leaderBoard.added_to_lb();
        Debug.Log("winnerId: " + winnerId);
        CallWinnerAction(winnerId);
        StartCoroutine(RestartGame());
        //checa o score mais alto e emite uma action maneira
    }

    private void CallWinnerAction(int winnerId)
    {
        playerWon?.Invoke(winnerId);
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void ChangeMechanic()
    {
        if (CurrentMechanic == Mechanic.Plant)
        {
            CurrentMechanic = Mechanic.None;
            return;
        }
        CurrentMechanic = (Mechanic)(((int)CurrentMechanic) + 1);
    }

    private void OnDestroy()
    {
        if (PlayerSpawner.Instance != null)
        {
            PlayerSpawner.Instance.createdPlayer -= OnCreatedPlayer;
            foreach (Player p in PlayerSpawner.Instance.Players)
            {
                if (p == null)
                {
                    continue;
                }
                p.modifiedTerrain -= OnPlayerModifiedTerrain;
            }
        }
    }
}
