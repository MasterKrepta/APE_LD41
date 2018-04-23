using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnManager : MonoBehaviour {

    #region Singleton

    private static TurnManager instance = null;
    public static TurnManager Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        }
        else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    SceneManagement sceneManagement;

    #region UI Stuff
    [SerializeField] Camera activeCamera;
    [SerializeField] Camera PlayerCam;
    [SerializeField] Camera EnemyCam;
    bool camSwitch = false;

    [SerializeField] Health playerHealth;
    [SerializeField] Health enemyHealth;
    [SerializeField] Text txtRemaining;
    #endregion

    [SerializeField] float timeToNextTurn = 7f;
    float currentTime;
    [SerializeField] bool playerTurn = true;
    [SerializeField] bool enemyTurn = false;
    public bool gameOver = false;
    public bool IsTesting = false;
    public bool canAttack = true;

    private void Start() {
        gameOver = false;
        sceneManagement = GetComponent<SceneManagement>();
        txtRemaining = FindObjectOfType<Text>();
        ResetClock();
        SetCamera();

    }

    private void DestroyActiveBananas() {
        ExplodeOnNewTurn[] active = FindObjectsOfType<ExplodeOnNewTurn>();
        enemyBoom[] enemyBoom = FindObjectsOfType<enemyBoom>();
        foreach (var banana in active) {
            banana.ExplodeAtStartOfTurn();
        }
        foreach (var banana in enemyBoom) {
            banana.ExplodeAtStartOfTurn();
        }
    }



    void Update() {
        if (gameOver) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                CallRestart();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
                
                CallGameOver();
            }
        }

        if (!IsTesting && !gameOver) {

            currentTime -= Time.deltaTime;
            txtRemaining.text = "Time Remaining: " + (currentTime % 60).ToString("00");
            if (currentTime <= 0) {
                ResetClock();
                NewTurn();
            }
        }
        else {
            playerTurn = true;
            enemyTurn = true;
            canAttack = true;
        }

    }
    public void HasFired() {
        canAttack = false;
    }

    void ResetCanFire() {
        canAttack = true;
    }

    private void ResetClock() {
        currentTime = timeToNextTurn;
    }

    public void NewTurn() {
        if (!gameOver) {
            playerTurn = !playerTurn;
            enemyTurn = !enemyTurn;
            SetCamera();
            ResetCanFire();
            DestroyActiveBananas();
            if (EnemyCam == null) {
                CallYouWin();
            }
            else if (PlayerCam == null) {
                CallGameOver();
            }
        }

    }

    private void SetCamera() {
        camSwitch = !camSwitch;
        PlayerCam.gameObject.SetActive(camSwitch);
        EnemyCam.gameObject.SetActive(!camSwitch);
        playerHealth.enabled = camSwitch;
        enemyHealth.enabled = !camSwitch;


    }

    public bool AcceptInput() {
        if (playerTurn) {
            return true;
        }
        else
            return false;
    }
    public bool AcceptAIInput() {
        if (enemyTurn) {
            return true;
        }
        else
            return false;
    }

    public void CallYouWin() {
        sceneManagement.LoadVictory();
    }
    public void CallGameOver() {
        sceneManagement.LoadGameOver();
    }
    public void CallRestart() {
        sceneManagement.RestartGame();
    }
}





