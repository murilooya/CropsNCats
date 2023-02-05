using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILBBlock : MonoBehaviour
{
    public TextMeshProUGUI text_score;
    public Image image;
    public int player_score = 0;
    public int player_position;
    public void set_score (int _score)
    {
        player_score = _score;
        text_score.SetText(player_score.ToString());
    }

    public void set_sprite(Color _sprite)
    {
        image.color = _sprite;
    }
}
