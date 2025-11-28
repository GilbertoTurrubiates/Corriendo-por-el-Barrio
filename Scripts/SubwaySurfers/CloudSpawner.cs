using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject[] cloudPrefabs; // Arrastra aquí tus prefabs de nubes
    public float spawnInterval = 3f;  // Cada cuántos segundos sale una nube
    
    [Header("Posición")]
    public float minHeight = 15f;     // Altura mínima (Y)
    public float maxHeight = 25f;     // Altura máxima (Y)
    
    // 🔹 CAMBIO: Valor reducido a 10 para que salgan sobre los rieles
    public float sideRange = 10f;     
    
    [Header("Referencias")]
    public Transform player;          // Arrastra al jugador aquí

    void Start()
    {
        // Buscar al jugador si se nos olvidó ponerlo manualmente
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        // Iniciar la generación de nubes
        InvokeRepeating(nameof(SpawnCloud), 0f, spawnInterval);
    }

    void SpawnCloud()
    {
        if (cloudPrefabs.Length == 0) return;

        // 1. Elegir nube al azar
        int randomIndex = Random.Range(0, cloudPrefabs.Length);
        
        // 2. Calcular posición aleatoria en el cielo
        // X: Aleatorio izquierda/derecha (reducido por sideRange)
        float randomX = Random.Range(-sideRange, sideRange);
        // Y: Aleatorio altura
        float randomY = Random.Range(minHeight, maxHeight);
        // Z: Siempre adelante del jugador (100 metros adelante)
        float spawnZ = 0f;
        if(player != null) 
            spawnZ = player.position.z + 100f;

        Vector3 spawnPos = new Vector3(randomX, randomY, spawnZ);

        // 3. Rotación aleatoria "Acostada"
        // 🔹 CAMBIO CLAVE: Ponemos -90 en X para que la nube se acueste.
        // El segundo valor (Y) es aleatorio para que giren y no se vean todas iguales.
        Quaternion randomRot = Quaternion.Euler(-90, Random.Range(0, 360), 0);

        // 4. Crear la nube
        Instantiate(cloudPrefabs[randomIndex], spawnPos, randomRot);
    }
}