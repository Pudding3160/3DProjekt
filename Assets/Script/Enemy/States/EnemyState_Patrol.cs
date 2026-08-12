using UnityEngine;

public class EnemyState_Patrol : IState
{
    private EnemyReferences enemyReferences;
    private PatrolArea patrolArea;

    public EnemyState_Patrol(EnemyReferences enemyReferences, PatrolArea patrolPoint)
    {
        this.enemyReferences = enemyReferences;
        this.patrolArea = patrolPoint; 
    }

    public Color GizmoColor()
    {
        return Color.yellow;
    }

    public void OnEnter()
    {
        PatrolPoint nextPoint = this.patrolArea.GetRandomPoint(enemyReferences.transform.position);
        enemyReferences.navMeshagent.SetDestination(nextPoint.transform.position);
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        throw new System.NotImplementedException();
    }

   
}
