using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EffectsManager : MonoBehaviour
{
    public GameObject rockParticle, plowedParticle, wateredParticle, plaintParticle;

    public GameObject scorePopup;
    public GameObject canvas;

    void Start()
    {
        PlayerSpawner.Instance.createdPlayer += ConnectToModifiedTerrain;
        GameController.Instance.playerIncreasedScore += ShowScore;
    }

    private void ShowScore(int playerId, int scoreIncrease) 
    {
        GameObject score = Instantiate(scorePopup, PlayerSpawner.Instance.Players[playerId].transform.position, Quaternion.identity);
        score.transform.SetParent(canvas.transform);
        score.GetComponentInChildren<TextMeshProUGUI>().text = "+" + scoreIncrease;
    }

    private void ConnectToModifiedTerrain(Player p)
    {
        p.modifiedTerrain += TerrainParticles;
    }

    private void TerrainParticles(Player p, TerrainTile tile, TerrainTile.Type type)
    {
        GameObject s = null;
        switch (type)
        {
            case TerrainTile.Type.Dirt:
                s = Instantiate(rockParticle, p.transform.position, Quaternion.identity);
                break;
            case TerrainTile.Type.Plowed:
                s = Instantiate(plowedParticle, p.transform.position, Quaternion.identity);
                break;
            case TerrainTile.Type.PlowedAndWatered:
                s = Instantiate(wateredParticle, p.transform.position, Quaternion.identity);
                break;
            case TerrainTile.Type.Planted:
                s = Instantiate(plaintParticle, p.transform.position, Quaternion.identity);
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