using System;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyState_Bite : IState
{
    private EnemyReferences refs;

    public EnemyState_Bite(EnemyReferences refs)
    {
        this.refs = refs;
    }
    public Color GizmoColor()
    {
        return Color.darkViolet;
    }

    public void OnEnter()
    {
        refs.walk.Stop();
        
        Debug.Log("Entering bite");
        bite();
        refs.navMeshagent.speed = 0;
        refs.animator.SetBool("IsBiting",true);
    }

    public void OnExit()
    {
       
    }

    public void Tick()
    {

    }

    private void bit2e()
    {
        Debug.Log("bited");
    }

    private void bite()
    {
        Transform player = refs.playerObject.transform;

        Vector3 direction = refs.enemyTransform.position - player.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            player.rotation = Quaternion.LookRotation(direction);
        }

        refs.playerControl.Die();
        
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
