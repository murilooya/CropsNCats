using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public enum Mechanic
    {
        None = 0,
        BreakRock = 1,
        Plow = 2,
        Water = 3,
        Plant = 4
    }

    public Mechanic CurrentMechanic = Mechanic.None;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(GameBegin());
    }
    
    private IEnumerator GameBegin()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("JOGO COMEÇOU");
        CurrentMechanic = Mechanic.BreakRock;
    }
}
