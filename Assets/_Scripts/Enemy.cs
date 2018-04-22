
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(EnemyState))]
public class Enemy : MonoBehaviour {

    bool acceptEnemyMovement = false;
    Animator anim;
    SphereCollider sphereCollider;
    EnemyState enemyState;
    NavMeshAgent agent;
    Vector3 newDest;

    [SerializeField] float viewRadius = 15f;
    [SerializeField] float patrolDistance = 20f;
    [SerializeField] float attackRadius = 10f;
    [SerializeField] GameObject banana;
    bool hasArrived = false;
    [SerializeField]float strength = 10;

    [SerializeField]Transform barrel;

    
    // Use this for initialization
    void Start () {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = viewRadius;
        enemyState = GetComponent<EnemyState>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        acceptEnemyMovement = TurnManager.Instance.AcceptAIInput();
        if (acceptEnemyMovement && TurnManager.Instance.canAttack) {
            agent.isStopped = false;
            
            switch (enemyState.Current) {
                case EnemyState.State.PATROL:
                    if(agent.remainingDistance == 0) {
                        hasArrived = true;
                    }
                    Patrol();
                    break;
                case EnemyState.State.ATTACKING:
                    Attacking();
                    break;
            }
        }
        else {
            agent.isStopped = true;
        }
        
        
    }

    private void Attacking() {

        Transform player = FindObjectOfType<FirstPersonController>().transform;
        
        if(player != null) {
            transform.LookAt(player);
            agent.SetDestination(player.position);
            float distToPlayer = Vector3.Distance(transform.position, player.position);
            if (distToPlayer <= attackRadius && TurnManager.Instance.canAttack) {
                TurnManager.Instance.HasFired();
                ThrowAtPlayer();
            }
        }
        
    }
    public void OnNewTurn() {
        if(enemyState.Current == EnemyState.State.PATROL) {
            newDest =  GetNewDestination();
        }
    }


    private void ThrowAtPlayer() {
        anim.Play("Throw");
        agent.isStopped = true;
        TurnManager.Instance.canAttack = false;
        GameObject go = Instantiate(banana, barrel.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        go.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * strength, ForceMode.Impulse);
    }

    private void Patrol() {
        if (hasArrived || agent.remainingDistance <= 1) {
            
            newDest = GetNewDestination();
        }
        agent.SetDestination(newDest);
    }

    Vector3 GetNewDestination() {
        Vector3 randPos = UnityEngine.Random.insideUnitSphere * patrolDistance;
        randPos += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randPos, out hit, patrolDistance, 1);
        hasArrived = false;
        return hit.position;
    }



    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<FirstPersonController>() != null) {
            enemyState.ChangeState();
            
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<FirstPersonController>() != null) {
            enemyState.ChangeState();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
