using System.Collections.Generic;
using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public GameObject groundPrefab;     // Tu prefab de rieles
    public Transform playerTransform;   // El Transform del Jugador
    
    // ----- ¡PON AQUÍ LA MEDIDA DEL PASO 1! -----
    public float prefabLength = 50f;    // La longitud (en Z) de tu prefab de suelo

    public int initialSections = 5;     // Cuántas secciones crear al inicio
    public float spawnAheadDistance = 100f; // Qué tan adelante del jugador debe estar el "punto de spawn"

    private float nextSpawnZ;           // Dónde se generará la siguiente pieza
    private List<GameObject> activeSections = new List<GameObject>();

    void Start()
    {
        // ---------------------------------------------------------
        // 🛡️ BLOQUE DE SEGURIDAD (AUTO-REPARACIÓN)
        // ---------------------------------------------------------
        // Si la casilla del jugador está vacía, lo buscamos automáticamente.
        if (playerTransform == null)
        {
            // Buscamos en la escena cualquier objeto que tenga el script "PlayerController"
            PlayerController playerFound = FindObjectOfType<PlayerController>();
            
            if (playerFound != null)
            {
                playerTransform = playerFound.transform;
                // Debug.Log("¡TrackSpawner encontró al jugador automáticamente!");
            }
            else
            {
                Debug.LogError("ERROR CRÍTICO: No se encuentra al Player en la escena. Asegúrate de que tu jugador tenga el script PlayerController.");
            }
        }
        // ---------------------------------------------------------


        // Establece la 'Z' inicial en 0
        nextSpawnZ = 0f;

        // Genera las primeras secciones del juego
        for (int i = 0; i < initialSections; i++)
        {
            SpawnSection();
        }
    }

    void Update()
    {
        // PRECAUCIÓN: Si por alguna razón el playerTransform sigue siendo null (ej. el jugador murió y se destruyó),
        // evitamos que el juego crashee saliendo de la función aquí mismo.
        if (playerTransform == null) return;

        // Esto comprueba si el jugador se está acercando al "final" del
        // camino que ya hemos generado.
        if (playerTransform.position.z + spawnAheadDistance > nextSpawnZ)
        {
            SpawnSection();
        }
    }

    void SpawnSection()
    {
        // 1. Crea la nueva sección de suelo en la posición 'nextSpawnZ'
        Vector3 spawnPos = new Vector3(0, 0, nextSpawnZ);
        GameObject newSection = Instantiate(groundPrefab, spawnPos, groundPrefab.transform.rotation);
        
        // 2. La hace hija de este objeto (para mantener la jerarquía limpia)
        newSection.transform.SetParent(transform);
        
        // 3. Actualiza la posición Z para la *siguiente* pieza
        nextSpawnZ += prefabLength;
    }
}