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

    #region UI Stuff
    
    [SerializeField]Text txtRemaining;
    #endregion

    [SerializeField] float timeToNextTurn = 7f;
    float currentTime;
    [SerializeField] bool playerTurn = true;
    [SerializeField] bool enemyTurn = false;

    public bool IsTesting = false;

    private void Start() {
        ResetClock();
    }

    void Update () {
        if (!IsTesting) {
            currentTime -= Time.deltaTime;
            FormatTime();
            txtRemaining.text = "Time Remaining: " + (currentTime % 60).ToString("00");
            if (currentTime <= 0) {
                ResetClock();
                NewTurn();
            }
        }
        else {
            playerTurn = true;
            enemyTurn = true;
        }
        
	}

    private void FormatTime() {
        float seconds = Mathf.Floor(currentTime % 60);
    }

    private void ResetClock() {
        currentTime = timeToNextTurn;
    }

    public void NewTurn() {
        playerTurn = !playerTurn;
        enemyTurn = !enemyTurn;
    }

    public bool AcceptInput() {
        if (playerTurn) {
            return true;
        }else
            return false;
    }
    public bool AcceptAIInput() {
        if (enemyTurn) {
            return true;
        }
        else
            return false;
    }
}