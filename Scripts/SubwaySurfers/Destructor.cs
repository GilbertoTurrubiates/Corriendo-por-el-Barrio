using UnityEngine;

public class Destructor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // CASO 1: Es una moneda o un obstáculo (objetos simples)
        if (other.CompareTag("Coin") || other.CompareTag("Obstacle"))
        {
            // Los destruimos y terminamos.
            Destroy(other.gameObject);
            return;
        }

        // CASO 2: Es una parte del suelo (un hijo como rail_center)
        // Comprobamos si tiene un "padre" y si ESE PADRE tiene la etiqueta.
        if (other.transform.parent != null && other.transform.parent.CompareTag("GroundSection"))
        {
            // ¡Sí! Es un hijo del suelo.
            // Destruimos al PADRE (other.transform.parent.gameObject)
            // para que se elimine la pieza de suelo ENTERA.
            Destroy(other.transform.parent.gameObject);
            return;
        }

        // CASO 3: Es el objeto padre (en caso de que el collider esté en el padre)
        if (other.CompareTag("GroundSection"))
        {
            // Destruimos el objeto mismo.
            Destroy(other.gameObject);
        }
    }
}