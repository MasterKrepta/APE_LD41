using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(EnemyState))]
public class Enemy : MonoBehaviour, IDestructable {

    [SerializeField]
    float viewRadius = 15f;
    SphereCollider sphereCollider;
    EnemyState enemyState;

    [SerializeField]
    float patrolDistance = 20f;

    [SerializeField]
    float attackRadius = 10f;

    NavMeshAgent agent;


    // Use this for initialization
    void Start () {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = viewRadius;
        enemyState = GetComponent<EnemyState>();
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (enemyState.Current) {
            case EnemyState.State.PATROL:
                Patrol();
                break;
            case EnemyState.State.ATTACKING:
                Attack();
                break;
        }
	}

    private void Attack() {
        Transform player = FindObjectOfType<FirstPersonController>().transform;
        agent.SetDestination(player.position);
    }

    private void Patrol() {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, 0, transform.position.z + patrolDistance);
        agent.SetDestination(targetPos);

    }

    public void Kill() {
        Destroy(gameObject);
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
}
