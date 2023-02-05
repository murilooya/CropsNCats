using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class UIBoard : MonoBehaviour {
    public Dictionary<int, int> dictScore = new Dictionary<int, int>();
    public List<UILBBlock> listBlocks = new List<UILBBlock>();

    public PlayerSpawner playerSpawner;

    private void Start()
    {
        
    }

    public void add_score(int _id, int _score)
    {
        Debug.Log(_id + " " + _score);
        dictScore.Add(_id, _score);
    }

    public Dictionary<int, int> sort_dictionary()
    {
        var top = dictScore.OrderByDescending(pair => pair.Value).Take(listBlocks.Count)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        return top;
    }

    public void set_scoreboard()
    {
        if (playerSpawner == null)
            playerSpawner = PlayerSpawner.Instance;
        var top = sort_dictionary();
        for (int i = 0; i < listBlocks.Count; i++)
        {
            if (top.Count == i)
            {
                break;
            }
            listBlocks[i].set_score(top.Values.ElementAt(i));
            listBlocks[i].set_sprite(playerSpawner.get_player_by_id(top.Keys.ElementAt(i)).MyColor);
            listBlocks[i].gameObject.SetActive(true);
        }
    }
}