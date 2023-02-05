using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public GameObject particle;

    public Sprite rock;


    void Start()
    {
        PlayerSpawner.Instance.createdPlayer += ConnectToModifiedTerrain;
    }

    private void ConnectToModifiedTerrain(Player p)
    {
        p.modifiedTerrain += TerrainParticles;
    }

    private void TerrainParticles(Player p, TerrainTile tile, TerrainTile.Type type)
    {
        var s = Instantiate(particle, p.transform.position, Quaternion.identity);
        switch (type)
        {
            case TerrainTile.Type.Dirt:
                s.GetComponent<ParticleSystem>().textureSheetAnimation.AddSprite(rock);
                break;
            case TerrainTile.Type.Plowed:
                break;
            case TerrainTile.Type.PlowedAndWatered:
                break;
            case TerrainTile.Type.Planted:
                break;
        }
        s.GetComponent<ParticleSystem>().Play();
        s.GetComponent<AutoDestroy>().enabled = true;
    }

    private void OnDisable()
    {
        PlayerSpawner.Instance.createdPlayer -= ConnectToModifiedTerrain;
    }
}
