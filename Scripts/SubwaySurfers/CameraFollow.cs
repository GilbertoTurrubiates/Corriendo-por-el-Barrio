using UnityEngine;

public class CameraFollowPro : MonoBehaviour
{
    public Transform target;          // Arrastra aquí a tu JUGADOR
    
    [Header("Configuración")]
    public float followSpeed = 5f;    // Velocidad general
    public float laneSpeed = 10f;     // Velocidad lateral (¡Más rápida!)
    public Vector3 offset;            // Distancia (calculada autom. al inicio)

    void Start()
    {
        // Calculamos la distancia inicial automáticamente para que no batalles
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. Calculamos dónde debería estar la cámara en X, Y y Z
        Vector3 targetPos = target.position + offset;

        // 2. Posición actual de la cámara
        Vector3 currentPos = transform.position;

        // 3. SEPARAMOS LOS EJES:
        
        // Eje X (Lados): Usamos 'laneSpeed' para que reaccione rápido al cambio de carril
        float x = Mathf.Lerp(currentPos.x, targetPos.x, Time.deltaTime * laneSpeed);

        // Eje Y (Altura): Lo dejamos fijo o suave (aquí uso el targetPos directo para seguir saltos, o currentPos.y para fijo)
        // Si no quieres que salte con el personaje, cambia 'targetPos.y' por 'currentPos.y'
        float y = Mathf.Lerp(currentPos.y, targetPos.y, Time.deltaTime * followSpeed);

        // Eje Z (Avance): Usamos 'followSpeed' normal
        float z = Mathf.Lerp(currentPos.z, targetPos.z, Time.deltaTime * followSpeed);

        // 4. Aplicamos la nueva posición combinada
        transform.position = new Vector3(x, y, z);
    }
}