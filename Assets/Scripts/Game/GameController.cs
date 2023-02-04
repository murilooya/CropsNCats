using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public float[] mechanicTime;
    public float count;

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
        //yield return new WaitForSeconds(3);
        //Debug.Log("JOGO COMEÇOU");
        //CurrentMechanic = Mechanic.BreakRock;
        int i = 0;
        count = mechanicTime[0];
        while (i < mechanicTime.Length - 1)
        {
            Debug.Log(count);
            if (count > 0)
            {
                yield return new WaitForEndOfFrame();
                count -= Time.deltaTime;
            }
            else
            {
                i++;
                count = mechanicTime[i];
                ChangeMechanic();
            }
        }
    }

    private void ChangeMechanic()
    {
        if (CurrentMechanic == Mechanic.Plant)
        {
            CurrentMechanic = Mechanic.None;
            return;
        }
        CurrentMechanic = (Mechanic)(((int)CurrentMechanic) + 1);
    }
}
