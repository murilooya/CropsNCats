using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILap : MonoBehaviour
{
    public List<Sprite> sprites;

    public void show_lap(int number)
    {
        GetComponent<Image>().sprite = sprites[number];
    }
}
