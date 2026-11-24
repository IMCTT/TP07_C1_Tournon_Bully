using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask bulletMask;
    [SerializeField] private Image enemyLife;
    [SerializeField] GameObject enemy;
    [SerializeField] private ParticleSystem enemyParticle;
    // Suggestion: cada enemigo guarda una referencia a Playermovement solo para leer powerUp (24 referencias cableadas a mano en la escena).
    // Es más simple que la bala lleve su propio daño y el power up modifique ese daño, así el enemigo no depende del jugador.
    [SerializeField] private Playermovement playermovement;

    Rigidbody2D rb;
    Vector2 loopRight = new Vector2(1, 0);
    Vector2 loopLeft = new Vector2(-1, 0);
    float looptime;

    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip SfxEnemy;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        looptime = 0;
    }
    void Update()
    {
        looptime += Time.deltaTime;
        if (looptime < 2)
        {
            // Warning: seteás rb.linearVelocity desde Update. La física va en FixedUpdate.
            // Además entre los segundos 2 y 3 no se setea ninguna velocidad y el enemigo sigue con loopRight: el ciclo real es 3 s a la derecha y 3 s a la izquierda.
            rb.linearVelocity = loopRight;
        }
        else if (looptime >= 3)
        {

            rb.linearVelocity = loopLeft;
            //rb.AddForce(Vector3.right * loopSpeed * Time.deltaTime, ForceMode2D.Force);

            if (looptime >= 6)
            {

                looptime = 0;
            }
        }

        // Suggestion: chequeás la muerte en cada frame. Hacelo una sola vez dentro de OnTriggerEnter2D, justo después de restar vida.
        if (enemyLife.fillAmount <= 0)
        {
            Destroy(enemy);
            Instantiate(enemyParticle, transform.position, quaternion.identity);
            // Warning: enemyParticle es la referencia al prefab, no a la instancia. Este Play() no hace nada sobre el sistema que se ve.
            // Guardá la instancia: var particles = Instantiate(enemyParticle, ...); particles.Play(); o confiá en playOnAwake y borrá esta línea.
            enemyParticle.Play();
            AudioSource.clip = SfxEnemy;
            AudioSource.Play();

        }

    }
    private void OnTriggerEnter2D(Collider2D other) // cuando toca un enemigo, insertar danio
    {
        if (FloorDetect.CheckLayerInMask(bulletMask, other.gameObject.layer))
        {
            if (playermovement.powerUp)
            {
                enemyLife.fillAmount -= 0.50f;
            }
            else
            {

                enemyLife.fillAmount -= 0.25f;
            }
        }
    }
}
