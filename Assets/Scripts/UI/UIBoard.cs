using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBoard : MonoBehaviour {

    public UILBBlock block;
    public List<UILBBlock> list = new List<UILBBlock>();

    public void add_board(string _name, int _score) {
        UILBBlock iBlock = Instantiate(block);
        iBlock.set_name(_name);
        iBlock.set_score(_score);
        list.Add(iBlock);
    }

    public void sort_board()
    {
        GFG g = new GFG();
        list.Sort(g);
    }
}

public class GFG : IComparer<UILBBlock>
{
    public int Compare(UILBBlock x, UILBBlock y)
    {
        if (x == null || y == null)
        {
            return 0;
        }
        return x.player_score.CompareTo(y.player_score); 
    }
}