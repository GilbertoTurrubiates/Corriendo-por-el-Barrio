using UnityEngine;
using UnityEngine.SceneManagement; // 🔸 Necesario para cargar escenas

public class GameManager : MonoBehaviour
{
    public void ReiniciarNivel()
    {
        // 1. Muy importante: Descongelar el tiempo
        // Como pausamos el juego al perder (Time.timeScale = 0),
        // si no lo regresamos a 1, el juego reiniciará pero se quedará quieto.
        Time.timeScale = 1f;

        // 2. Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}