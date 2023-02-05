using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HudController : MonoBehaviour
{
    public string[] startPhrases;

    public int currentMechanicValue;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI[] scoreText;

    public UILap lap;

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
            if (currentMechanicValue == 0)
            {
                Debug.Log(number);
                lap.show_lap(number);
            }
        }

        else
        {
            if (countdownText.text != "" && currentMechanicValue < 4)
            {
                currentMechanicValue++;
            }
            countdownText.text = "";
            if (lap.GetComponent<Image>().sprite != null)
                StartCoroutine(GoRoutine());
        }
    }

    private IEnumerator GoRoutine()
    {
        //todo: botar o sprite de go
        lap.show_lap(0);
        yield return new WaitForSeconds(1);
        lap.GetComponent<Image>().sprite = null;
        lap.gameObject.SetActive(false);
    }
}
