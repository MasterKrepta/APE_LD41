using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    [SerializeField]
    GameObject bannana;

    [SerializeField]
    float strength = 10;

    [SerializeField]
    Transform barrel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0)) {
             GameObject go = Instantiate(bannana, barrel.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * strength, ForceMode.Impulse);
        }
	}
}
