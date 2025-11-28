using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab de la moneda
    public float spawnDistance = 3f; // Distancia entre monedas
    public int coinsPerRow = 10; // Cuántas monedas por fila
    public float laneOffset = 3f; // Distancia entre carriles
    public float spawnInterval = 2f; // Cada cuántos segundos se generan nuevas monedas

    [Range(0.0f, 1.0f)]
    public float coinSpawnChance = 0.7f; // Probabilidad de que aparezca una moneda

    private float nextSpawnZ = 0f;

    void Start()
    {
        // Comienza a generar monedas repetidamente
        InvokeRepeating(nameof(SpawnPattern), 0f, spawnInterval);
    }

    // ----- FUNCIÓN PRINCIPAL DE GENERACIÓN -----
    void SpawnPattern()
    {
        // 1. Decide aleatoriamente en qué carriles SÍ vamos a intentar generar.
        bool spawnLeft = (Random.value > 0.5f); 
        bool spawnCenter = (Random.value > 0.5f); 
        bool spawnRight = (Random.value > 0.5f); 

        // 2. Nos aseguramos de que al menos UN carril tenga monedas.
        if (!spawnLeft && !spawnCenter && !spawnRight)
        {
            int randomLane = Random.Range(-1, 2); 
            if (randomLane == -1) spawnLeft = true;
            if (randomLane == 0) spawnCenter = true;
            if (randomLane == 1) spawnRight = true;
        }

        // 3. Llamamos a la función para generar la fila en los carriles seleccionados.
        if (spawnLeft)
        {
            SpawnGappyLane(-1); // Carril Izquierdo
        }
        if (spawnCenter)
        {
            SpawnGappyLane(0); // Carril Central
        }
        if (spawnRight)
        {
            SpawnGappyLane(1); // Carril Derecho
        }

        // Aumenta la posición Z para el siguiente patrón
        nextSpawnZ += coinsPerRow * spawnDistance;
    }


    // ----- FUNCIÓN QUE GENERA LA FILA CON HUECOS -----
    void SpawnGappyLane(int lane)
    {
        for (int i = 0; i < coinsPerRow; i++)
        {
            // Comprueba la probabilidad de 'coinSpawnChance'
            if (Random.value < coinSpawnChance)
            {
                // Si el azar lo decide, ¡genera la moneda!
                Vector3 spawnPos = transform.position + new Vector3((lane * laneOffset), 1f, nextSpawnZ + i * spawnDistance);
                Instantiate(coinPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}