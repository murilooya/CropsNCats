using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float time;

    private void Start()
    {
        if (GetComponent<AudioClip>() != null)
        {
            time = GetComponent<AudioSource>().clip.length;
        }
        else
        {
            time = GetComponent<ParticleSystem>().startLifetime;
        }
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(gameObject);
        }
    }
}
