using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] Text dhText;
    [SerializeField] Text ipgText;
    [SerializeField] Text scoreText;
    [SerializeField] Text rGroupText;
    [SerializeField] Text gGroupText;
    [SerializeField] Text bGroupText;
    [SerializeField] Text rgbGroupText;

    // Start is called before the first frame update

    public void ShowDH(int count){
       dhText.text = "DH: "+count;
    }

    public void ShowIPG(int count){
       ipgText.text = "IPG: "+count;
    }

    public void ShowScore(int score){
       scoreText.text = "Score: "+score;
    }

    public void ShowRedGroup(int count){
       rGroupText.text = "R: "+count;
    }
    
    public void ShowGreenGroup(int count){
       gGroupText.text = "G: "+count;
    }
    
    public void ShowBlueGroup(int count){
       bGroupText.text = "B: "+count;
    }
    
    public void ShowRGBGroup(int count){
       rgbGroupText.text = "RGB: "+count;
    }
}

