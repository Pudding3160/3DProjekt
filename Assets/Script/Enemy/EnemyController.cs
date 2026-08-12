using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyReferences enemyRef;
    private StateMachine stateMachine;

    void Start()
    {
        enemyRef = GetComponent<EnemyReferences>();

        // StateMachine is NOT a MonoBehaviour → instantiate manually
        stateMachine = new StateMachine();

        PatrolArea area = GetComponent<PatrolArea>();

        // STATES
        var patrol = new EnemyState_Patrol(enemyRef, area);

        // SET INITIAL STATE
        stateMachine.SetState(patrol);

        // TRANSITIONS (example)
        void At(IState from, IState to, Func<bool> condition) =>
            stateMachine.AddTransition(from, to, condition);

        void Any(IState to, Func<bool> condition) =>
            stateMachine.AddAnyTransition(to, condition);

        // Example transition (you can add your own)
        // At(patrol, chase, () => enemyRef.CanSeePlayer);
    }

    void Update()
    {
        stateMachine.Tick();
    }

    private void OnDrawGizmos()
    {
        if (stateMachine != null)
        {
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
    }
}
