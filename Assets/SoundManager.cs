using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject sound;

    public AudioClip rockSound, harvestSound, hoeSound, plantSound, wateringSound;
    public float pitchChange;

    void Start()
    {
        TerrainSpawner.Instance.flowerBlossom += BlossomSound;
        PlayerSpawner.Instance.createdPlayer += ConnectToModifiedTerrain;
    }

    private void ConnectToModifiedTerrain(Player p)
    {
        p.modifiedTerrain += TerrainSounds;
    }

    private void TerrainSounds(Player p, TerrainTile tile, TerrainTile.Type type)
    {
        var s = Instantiate(sound, p.transform.position, Quaternion.identity);
        switch (type)
        {
            case TerrainTile.Type.Dirt:
                s.GetComponent<AudioSource>().clip = rockSound;
                break;
            case TerrainTile.Type.Plowed:
                s.GetComponent<AudioSource>().clip = hoeSound;
                break;
            case TerrainTile.Type.PlowedAndWatered:
                s.GetComponent<AudioSource>().clip = wateringSound;
                break;
            case TerrainTile.Type.Planted:
                s.GetComponent<AudioSource>().clip = plantSound;
                break;
        }
        s.GetComponent<AudioSource>().Play();
        s.GetComponent<AutoDestroy>().enabled = true;
        s.GetComponent<AudioSource>().pitch += Random.Range(-pitchChange, pitchChange);
    }

    private void BlossomSound(TerrainTile tile) 
    {
        var s = Instantiate(sound, tile.transform.position, Quaternion.identity);
        s.GetComponent<AudioSource>().clip = harvestSound;
        s.GetComponent<AudioSource>().Play();
        s.GetComponent<AutoDestroy>().enabled = true;
        s.GetComponent<AudioSource>().pitch += Random.Range(-pitchChange, pitchChange);
    }

    private void OnDisable() {
        PlayerSpawner.Instance.createdPlayer -= ConnectToModifiedTerrain;
    }
}
