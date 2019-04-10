using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] int debrisHit = 0;
    [SerializeField] int ipg = 0;
    [SerializeField] int rGroup = 0;  
    [SerializeField] int gGroup = 0;  
    [SerializeField] int bGroup = 0;
    [SerializeField] int rbgGroup = 0;    
    [SerializeField] UIDisplay ui;

    private void Awake() {
        SetupSingleton();    
    }

    void Start(){
        ui = FindObjectOfType<UIDisplay>();
    }

    void SetupSingleton(){
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        if(gameSessionCount >1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DebrisDestroy(){
        score += 5;
        debrisHit ++;
        ui.ShowDH(debrisHit);
        ui.ShowScore(score);
    }

    public void VerticalDestroy(int type){
        if(type == 1){
            rGroup++;
            ui.ShowRedGroup(rGroup);
        }else if(type == 2){
            gGroup++;
            ui.ShowGreenGroup(gGroup);
        }else if(type == 3){
            bGroup++;
            ui.ShowBlueGroup(bGroup);
        }
        score += 10;
        ui.ShowScore(score);
        ui.ShowIPG(GetIPG());
     }

    public void HorizontalDestroy(int num){
        score += 15 * num;
        rbgGroup += num;
        ui.ShowScore(score);
        ui.ShowRGBGroup(rbgGroup);
        ui.ShowIPG(GetIPG());
    }

    public int GetIPG(){
        return rGroup+bGroup+gGroup+rbgGroup;
    }

}
