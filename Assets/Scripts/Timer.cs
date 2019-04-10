using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    string minute = "00"; 
    string second = "00"; 
    private bool timerStarted = true;
    Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted){
            time += Time.deltaTime;
            minute = Mathf.Floor(time/60).ToString("00");
            second = (time%60).ToString("00");
            SetText();
        }
    }

    void SetText(){
        timerText.text ="T: "+ minute + ":" +second;
    }
}
