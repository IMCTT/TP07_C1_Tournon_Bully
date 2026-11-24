using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    // Warning: myPos y myPlay son public. Usá [SerializeField] private y nombres descriptivos (offset, target).
    public Vector3 myPos;
    public Transform myPlay;

    // Suggestion: el seguimiento de cámara va en LateUpdate para que se ejecute después de que el jugador se movió en el frame.
    public void Update()
    {
        transform.position = myPlay.position + myPos;
    }
}
