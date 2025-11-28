using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 8f;
    public float laneDistance = 3f;
    public float jumpForce = 8f;

    [Header("Deslizamiento (Agacharse)")]
    public float slideDuration = 1.0f; 
    public float slideHeight = 0.5f;   
    private bool isSliding = false;
    
    // Variables para recordar el tamaño original
    private float originalHeight;
    private Vector3 originalCenter;

    [Header("UI Game Over")] 
    public GameObject gameOverPanel; 

    [Header("Componentes")]
    private CharacterController controller;
    private Vector3 moveDirection;
    
    // Variable para controlar las animaciones (Arrastra el hijo aquí)
    public Animator playerAnimator; 

    [Header("Carriles")]
    private int lane = 1; // 0 = izquierda, 1 = centro, 2 = derecha
    private float startX;

    [Header("Monedas")]
    private int coins = 0;

    // 🔹 NUEVO: Variables para el Power-Up IMÁN
    [Header("Power Ups")]
    public bool isMagnetActive = false; // ¿Está activo el imán?
    public float magnetDuration = 5f;   // Duración del poder

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startX = transform.position.x;

        originalHeight = controller.height;
        originalCenter = controller.center;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        // 1. Movimiento hacia adelante constante
        moveDirection.z = speed;

        // 2. Cambio de carriles
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lane > 0)
            lane--;
        if (Input.GetKeyDown(KeyCode.RightArrow) && lane < 2)
            lane++;

        float targetX = (lane - 1) * laneDistance + startX;
        float deltaX = targetX - transform.position.x;
        moveDirection.x = deltaX * 10f;

        // 3. Lógica de Salto y Gravedad
        if (controller.isGrounded)
        {
            // SALTAR
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isSliding)
            {
                moveDirection.y = jumpForce;
                if(playerAnimator != null) playerAnimator.SetTrigger("Saltar");
            }
            
            // DESLIZARSE
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
            {
                StartCoroutine(Slide());
            }
        }
        else
        {
            // Caída rápida
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding)
            {
                moveDirection.y = -jumpForce;
                StartCoroutine(Slide());
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);

        // 🔹 CORRECCIÓN DE ROTACIÓN: Obligar al modelo a mirar al frente
        if (playerAnimator != null)
        {
            playerAnimator.transform.localRotation = Quaternion.identity;
        }
    }

    // --- CORRUTINA PARA AGACHARSE ---
    IEnumerator Slide()
    {
        isSliding = true;
        if(playerAnimator != null) playerAnimator.SetTrigger("Deslizar");

        controller.height = slideHeight;
        controller.center = new Vector3(0, slideHeight / 2, 0);

        yield return new WaitForSeconds(slideDuration);

        controller.height = originalHeight;
        controller.center = originalCenter;
        isSliding = false;
    }

    // 🔹 NUEVO: Corrutina para activar y desactivar el Imán
    IEnumerator ActivateMagnet()
    {
        isMagnetActive = true;
        Debug.Log("¡IMÁN ACTIVADO! 🧲");
        
        yield return new WaitForSeconds(magnetDuration);
        
        isMagnetActive = false;
        Debug.Log("Imán terminado...");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            Debug.Log("Monedas: " + coins);
            Destroy(other.gameObject);
        }
        // 🔹 NUEVO: Detectar el objeto Imán
        else if (other.CompareTag("Magnet"))
        {
            StartCoroutine(ActivateMagnet());
            Destroy(other.gameObject); // Borrar el imán de la escena
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("¡JUEGO TERMINADO!");
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            this.enabled = false; 
        }
    }

    public int GetCoins()
    {
        return coins;
    }
}