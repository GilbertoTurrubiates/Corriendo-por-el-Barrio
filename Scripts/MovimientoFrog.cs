using UnityEngine;
using TMPro;
public class MovimientoFrog : MonoBehaviour
{
    public float paso=1f;
    public TextMeshProUGUI scoreText;
    private Vector3 inicio;
    private int score=0;
    void Start()
    {
        inicio=transform.position;
        ActualizarScore();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            transform.position+=Vector3.forward*paso;
            score++;
            ActualizarScore();
        }
        
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.position+=Vector3.forward*paso;
            score++;
            ActualizarScore();
        }
      
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.position+=Vector3.left*paso;
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.position+=Vector3.right*paso;
        }
       
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Car")){
            //si chocamos un auto, volvemos al inicio
            transform.position=inicio;
            score=0;
            scoreText.text="Puntuacion: "+score;

        }else if(col.gameObject.CompareTag("Meta")){
            Debug.Log("Ganaste!");
        }
    } 
    void ActualizarScore(){
        if(scoreText!=null){
            scoreText.text="Score: "+score;
        }
    }

}
