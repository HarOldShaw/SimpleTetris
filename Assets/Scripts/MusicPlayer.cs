using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        SetSingleton();
    }

    void SetSingleton(){
        if(FindObjectsOfType<MusicPlayer>().Length >1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }

}
