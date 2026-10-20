using System.Collections;
using UnityEngine;

public class DodgeManager : MonoBehaviour
{

    private MovementManager movementManager;

    [SerializeField] private float dashForce = 512;

    private void Start()
    {
        movementManager = GetComponent<MovementManager>();
    }

    public void Dodge()
    {
        Vector2 direction = movementManager.directionPlayer;

        if (movementManager.running && direction != Vector2.zero)
            Dash(direction);
        else
        {
            movementManager.Stop();
            StartCoroutine(DodgeAnimation());
        } 
    }

    private IEnumerator DodgeAnimation()
    {
        Debug.Log("Dodge animation started");

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.material.color;
            color.a = 0.5f;
            spriteRenderer.material.color = color;

            yield return new WaitForSeconds(0.5f);

            color.a = 1f;
            spriteRenderer.material.color = color;
        }

        Debug.Log("Dodge animation ended");
    }

    public void Dash(Vector2 direction)
    {
        movementManager.Stop();
        movementManager.directionEffective.x = direction.x;
        movementManager.ApplyForce(direction.normalized * dashForce);
    }
}
