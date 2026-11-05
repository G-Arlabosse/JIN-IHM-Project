using UnityEngine;

public class End : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (ScenesManager.instance != null)
            ScenesManager.instance.openMenu();
    }
}
