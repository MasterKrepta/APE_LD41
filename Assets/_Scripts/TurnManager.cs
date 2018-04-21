using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    public bool playerTurn = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
