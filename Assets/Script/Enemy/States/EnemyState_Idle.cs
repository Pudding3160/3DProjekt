using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyState_Idle : IState
{
    private EnemyReferences refs;
    public EnemyState_Idle(EnemyReferences refs)
    {
        this.refs = refs;
    }

    public Color GizmoColor()
    {
        return Color.yellow;
    }

    public void OnEnter()
    {
        Debug.Log("Entered Idle");
    }

    public void OnExit()
    {
        Debug.Log("Exited Idle");
    }

    public void Tick()
    {
        Debug.Log("Still idle"); 
    }

 
}
