using System.Net.Sockets;
using UnityEngine;

public class EnemyState_PlayerLastPos : IState
{
    private EnemyReferences refs;
    private Transform playerLastPos;
    private EnemySight sight;
    public EnemyState_PlayerLastPos(EnemyReferences refs, EnemySight sight)
    {
        this.refs = refs;
        this.sight = sight; 

    }
    public Color GizmoColor()
    {
        return Color.orange;
    }

    public void OnEnter()
    {

        refs.navMeshagent.SetDestination(
                   refs.playerLastPos.position
                );
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Tick()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
