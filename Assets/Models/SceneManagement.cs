using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour {

    


    public void LoadMainMenu(){
        
        SceneManager.LoadScene(0);
    }

    public void LoadGameOver() {
        TurnManager.Instance.gameOver = true;
        SceneManager.LoadScene(2);
    }
    public void LoadVictory() {
        TurnManager.Instance.gameOver = true;
        SceneManager.LoadScene(3);
    }

    public void RestartGame() {
        TurnManager.Instance.gameOver = false;
        SceneManager.LoadScene(1);
    }
    public void StartGame() {
        
        SceneManager.LoadScene(1);
    }
}
