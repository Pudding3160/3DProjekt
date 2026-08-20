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
    public Transform[] playerCirclePoints;
    public Transform playerCircleContainer;
    private EnemySight sight;
    private EnemyBite bite;
    private bool aggresive = true;
    public bool attackDone;
   

    private void Awake()
    {
        enemyRefs = GetComponent<EnemyReferences>();
        
        patrolPoints = patrolPointContainer.GetComponentsInChildren<Transform>();

        playerCirclePoints = playerCircleContainer.GetComponentsInChildren<Transform>();

        sight = GetComponent<EnemySight>();

        bite= GetComponent<EnemyBite>();    

        stateMachine = new StateMachine();  
    }

    private void Start()
    {
        //states
        var patrolState = new EnemyState_Patrol(enemyRefs, patrolPoints);
        var idleState = new EnemyState_Idle(enemyRefs);
        var chaseState= new EnemyState_Chase(enemyRefs);
        var circleState= new EnemyState_Circle(enemyRefs,playerCirclePoints);
        var attackState = new EnemyState_Bite();

        //transitions
        //going from idle to patrol
       // stateMachine.AddTransition(idleState,patrolState,PlayerClose());
        stateMachine.AddAnyTransition(chaseState, PlayerInSightChase());
        stateMachine.AddTransition(chaseState, idleState, PlayerInAttackRange());
       // stateMachine.AddTransition(attackState, chaseState, CantBite());
       // stateMachine.AddTransition(attackStunnedState, idleState, StunCompleted());
       // stateMachine.AddAnyTransition(stunnedFromAttackState, HitByAttack());
        stateMachine.AddTransition(patrolState, circleState, PlayerInSight());
        stateMachine.AddTransition(chaseState, patrolState, LostPlayer());


        //starting state
        stateMachine.SetState(patrolState);
    }

    private Func<bool> CantBite() => () =>
    {
        return !bite.canBite();
    };

    private Func<bool> PlayerInAttackRange() => () =>
    {

        return bite.canBite();
    };

   

    private Func<bool> LostPlayer() => ()=>{
       // Debug.Log($"Time since spotted: {sight.timeSinceLostPlayer}");
        return sight.lostPlayer();
        };

    private Func<bool> PlayerInSight() => () =>
    {
        return sight.canSeePlayer() && !aggresive;

    };
    private Func<bool> PlayerInSightChase() => () =>
    {
        return sight.canSeePlayer() && aggresive;


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
