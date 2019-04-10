using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float delay = 3f;

    // Scene index: 0-start menu, 1-Game, 2-GameOver(win or Lose)
    public void LoadStartMenu(){
        SceneManager.LoadScene(0);
    }
    public void LoadGame(){
        SceneManager.LoadScene(1);
    }

    public void LoadLastScene(){
        // StartCoroutine(WaitAndLoad());
        SceneManager.LoadScene(2);
    }

    IEnumerator WaitAndLoad(){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }


    public void QuitGame(){
        Application.Quit();
    }

}
