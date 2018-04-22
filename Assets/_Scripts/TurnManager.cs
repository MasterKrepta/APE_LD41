using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class TurnManager : MonoBehaviour {

    SceneManagement sceneManagement;
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
        sceneManagement = GetComponent<SceneManagement>();
        enemy = FindObjectOfType<Enemy>();
        PlayerCam = FindObjectOfType<FirstPersonController>().GetComponentInChildren<Camera>();
        EnemyCam = FindObjectOfType<Enemy>().GetComponentInChildren<Camera>();
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

        if (EnemyCam = null) {
            CallYouWin();
        }
        else if (PlayerCam = null) {
            CallGameOver();
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
        Debug.Log(PlayerCam.name);
        
        playerTurn = !playerTurn;
        enemyTurn = !enemyTurn;
        
        SetCamera();
        
        DestroyActiveBananas();
        Debug.Log(PlayerCam.name + " on the way out of destroy bannanas");

        ResetCanFire();
        
        enemy.OnNewTurn();
    }

    private void SetCamera() {
        PlayerCam = FindObjectOfType<FirstPersonController>().GetComponentInChildren<Camera>();
        EnemyCam = FindObjectOfType<Enemy>().GetComponentInChildren<Camera>();

        camSwitch = !camSwitch;
        PlayerCam.gameObject.SetActive(camSwitch);
        EnemyCam.gameObject.SetActive(!camSwitch);

        //playerHealth.enabled = camSwitch;
        //enemyHealth.enabled = !camSwitch;
        //NOTE bullets have been seperated due to a late minute bug - IS NOW LAYER BASED INSIDE OF UNITY
        Debug.Log(PlayerCam.name + " end of set camera");
        
    }
    public void CallYouWin() {
        sceneManagement.LoadVictory();
    }

    public void CallGameOver() {
        sceneManagement.LoadGameOver();
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