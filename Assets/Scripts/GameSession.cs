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
    [SerializeField] int debrisInStore = 0;    
    [SerializeField] UIDisplay ui;

    [Header("Labels need to be assigned")]
    [SerializeField] GameObject winLabel;
    [SerializeField] GameObject loseLabelOne;
    [SerializeField] GameObject loseLabelTwo;

    bool gamePause = false;
    // private void Awake() {
    //     SetupSingleton();    
    // }

    void Start(){
        ui = FindObjectOfType<UIDisplay>();
        winLabel.SetActive(false);
        loseLabelOne.SetActive(false);
        loseLabelTwo.SetActive(false);
        ui.ShowScore(score);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(gamePause){
                gamePause = false;
                Time.timeScale = 1;
            }else{
                gamePause = true;
                Time.timeScale = 0;
            }
        }
    }

    // void SetupSingleton(){
    //     int gameSessionCount = FindObjectsOfType<GameSession>().Length;
    //     if(gameSessionCount >1){
    //         Destroy(gameObject);
    //     }else{
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }


    private void AddScore(int amount){
        score += amount;
        ui.ShowScore(score);
        CheckWinCondition();
    }


    public void AddDebrisInStore(){
        debrisInStore++;
        CheckLoseCondition();
    }

    public void DebrisDestroy(){
        AddScore(5);
        debrisHit ++;
        ui.ShowDH(debrisHit);   
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
        AddScore(10);
        ui.ShowIPG(GetIPG());
     }

    public void HorizontalDestroy(int num){
        AddScore(15 * num);
        rbgGroup += num;
        ui.ShowRGBGroup(rbgGroup);
        ui.ShowIPG(GetIPG());
    }
 
    public int GetIPG(){
        return rGroup+bGroup+gGroup+rbgGroup;
    }
   private void CheckWinCondition(){
        if(score >= 100){
            winLabel.SetActive(true);
            StartCoroutine(Pause(3));
       }
    }

    private void CheckLoseCondition(){
        if(debrisInStore>=5){
            loseLabelOne.SetActive(true);
            StartCoroutine(Pause(3));
        }
    }

    public void HandleSecondLoseCondition(){
        loseLabelTwo.SetActive(true);
        StartCoroutine(Pause(3)); 
    }

    private IEnumerator Pause (float pauseDuration) {
        float originalTimeScale = Time.timeScale; // store original time scale in case it was not 1
        Time.timeScale = 0; // pause
        float t = 0;
        while (t < pauseDuration) {
            yield return null; // do nothing if Time.timeScale is 0!
            t += Time.unscaledDeltaTime; // returns deltaTime without being multiplied by Time.timeScale
        }
        Time.timeScale = originalTimeScale; // restore time scale from before pause
        FindObjectOfType<SceneLoader>().LoadLastScene(); // load the last scene
    }


}
