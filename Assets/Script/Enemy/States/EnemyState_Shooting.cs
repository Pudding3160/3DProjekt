using System;
using Unity.Mathematics;
using UnityEngine;

public class EnemyState_Shooting : IState
{

    public EnemyReferences refs;
    public GameObject projectile;
    public EnemyState_Shooting(EnemyReferences refs)
    {
        this.refs = refs;
    }
   

    public Color GizmoColor()
    {
        return Color.paleVioletRed;
    }

    public void OnEnter()
    {
        ShootPlayer();
    }

    private void ShootPlayer()
    {
        Vector3 dir = (refs.player.position-refs.enemyTransform.position).normalized;
        GameObject bullet=UnityEngine.Object.Instantiate(projectile,refs.enemyTransform.position,quaternion.identity);
        Rigidbody rb=bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = dir * 20f;
        UnityEngine.Object.Destroy(bullet, 2f);
    }

    public void OnExit()
    {
        
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
