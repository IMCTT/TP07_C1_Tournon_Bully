using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] Playermovement playermovement;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] AudioClip SfxPick;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FloorDetect.CheckLayerInMask(playerMask, collision.gameObject.layer))
        {
            // Suggestion: el power up queda activo para siempre porque nadie vuelve powerUp a false. Si es intencional está bien; si no, agregale duración como a la invencibilidad.
            playermovement.powerUp = true;
            AudioSource.clip = SfxPick;
            AudioSource.Play();

            Destroy(gameObject);
            
        }

    }
}
