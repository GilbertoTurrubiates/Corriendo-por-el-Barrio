using UnityEngine;

public class Car : MonoBehaviour
{
    public float velocidad = 3f;
    public float limite = 10f;
    private int direccion = 1;

    private float inicioX;
    
    void Start(){
        inicioX = transform.position.x;
    }


    void Update()
    {
        transform.Translate(Vector3.down * velocidad * direccion
         * Time.deltaTime);

        float offset=transform.position.x - inicioX;

        if (offset > limite || offset < -limite)
        {
            direccion *= -1;

            // Clamp para evitar pasarse y rebotar mal
            float x = Mathf.Clamp(transform.position.x, inicioX-limite,
             inicioX+ limite);
            transform.position = new Vector3(x, transform.position.y, 
            transform.position.z);
        }
    }
}