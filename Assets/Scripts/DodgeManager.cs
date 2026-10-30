using System;
using System.Collections;
using UnityEngine;

public class DodgeManager : MonoBehaviour
{

    private MovementManager movementManager;

    [SerializeField] private float dashForce = 512;
    [SerializeField] private float dodgeDuration = 0.2f;
    [SerializeField] private float dashDuration = 0.4f;

    [HideInInspector]
    public int nmb_dashAndDodge = 1;

    private void Start()
    { movementManager = GetComponent<MovementManager>(); }


    public void Dodge(Vector2 direction)
    {
        if (nmb_dashAndDodge == 0) return;
        nmb_dashAndDodge -= 1;

        StartCoroutine(DodgeAnimation());
        StartCoroutine(movementManager.Freeze(dodgeDuration));
        movementManager.Stop();
    }

    private IEnumerator DodgeAnimation()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.material.color;
            color.a = 0.5f;
            spriteRenderer.material.color = color;

            yield return new WaitForSeconds(dodgeDuration);

            color.a = 1f;
            spriteRenderer.material.color = color;
        }
    }

    public void Dash(Vector2 direction)
    {
        if (nmb_dashAndDodge == 0) return;
        nmb_dashAndDodge -= 1;

        StartCoroutine(movementManager.Freeze(0));

        movementManager.ApplyForce(direction.normalized * dashForce);
    }
}
