using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    [SerializeField] private Vector2 respawnPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            MovementManager player = collision.gameObject.GetComponent<MovementManager>();
            if (player != null)
            {
                player.dead();
                StartCoroutine(Respawn(player));
            }
        }
    }

    private IEnumerator Respawn(MovementManager player)
    {
        player.Die();
        yield return new WaitForSeconds(0.4f);
        player.Respawn(respawnPosition);
    }
}
