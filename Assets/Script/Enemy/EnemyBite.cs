using UnityEngine;

public class EnemyBite : MonoBehaviour
{

    private EnemyReferences refs;

    [Header("Settings")]
    [SerializeField] private float biteRange = 2f;
    private void Start()
    {
        refs = GetComponent<EnemyReferences>();
    }
    public bool canBite() 
    {
        float distance = Vector3.Distance(
          transform.position,
          refs.player.transform.position
      );
        return distance < biteRange; 
    }


}
