using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

// Suggestion: Cacty es una copia de Enemies.cs que solo cambia la velocidad (3 en vez de 1) y el flip del sprite.
// Unificalos en una sola clase con la velocidad serializada (o en un ScriptableObject de datos del enemigo).
public class Cacty : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask bulletMask;
    [SerializeField] private Image enemyLife;
    [SerializeField] GameObject enemy;
    [SerializeField] private ParticleSystem enemyParticle;
    [SerializeField] private Playermovement playermovement;

    Rigidbody2D rb;
    Vector2 loopRight = new Vector2(3, 0);
    Vector2 loopLeft = new Vector2(-3, 0);
    float looptime;

    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip SfxEnemy;
    [SerializeField] SpriteRenderer sp;


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
            // Warning: rb.linearVelocity se setea desde Update. Movelo a FixedUpdate.
            rb.linearVelocity = loopRight;
            sp.flipX = true;
        }
        else if (looptime >= 3)
        {

            rb.linearVelocity = loopLeft;
            sp.flipX = false;
          

            if (looptime >= 6)
            {

                looptime = 0;
            }
        }

        if (enemyLife.fillAmount <= 0)
        {
            Destroy(enemy);
            Instantiate(enemyParticle, transform.position, quaternion.identity);
            // Warning: Play() se llama sobre el prefab, no sobre la instancia que creaste en la línea anterior. No tiene efecto.
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
