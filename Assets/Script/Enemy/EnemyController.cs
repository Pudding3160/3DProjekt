using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyController : MonoBehaviour
{
    private StateMachine stateMachine;
    private EnemyReferences enemyRefs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;
    private EnemySight sight;

   

    private void Awake()
    {
        enemyRefs = GetComponent<EnemyReferences>();
        
        patrolPoints = patrolPointContainer.GetComponentsInChildren<Transform>();

        sight = GetComponent<EnemySight>();


        stateMachine = new StateMachine();  
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
        stateMachine.AddTransition(chaseState, patrolState, LostPlayer());
        //starting state
        stateMachine.SetState(idleState);
    }

    private Func<bool> LostPlayer() => ()=>{
        Debug.Log($"Time since spotted: {sight.timeSinceLostPlayer}");
        return sight.lostPlayer();
        };

    private Func<bool> PlayerInSight() => () =>
    {

 

        return sight.canSeePlayer();

    };

    private Func<bool> PlayerClose() => () =>
    {
        float distance = Vector3.Distance(
            transform.position,
            enemyRefs.player.transform.position
        );


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
