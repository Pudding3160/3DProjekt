using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyState_Bite : IState
{
    private EnemyReferences refs;

    private float biteTimer;
    private const float biteDuration = 1.5f;
        
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
        refs.navMeshagent.ResetPath();
        refs.navMeshagent.speed = 0f;

        Debug.Log("Entering bite");

        Transform player = refs.playerObject.transform;

        Vector3 direction = refs.enemyTransform.position - player.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            player.rotation = Quaternion.LookRotation(direction);
        }

        refs.playerControl.Die();

        biteTimer = 0f;

        refs.animator.SetBool("IsBiting", true);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        biteTimer += Time.deltaTime;

        if (biteTimer >= biteDuration)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}