using UnityEngine;

public class TutorialEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject TutorialEnd;
    [SerializeField] private PlayerControl ctrls;

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        TutorialEnd.SetActive(true);

        ctrls.CursorUnlock();
    }
}

