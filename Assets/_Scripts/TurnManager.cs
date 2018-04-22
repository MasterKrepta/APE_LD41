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

    [SerializeField] Health playerHealth;
    [SerializeField] Health enemyHealth;
    [SerializeField]Text txtRemaining;
    #endregion

    [SerializeField] float timeToNextTurn = 7f;
    float currentTime;
    [SerializeField] bool playerTurn = true;
    [SerializeField] bool enemyTurn = false;

    public bool IsTesting = false;
    public bool canAttack = true;
    Enemy enemy;

    private void Start() {
        enemy = FindObjectOfType<Enemy>();
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
        DestroyActiveBananas();
        ResetCanFire();
        
        enemy.OnNewTurn();
    }

    private void SetCamera() {
        camSwitch = !camSwitch;
        PlayerCam.gameObject.SetActive(camSwitch);
        EnemyCam.gameObject.SetActive(!camSwitch);
        //playerHealth.enabled = camSwitch;
        //enemyHealth.enabled = !camSwitch;
        //NOTE bullets have been seperated due to a late minute bug - IS NOW LAYER BASED INSIDE OF UNITY

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