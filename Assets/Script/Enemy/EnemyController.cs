using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private StateMachine stateMachine;
    private EnemyReferences enemyRefs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;

    [Header("Settings")]
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float viewAngle = 90f;
   

    private void Awake()
    {
        enemyRefs = GetComponent<EnemyReferences>();
        
        patrolPoints = patrolPointContainer.GetComponentsInChildren<Transform>();



        stateMachine = new StateMachine();   // IMPORTANT: StateMachine is NOT a MonoBehaviour
    }

    private void Start()
    {
        //states
        var patrolState = new EnemyState_Patrol(enemyRefs, patrolPoints);
        var idleState = new EnemyState_Idle(enemyRefs);
        var chaseState= new EnemyState_Chase(enemyRefs);
        //transitions
        //going from idle to patrol
        stateMachine.AddTransition(idleState,patrolState,PlayerClose());
        stateMachine.AddTransition(patrolState, chaseState, PlayerInSight());
       // stateMachine.AddTransition(chaseState, patrolState, LostPlayer());
        //starting state
        stateMachine.SetState(idleState);
    }

    private Func<bool> PlayerInSight()
    {
        float distance = Vector3.Distance(
            transform.position,
            enemyRefs.player.transform.position
        );

        return distance > detectionRange && canSeePlayer();

    }

    private bool canSeePlayer()
    {
        return true;
    }

    private Func<bool> PlayerClose() => () =>
    {
        float distance = Vector3.Distance(
            transform.position,
            enemyRefs.player.transform.position
        );

        Debug.Log($"Player distance: {distance}");

        return distance <= 50f;
    };

    private void Update()=>stateMachine.Tick();
    

    private void OnDrawGizmos()
    {
        if (stateMachine != null)
        {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 2f, 0.3f);
        }
    }
}
