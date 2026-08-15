using UnityEngine;

public class EnemyState_Chase : IState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyReferences refs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;
    private float waitTimer;
    public Transform player;
    public EnemyState_Chase(EnemyReferences refs, Transform player) 
    {
        this.refs = refs;
        this.player = player;   
    }

    public void Tick()
    {
        refs.navMeshagent.SetDestination(
                    patrolPoints[Random.Range(0, patrolPoints.Length)].position
                );
    }

    public void OnEnter()
    {
        Debug.Log("entered chase");
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public Color GizmoColor()
    {
        throw new System.NotImplementedException();
    }
}
