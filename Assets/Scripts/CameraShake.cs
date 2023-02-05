using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 startPos;

    private void Start() 
    {
        startPos = transform.position;
        PlayerSpawner.Instance.createdPlayer += ConnectToModifiedTerrain;
    }

    private void ConnectToModifiedTerrain(Player p) 
    {
        p.modifiedTerrain += CheckTerrainChange;
    }

    private void CheckTerrainChange(Player p, TerrainTile tile, TerrainTile.Type type) 
    {
        if (type == TerrainTile.Type.Dirt) 
        {
            StartCoroutine(Shake(0.025f, 0.1f));
        }
    }

    private IEnumerator Shake(float intensity, float lenght) 
    {
        float startIntensity = intensity;
        float timePassed = 0;

        while (timePassed < lenght) 
        {
            transform.position = startPos + (Vector3)Random.insideUnitCircle * intensity;
            intensity = (1 - timePassed / lenght) * startIntensity;
            timePassed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }
}
