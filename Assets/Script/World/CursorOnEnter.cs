using UnityEngine;

public class CursorOnEnter : MonoBehaviour
{
    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
