using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class ReiniciarEscena : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
