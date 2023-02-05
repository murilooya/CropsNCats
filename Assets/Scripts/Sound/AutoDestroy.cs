using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    private float time;

    public enum type
    {
        none = 0,
        audioSource,
        particleSystem
    }

    public type currentType;

    private void Start()
    {
        if (currentType == type.audioSource)
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
