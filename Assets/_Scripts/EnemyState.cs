using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

    public enum State {
        PATROL,
        ATTACKING
    }

    public State Current;

    private void Start() {
        Current = State.PATROL;
    }

    public void ChangeState() {
        if(Current == State.PATROL) {
            Current = State.ATTACKING;
        }
        else {
            Current = State.PATROL;
        }
        //Debug.Log("State Changed to: " + Current);
    }
}
