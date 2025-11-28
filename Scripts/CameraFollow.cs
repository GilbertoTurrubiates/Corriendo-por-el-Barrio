using UnityEngine;

public class CameraFollowZ : MonoBehaviour

{
    public Transform jugador;
    public float offsetZ = -10f;
    public float smoothSpeed = 5f;

    private float yInicial;
    private float xInicial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yInicial = transform.position.y;
        xInicial = transform.position.x;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (jugador == null) return;

        //La camara solo seguira en Z
        float zDeseado = jugador.position.z + offsetZ;

        //Suavizado
        float zSuavizado = Mathf.Lerp(transform.position.z, zDeseado, smoothSpeed * Time.deltaTime);
        
        //Aplicamos nueva posicion
        transform.position = new Vector3(xInicial, yInicial, zSuavizado);
        
    }
}
