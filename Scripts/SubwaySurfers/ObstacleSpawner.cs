using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Array para poner TODOS tus prefabs de obstáculos (Tren, Pluma, etc.)
    public GameObject[] obstaclePrefabs;

    // ----- Configuración de carriles -----
    // ¡Asegúrate de que este valor sea IDÉNTICO al de tu CoinSpawner!
    public float laneOffset = 3f; 

    // ----- Configuración de tiempo -----
    public float spawnInterval = 4f;     // Cada cuántos segundos aparece un obstáculo
    
    // Dónde (en Z) empieza el primer spawn (ponlo lejos, delante del jugador)
    private float nextSpawnZ = 40f;
    // Distancia mínima (en Z) entre un obstáculo y el siguiente
    public float distanceBetweenSpawns = 30f; 

    void Start()
    {
        // Llama a la función SpawnObstacle cada 'spawnInterval' segundos
        InvokeRepeating(nameof(SpawnObstacle), 0f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // 1. Elige un prefab de obstáculo AL AZAR de tu lista
        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject prefabToSpawn = obstaclePrefabs[prefabIndex];

        // 2. Elige un carril AL AZAR (-1 = Izq, 0 = Cen, 1 = Der)
        int laneIndex = Random.Range(-1, 2); // Devuelve -1, 0, ó 1
        float spawnX = laneIndex * laneOffset;

        // 3. Calcula la altura (Y).
        // Usamos 1.0f por defecto. Ajusta este número si tu tren
        // aparece flotando o enterrado.
        float spawnY = 1.0f; 

        // 4. Define la posición completa donde aparecerá
        Vector3 spawnPos = new Vector3(spawnX, spawnY, nextSpawnZ);

        // 5. Crea el obstáculo en la escena
        Instantiate(prefabToSpawn, spawnPos, prefabToSpawn.transform.rotation);

        // 6. Aumenta la posición Z para el siguiente spawn
        nextSpawnZ += distanceBetweenSpawns;
    }
}