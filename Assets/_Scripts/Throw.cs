using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Throw : MonoBehaviour {

    bool acceptPlayerInput = true;
    FirstPersonController FPS;

    [SerializeField]GameObject banana;
    [SerializeField]float strength = 10;
    [SerializeField]Transform barrel;

    private void Awake() {
        FPS = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Z)) { // FOR TESTING ONLY!!!! DONT KEEP THIS!!!!
            TurnManager.Instance.NewTurn();
        }

        acceptPlayerInput = TurnManager.Instance.AcceptInput();
        if (acceptPlayerInput) {
            FPS.enabled = true;
            if (Input.GetMouseButtonDown(0) && TurnManager.Instance.canAttack) {
                TurnManager.Instance.HasFired();
                GameObject go = Instantiate(banana, barrel.position, Quaternion.identity);
                go.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * strength, ForceMode.Impulse);
            }
        }
        else {
            FPS.enabled = false;
        }
	}
}
