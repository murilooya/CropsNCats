using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public string[] startPhrases;

    public int currentMechanicValue;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI[] scoreText;

    private void Update()
    {
        if (Mathf.CeilToInt(GameController.Instance.count) <= 3 && Mathf.CeilToInt(GameController.Instance.count) > 0)
        {
            int number = Mathf.CeilToInt(GameController.Instance.count);
            string text = startPhrases[currentMechanicValue] + " " + number.ToString() + "...";
            int invisibleCharacters = Mathf.CeilToInt(Mathf.Abs(number - GameController.Instance.count) / 0.25f) - 1;
            invisibleCharacters = Mathf.Clamp(invisibleCharacters, 0, 3);
            invisibleCharacters = 3 - invisibleCharacters;
            countdownText.text = text;
            countdownText.maxVisibleCharacters = countdownText.text.Length - invisibleCharacters;
        }
        else
        {
            if (countdownText.text != "" && currentMechanicValue < 4)
            {
                currentMechanicValue++;
            }
            countdownText.text = "";
        }
    }
}
