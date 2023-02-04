using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIVictoryBanner : MonoBehaviour
{
    private void Start()
    {
        GameController.Instance.playerWon += OnPlayerIdWin;
    }

    private void OnPlayerIdWin(int id)
    {
        GameController.Instance.playerWon -= OnPlayerIdWin;
        string hex = ColorUtility.ToHtmlStringRGB(PlayerSpawner.Instance.Colors[id]);
        string score = GameController.Instance.DicScore[id].ToString();
        string prefix = "<color=#" + hex + ">P" + (id + 1).ToString() + " WINS!</color>";
        this.GetComponent<TextMeshProUGUI>().text = prefix;
    }
}
