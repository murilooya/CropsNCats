using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILBBlock : MonoBehaviour
{
    public TextMeshProUGUI text_name;
    public TextMeshProUGUI text_score;
    
    public string player_name;
    public int player_score;

    public void set_name (string _name)
    {
        player_name = '#' + _name;
        text_name.text = player_name;
    }

    public void set_score (int _score)
    {
        player_score = _score;
        text_score.text = player_score.ToString();
    }
}
