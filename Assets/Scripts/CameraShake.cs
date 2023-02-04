using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Start() 
    {
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
        Vector3 startPos = transform.position;
        float startIntensity = intensity;
        float timePassed = 0;

        while (timePassed < lenght) 
        {
            transform.position = startPos + (Vector3)Random.insideUnitCircle * intensity;
            intensity = (1 - timePassed / lenght) * startIntensity;
            Debug.Log((1 - timePassed / lenght) * startIntensity);
            timePassed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;
    }
}
