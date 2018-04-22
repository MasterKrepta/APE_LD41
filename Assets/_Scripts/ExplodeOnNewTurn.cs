using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnNewTurn : MonoBehaviour {
    
    [SerializeField]GameObject effect;

    [SerializeField]float explodeTime = 5f;
    
    [SerializeField]float boomBoomRadius = 5f; // :) I am aware this is silly naming but i dont care

    // Use this for initialization
    void Start () {
        if(TurnManager.Instance.IsTesting)
            StartCoroutine(Explode());
	}

    IEnumerator Explode() {
        yield return new WaitForSeconds(explodeTime);
        Collider[] colliders = Physics.OverlapSphere(transform.position, boomBoomRadius);

        foreach (var collider in colliders) {
            IDestructable destructable = collider.GetComponent<IDestructable>();
            if (destructable != null) {
                destructable.TakeDamage(1);
            }
        }
        Instantiate(effect, transform.position, Quaternion.identity);
        
        Destroy(this.gameObject);
    }

    public void ExplodeAtStartOfTurn() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, boomBoomRadius);

        foreach (var collider in colliders) {
            IDestructable destructable = collider.GetComponent<IDestructable>();
            
            if (destructable != null) {
                destructable.TakeDamage(1);
            }
        }
        Instantiate(effect, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, boomBoomRadius);
    }
}