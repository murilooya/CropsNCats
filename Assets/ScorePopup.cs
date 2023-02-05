using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    private Animator anim;

    private void Start() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update() 
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) 
        {
            Destroy(gameObject);
        }
    }
}
