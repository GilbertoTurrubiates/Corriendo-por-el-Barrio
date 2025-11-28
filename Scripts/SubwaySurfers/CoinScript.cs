using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Variables de velocidad y rango para que las monedas vuelen
    public float magnetSpeed = 25f; // Velocidad de vuelo de la moneda
    public float magnetRange = 15f; // Distancia a la que detecta al jugador
    
    // Referencias a los componentes del jugador
    private Transform playerTransform;
    private PlayerController playerScript;

    void Start()
    {
        // 1. Buscamos al objeto principal del jugador por su etiqueta "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            // 2. Guardamos la posición y el script del jugador
            playerTransform = player.transform;
            playerScript = player.GetComponent<PlayerController>();
        }
        
        // 🔹 NOTA: Si el script del jugador no se encuentra (es null), la lógica de Update() 
        // simplemente se detiene, previniendo un error.
    }

    void Update()
    {
        // Si no encontramos al jugador o su script, salimos de la función.
        if (playerTransform == null || playerScript == null) return;

        // Lógica del Imán: Si el jugador tiene el poder activo...
        if (playerScript.isMagnetActive)
        {
            // Calculamos la distancia
            float distance = Vector3.Distance(transform.position, playerTransform.position);

            // Si la moneda está dentro del rango del imán...
            if (distance < magnetRange)
            {
                // ¡Mover la moneda hacia la posición del jugador!
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    playerTransform.position, 
                    magnetSpeed * Time.deltaTime
                );
            }
            
            // Cuando la moneda toque al jugador, el OnTriggerEnter del PlayerController 
            // se encargará de destruirla y sumarla al contador.
        }
    }
}