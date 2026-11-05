using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    [SerializeField] private Vector2 respawnPosition;
    private float deathTimer = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            MovementManager player = collision.gameObject.GetComponent<MovementManager>();
            if (player != null)
            {
                StartCoroutine(Respawn(player));
            }
        }
    }

    private IEnumerator Respawn(MovementManager player)
    {
        player.Die(deathTimer);
        yield return new WaitForSeconds(deathTimer);
        player.Respawn(respawnPosition);
    }
}
