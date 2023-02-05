using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public string[] startPhrases;

    public int currentMechanicValue;

    public TextMeshProUGUI phrase, inText;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI[] scoreText;


    private void Update()
    {
        if (Mathf.RoundToInt(GameController.Instance.count) <= 3 && Mathf.RoundToInt(GameController.Instance.count) > -1)
        {
            phrase.text = startPhrases[currentMechanicValue];
            inText.text = "in";
            countdownText.text = Mathf.RoundToInt(GameController.Instance.count).ToString();
        }
        else
        {
            if (phrase.text != "" && currentMechanicValue < 4)
            {
                currentMechanicValue++;
            }
            phrase.text = "";
            inText.text = "";
            countdownText.text = "";
        }
    }
}
