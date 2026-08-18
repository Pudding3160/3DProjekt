using UnityEngine;

public class EnemyState_Chase : IState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyReferences refs;
    public Transform[] patrolPoints;
    public Transform patrolPointContainer;
    private float waitTimer;
    

    public EnemyState_Chase(EnemyReferences refs)
    {
        this.refs = refs;

    }
    public void Tick()
    {
        refs.navMeshagent.SetDestination(
                    refs.player.transform.position
                );
    }

    public void OnEnter()
    {
        Debug.Log("entered chase");
    }

    public void OnExit()
    {
        Debug.Log("exited chase");
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
