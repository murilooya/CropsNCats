using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UILeaderBoard : MonoBehaviour
{
    public UIBoard board;
    public GameController gameController;

    public void added_to_lb()
    {
        foreach (KeyValuePair<int, int> player in gameController.DicScore)
        {
            board.add_score(player.Key, player.Value);
        }
        board.set_scoreboard();
        gameObject.SetActive(true);
    }

}
