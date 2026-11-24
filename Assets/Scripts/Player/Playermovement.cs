
using UnityEngine;
using UnityEngine.UI;


public class Playermovement : MonoBehaviour
{
    [Header("Parametros")]
    [SerializeField] float speed = 1.0f;
    // Warning: jumpForce es public y cualquier clase puede mutarlo. Pasalo a [SerializeField] private float jumpForce;
    // y si otra clase lo necesita, exponé una property de solo lectura.
    public float jumpForce = 10;
    [SerializeField] Image playerLifebar;
    // Warning: powerUp e invincible son public y los modifican PowerUP, Invincibility, Enemies y Cacty escribiendo el campo directo.
    // Hacelos private y exponé métodos (ActivatePowerUp(), ActivateInvincibility()) y properties de solo lectura (IsPoweredUp).
    public bool powerUp = false;
    public bool invincible = false;
    private float invincibilityTimer;

    [Header("Movimiento")]

    [SerializeField] private KeyCode keyLeft = KeyCode.A;
    [SerializeField] private KeyCode keyRight = KeyCode.D;
    [SerializeField] private KeyCode shoot = KeyCode.R;
    [SerializeField] private KeyCode jump = KeyCode.W;
    [SerializeField] private Animator animator;
    [SerializeField] FloorDetect floorDetect;

    private Rigidbody2D rb;

    // Suggestion: el campo flip nunca se usa. Además en los bloques de keyLeft y keyRight declarás una variable local con el mismo nombre que lo tapa.
    // Borrá el campo o renombrá la local (por ejemplo lookRotation).
    public bool flip = false;

    [Header("Objetos")]
    [SerializeField] private GameObject bullet;
    [SerializeField] Transform launchOffset;
    [SerializeField] private LayerMask enemyMask;
    // Suggestion: playerMask no se usa en ningún lugar de esta clase. Borralo.
    [SerializeField] private LayerMask playerMask;


    [Header("Audio")]
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip SfxJump;
    [SerializeField] AudioClip SfxShoot;

   // Warning: sr es public, no se usa en esta clase y en la escena queda en None hasta que Awake lo pisa.
   // Si no lo necesitás borralo; si lo necesitás, [SerializeField] private o private con GetComponent en Awake.
   public  SpriteRenderer sr;



    private GameObject shot;
    


    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        invincibilityTimer = 0;

    }

    void Update()
    {
        Movevement();

        if (invincible)
        {
            invincibilityTimer += Time.deltaTime;
        }
        // Suggestion: el 10 es la duración de la invencibilidad y está hardcodeado. Llevalo a un campo serializado o a un ScriptableObject (const o invincibilityDuration).
        if (invincibilityTimer > 10)
        {
            invincible = false;
            invincibilityTimer = 0;
        }
    }


    private void Movevement()
    {
        if (Input.GetKeyDown(jump) && floorDetect.IsGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            AudioSource.clip = SfxJump;
            AudioSource.Play();

            



        }
       

        if (Input.GetKey(keyLeft))
        {

            // Warning: aplicás fuerzas al Rigidbody2D desde Update. La física va en FixedUpdate: leé el input en Update, guardalo en un campo
            // y aplicá la fuerza en FixedUpdate con Time.fixedDeltaTime (o seteá rb.linearVelocity en x).
            rb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode2D.Impulse);
            Quaternion flip = Quaternion.Euler(0, 0, 0);
            transform.rotation = flip;
            
            // Bug: SetTrigger("walk") y SetTrigger("Idle") se llaman todos los frames. Si mantenés A, este bloque dispara "walk" y el else del bloque
            // de keyRight dispara "Idle" en el mismo frame, así que el Animator recibe los dos triggers a la vez y la animación salta entre estados.
            // Usá un parámetro float (Speed = Mathf.Abs(rb.linearVelocity.x)) o un bool IsWalking y transiciones por condición. Los strings van como const.
            animator.SetTrigger("walk");
            


        }
        else if (Input.GetKeyUp(keyLeft))
        {
            
            // Bug: al soltar la tecla seteás rb.linearVelocity = Vector3.zero, que también anula la velocidad en Y.
            // Si soltás A en el aire el personaje se frena en seco y empieza a caer desde cero. Poné solo la X en 0: rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            rb.linearVelocity = Vector3.zero;
            
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        if (Input.GetKey(keyRight))
        {
            
            animator.SetTrigger("walk");
            Quaternion flip = Quaternion.Euler(0, 180, 0);
            transform.rotation = flip;
            rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            // Warning: SpeedLimit() está comentado y no hay ningún tope de velocidad: mientras mantenés la tecla el impulso se acumula frame a frame.
            // Limitá la velocidad horizontal (Mathf.Clamp sobre rb.linearVelocity.x) o seteá la velocidad directamente.
            //SpeedLimit();
        }
        else if (Input.GetKeyUp(keyRight))
        {
           
            rb.linearVelocity = Vector3.zero;
            
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        // Bug: con el juego en pausa (UIManager pone Time.timeScale = 0) el disparo se sigue procesando e instancia balas que quedan congeladas.
        // Chequeá Time.timeScale > 0 o un estado de pausa antes de leer el input.
        if (Input.GetKeyDown(shoot))
        {
           
                Instantiate(bullet, launchOffset.position, transform.rotation);
            
            
            AudioSource.clip = SfxShoot;
            AudioSource.Play();
            animator.SetTrigger("Shot");

        }
        

    }



    private void OnTriggerEnter2D(Collider2D other) // cuando toca un enemigo, insertar danio
    {
        if (FloorDetect.CheckLayerInMask(enemyMask, other.gameObject.layer))
        {
            // Suggestion: el if (invincible) tiene el cuerpo vacío. Invertí la condición: if (!invincible) { ... }.
            if (invincible)
            {
                
            }
            else 
            {
                // Error: la vida baja con fillAmount pero nunca se chequea si llegó a 0. No hay condición de derrota ni sonido al perder, que pide la consigna.
                // Después de restar, si playerLifebar.fillAmount <= 0 activá un panel de Game Over, reproducí un clip de derrota y frená el juego.
                // Mejor aún, llevá la vida a un int/float propio y que la barra solo la muestre.
                playerLifebar.fillAmount -= 0.25f;

                animator.SetTrigger("hit");
            }
            
        }

    }



}

