using UnityEngine;
using UnityEngine.SceneManagement; //Necesario para manejar escenas

public class CambioEscena : MonoBehaviour
{
    public string nombreEscena; //Ecribe el nombre exacto de la escena en el Inspector

    public void CargarEscena(){
        SceneManager.LoadScene(nombreEscena);
    } 
}
