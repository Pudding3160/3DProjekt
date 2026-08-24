using UnityEngine;

public class EnemyState_AttackStunned : IState
{
    private EnemyReferences refs;
    private float timer = 0f;
    public EnemyState_AttackStunned(EnemyReferences refs)
    {
        this.refs = refs;
        
    }
    public Color GizmoColor()
    {
        return Color.white;
    }

    public void OnEnter()
    {
        Debug.Log("Hit Stunned");
        refs.navMeshagent.speed = 0f;

    }

    public void OnExit()
    {
    }

    public void Tick()
    { if (refs.stunned == true) { 
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            timer = 0f;
            refs.stunned=false;
        }
        }
    }

}
