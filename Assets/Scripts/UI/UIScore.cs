using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    public int Id = -1;
    public TextMeshProUGUI Text;


    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        PlayerSpawner.Instance.createdPlayer += OnPlayerCreated;
        GameController.Instance.playerIncreasedScore += OnPlayerIncreasedScore;
    }

    private void OnPlayerIncreasedScore(int id)
    {
        if (this.Id != id)
        {
            return;
        }
        SetScore();
    }

    private void SetScore()
    {
        string hex = ColorUtility.ToHtmlStringRGB(PlayerSpawner.Instance.Colors[Id]);
        string score = GameController.Instance.DicScore[Id].ToString();
        string prefix = "<color=#" + hex + ">P" + (Id + 1).ToString() + ": " + score + "</color>";
        Text.text = prefix;
    }

    private void OnPlayerCreated(Player p)
    {
        if (p.Id == Id)
        {
            StartCoroutine(DoInitialStuff(p));
        }
    }

    private IEnumerator DoInitialStuff(Player p)
    {
        PlayerSpawner.Instance.createdPlayer -= OnPlayerCreated;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        yield return null;
        SetScore();
    }

    private void OnDestroy()
    {
        if (GameController.Instance != null)
        {
            GameController.Instance.playerIncreasedScore = OnPlayerIncreasedScore;
        }
    }
}
