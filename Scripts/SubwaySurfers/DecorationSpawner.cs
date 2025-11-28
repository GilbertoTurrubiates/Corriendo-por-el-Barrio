using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject[] buildingPrefabs; // Array para poner varias casas distintas si quieres
    public float spawnDistance = 10f;    // Distancia entre cada casa (hacia adelante)
    public float sideOffset = 10f;       // Qué tan lejos del centro aparecen (hacia los lados)
    
    private float nextSpawnZ = 30f;      // Empezamos a generar a 30m del jugador

    [Header("Referencias")]
    public Transform player;             // Para saber dónde generar terreno nuevo

    void Start()
    {
        // Si no asignaste al jugador, lo buscamos
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
            
        // Generamos unas cuantas casas iniciales para que no se vea vacío
        for (int i = 0; i < 5; i++)
        {
            SpawnBuildings();
        }
    }

    void Update()
    {
        // Si el jugador avanza, generamos más casas adelante
        // Mantenemos siempre casas generadas 50 metros por delante
        if (player.position.z > nextSpawnZ - 50f)
        {
            SpawnBuildings();
        }
    }

    void SpawnBuildings()
    {
        // Elegir una casa al azar de tu lista
        int randomIndex = Random.Range(0, buildingPrefabs.Length);
        GameObject prefabToSpawn = buildingPrefabs[randomIndex];

        // --- LADO IZQUIERDO ---
        Vector3 leftPos = new Vector3(-sideOffset, 0, nextSpawnZ);
        Instantiate(prefabToSpawn, leftPos, Quaternion.Euler(0, 90, 0)); // Rotamos 90 grados si es necesario

        // --- LADO DERECHO ---
        Vector3 rightPos = new Vector3(sideOffset, 0, nextSpawnZ);
        Instantiate(prefabToSpawn, rightPos, Quaternion.Euler(0, -90, 0)); // Rotamos -90 grados para que mire al centro

        // Avanzamos el punto de generación
        nextSpawnZ += spawnDistance;
    }
}