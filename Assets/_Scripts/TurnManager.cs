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
    [SerializeField] Camera activeCamera;
    [SerializeField] Camera PlayerCam;
    [SerializeField] Camera EnemyCam;
    bool camSwitch = false;

    [SerializeField]Text txtRemaining;
    #endregion

    [SerializeField] float timeToNextTurn = 7f;
    float currentTime;
    [SerializeField] bool playerTurn = true;
    [SerializeField] bool enemyTurn = false;

    public bool IsTesting = false;
    public bool canAttack = true;

    private void Start() {
        ResetClock();
        SetCamera();
        
    }

    private void DestroyActiveBananas() {
        ExplodeOnNewTurn[] active = FindObjectsOfType<ExplodeOnNewTurn>();
        foreach (var banana in active) {
            banana.ExplodeAtStartOfTurn();
        }
    }

    

    void Update () {
        if (!IsTesting) {
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
        playerTurn = !playerTurn;
        enemyTurn = !enemyTurn;
        SetCamera();
        ResetCanFire();
        DestroyActiveBananas();
    }

    private void SetCamera() {
        camSwitch = !camSwitch;
        PlayerCam.gameObject.SetActive(camSwitch);
        EnemyCam.gameObject.SetActive(!camSwitch);

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