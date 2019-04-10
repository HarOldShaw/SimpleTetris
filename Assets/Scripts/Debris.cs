using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [SerializeField] GameSession gameSession;
    
    [Header("Effects")]
    [SerializeField] GameObject destructionVFX;
    [SerializeField] float durationOfDestruction = 0.5f;
    [SerializeField] AudioClip destructionSFX;
    
    [SerializeField] [Range(0,1)]float destructionVolumn = 0.5f;
    void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("collision layer: "+other.gameObject );
        if(other.gameObject.layer == 9)
        {
            gameSession.DebrisDestroy();
            Destroy(gameObject);
            GameObject destruction = Instantiate(destructionVFX,transform.position,transform.rotation);
            Destroy(destruction, durationOfDestruction);
            AudioSource.PlayClipAtPoint(destructionSFX,Camera.main.transform.position, destructionVolumn);
        }
    }
}
