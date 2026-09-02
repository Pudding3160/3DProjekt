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
        refs.walk.Play();
        refs.walk.pitch = 1.4f;
        Debug.Log("entered chase");
        refs.navMeshagent.speed = 5.0f;
        refs.animator.SetBool("IsChasing",true);
    }

    public void OnExit()
    {
        Debug.Log("exited chase");
        refs.navMeshagent.speed = 2.0f;

        refs.animator.SetBool("IsChasing", false);
    }

    public Color GizmoColor()
    {
        return Color.red;
    }
}
