using System;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyState_Bite : IState
{
    public float biteTimer;
    public bool attackExecuted;
    public Color GizmoColor()
    {
        return Color.darkViolet;
    }

    public void OnEnter()
    {
        Debug.Log("Entering bite");
        bite();
    }

    public void OnExit()
    {
       
    }

    public void Tick()
    {
        biteTimer*=Time.deltaTime;
        if (biteTimer >= 2f)
        {
            bite();
        }
    }

    private void bite()
    {
        Debug.Log("bited");
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
