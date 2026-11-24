
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float initialForce = 10.0f;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bullet;
    

   
    void Update()
    {

        // Warning: la bala no tiene tiempo de vida. Si no toca ningún trigger viaja para siempre y queda en la escena.
        // Agregá Destroy(gameObject, lifeTime) en Start o usá un pool.
        transform.position += -transform.right * Time.deltaTime * initialForce;


    }
    private void OnTriggerEnter2D(Collider2D other) // cuando toca un enemigo, insertar danio
    {
        
            
            // Warning: la bala se destruye con cualquier trigger (monedas, power ups, la zona de suelo) porque no filtrás por capa.
            // Ya declaraste enemyMask y no lo usás: chequeá FloorDetect.CheckLayerInMask(enemyMask, other.gameObject.layer) antes de destruir.
            Destroy(bullet);


        
    }
}
