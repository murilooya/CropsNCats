using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class UIBoard : MonoBehaviour {
    public Dictionary<string, int> dictScore = new Dictionary<string, int>();
    public List<UILBBlock> listBlocks = new List<UILBBlock>();

    // private void Start() {
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         listBlocks.Add(transform.GetChild(i).GetComponent<UILBBlock>()); //Adiciona os blocks na lista
    //     }
    // }
    public void add_score(string _name, int _score)
    {
        Debug.Log(_name + " " + _score);
        dictScore.Add(_name, _score);
    }

    public Dictionary<string, int> sort_dictionary()
    {
        var top = dictScore.OrderByDescending(pair => pair.Value).Take(listBlocks.Count)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        return top;
    }

    public void set_scoreboard()
    {
        var top = sort_dictionary();
        for (int i = 0; i < listBlocks.Count; i++)
        {
            if (top.Count == i)
            {
                break;
            }
            listBlocks[i].set_name(top.Keys.ElementAt(i));
            listBlocks[i].set_score(top.Values.ElementAt(i));
        }
    }
}
    // public void add_board(string _name, int _score) 
    // {
    //     List<UILBBlock> empty_block = get_empty_blocks();
    //     if (empty_block.Count < 0)
    //     {
    //         empty_block[0].set_name(_name);
    //         empty_block[0].set_score(_score);
    //     }
    //     else
    //     {
    //         List<UILBBlock> block_list = get_scores_block();
    //     }
    // }

    // public void sort_board()
    // {
    //     List<UILBBlock> block_list = get_scores_block();
    //     block_list.Sort(comp);
    // }

    // public List<UILBBlock> get_scores_block()
    // {
    //     List<UILBBlock> l = new List<UILBBlock>();
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         if (transform.GetChild(i).GetComponent<UILBBlock>().text_name.text.Equals(""))
    //         {
    //             continue;
    //         }
    //         l.Add(transform.GetChild(i).GetComponent<UILBBlock>());
    //     }
    //     return l;
    // }

    // public List<UILBBlock> get_empty_blocks()
    // {
    //     List<UILBBlock> l = new List<UILBBlock>();
        
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         if (!transform.GetChild(i).GetComponent<UILBBlock>().text_name.text.Equals(""))
    //         {
    //             continue;
    //         }
    //         l.Add(transform.GetChild(i).GetComponent<UILBBlock>());
    //     }
    //     return l;
    // }
// }

// public class Comp : IComparer<UILBBlock>
// {
//     public int Compare(UILBBlock x, UILBBlock y)
//     {
//         if (x == null || y == null)
//         {
//             return 0;
//         }
//         return x.player_score.CompareTo(y.player_score); 
//     }
// }