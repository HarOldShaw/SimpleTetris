using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] int currentIndex = 0;
    [SerializeField] int lastEditedIndex = 0;

    public void addChildIndex()
    {
        currentIndex++;
    }
    public void ReduceChildtIndex(){
        currentIndex--;
    }


    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public Particle GetParticle(int i){
        return transform.GetChild(i).gameObject.GetComponent<Particle>();
    }
}
