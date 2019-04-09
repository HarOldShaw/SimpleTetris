using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("collision layer: "+other.gameObject );
        if(other.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
