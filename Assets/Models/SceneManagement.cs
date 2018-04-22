using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour {

    


    public void LoadMainMenu(){
        
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver() {
        
        SceneManager.LoadScene(2);
    }
    public void LoadVictory() {
        SceneManager.LoadScene(3);
    }

    public void RestartGame() {
        SceneManager.LoadScene(1);
    }
}
