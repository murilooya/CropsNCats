using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudController : MonoBehaviour
{
    public int currentMechanicValue = 0;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI[] scoreText;


    private void Update()
    {
        if (Mathf.RoundToInt(GameController.Instance.count) <= 3 && Mathf.RoundToInt(GameController.Instance.count) > -1)
        {
            countdownText.text = Mathf.RoundToInt(GameController.Instance.count).ToString();
        }
        else
        {
            countdownText.text = "";
        }
    }
}
