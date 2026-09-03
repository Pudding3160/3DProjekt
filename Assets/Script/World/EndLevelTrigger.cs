using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [SerializeField] private GameObject LevelEnd;
    [SerializeField] private PlayerControl ctrls;
    public GameStateTracker tracker;

    private void Start()
    {
        
        if (ctrls == null)
        {
            GameObject player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                ctrls = player.GetComponent<PlayerControl>();
            }
        }
        
    }
    public void LoadTrackerAfterDontDestroyOnLoad()
    {
        tracker = FindFirstObjectByType<GameStateTracker>();
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        tracker.AddAbility();
        LevelEnd.SetActive(true);

        ctrls.CursorUnlock();
    }
}
