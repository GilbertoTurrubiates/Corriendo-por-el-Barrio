using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public Transform player;              // El jugador
    public GameObject groundPrefab;       // Prefab del tramo del suelo
    public float tileLength = 10f;        // Longitud de cada tramo
    public int tilesOnScreen = 5;         // Cuántos tramos mantener activos
    private float spawnZ = 0f;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        // Generar los tramos iniciales
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // Cuando el jugador avanza, generar más tramos
        if (player.position.z - 20 > (spawnZ - tilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    void SpawnTile()
    {
        GameObject tile = Instantiate(groundPrefab, Vector3.forward * spawnZ, Quaternion.identity);
        activeTiles.Add(tile);
        spawnZ += tileLength;
    }

    void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
