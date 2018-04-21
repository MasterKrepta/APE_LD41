using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnNewTurn : MonoBehaviour {

    
    [SerializeField]
    GameObject effect;

    [SerializeField]
    float explodeTime = 1f;

    [SerializeField]
    float boomBoomRadius = 5f; // :) I am aware this is silly naming but i dont care

    // Use this for initialization
    void Start () {
        StartCoroutine(Explode());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Explode() {
        yield return new WaitForSeconds(explodeTime);
        Collider[] colliders = Physics.OverlapSphere(transform.position, boomBoomRadius);

        foreach (var collider in colliders) {
            IDestructable destructable = collider.GetComponent<IDestructable>();
            if (destructable != null) {
                destructable.Kill();
            }
        }
        Instantiate(effect, transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
    }
}
