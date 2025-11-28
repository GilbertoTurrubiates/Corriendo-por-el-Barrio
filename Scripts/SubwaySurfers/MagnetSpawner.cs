using UnityEngine;

public class MagnetSpawner : MonoBehaviour
{
    [Header("Configuración del Imán")]
    public GameObject magnetPrefab; // Arrastra aquí tu Prefab del Imán
    public float spawnInterval = 10f; // Cada cuánto tiempo intentamos generar uno
    public float laneOffset = 3f; // Distancia entre carriles
    
    [Range(0.0f, 1.0f)]
    public float magnetSpawnChance = 0.2f; // Probabilidad de que aparezca (20%)

    private float nextSpawnZ; // Usaremos esta variable para saber dónde ponerlo.
    
    // La fila de monedas tiene 10 posiciones * 3m = 30m de largo
    private const float COIN_ROW_LENGTH = 30f; 

    void Start()
    {
        // Empezamos a generar a 40 metros del inicio para darle tiempo al jugador
        nextSpawnZ = 40f; 
        
        // Comienza a generar imanes cada 10 segundos
        InvokeRepeating(nameof(SpawnMagnet), 5f, spawnInterval); 
    }

    void SpawnMagnet()
    {
        // 1. Tiramos el dado: ¿Toca generar un imán ahora?
        if (magnetPrefab != null && Random.value < magnetSpawnChance)
        {
            // 2. Elige un carril al azar (-1, 0, ó 1)
            int randomLane = Random.Range(-1, 2); 
            
            // 3. Calculamos la posición: a una altura de 1m y en la nueva posición Z
            Vector3 spawnPos = new Vector3(randomLane * laneOffset, 1f, nextSpawnZ);
            
            // 4. ¡Genera el Imán!
            Instantiate(magnetPrefab, spawnPos, Quaternion.identity);

            Debug.Log($"MagnetSpawner: Imán generado en carril {randomLane}");
        }

        // Mover el punto de generación Z al próximo espacio posible
        // Sumamos la longitud de la fila de monedas (30m) más un poco de buffer (10m)
        nextSpawnZ += COIN_ROW_LENGTH + 10f; 
    }
}