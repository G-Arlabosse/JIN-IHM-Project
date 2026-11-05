using System.Collections;
using TMPro;
using UnityEngine;

public class DodgeManager : MonoBehaviour
{

    private MovementManager movementManager;
    [SerializeField] private DashManager dashManager;

    [SerializeField] private float dodgeDuration = 0.2f;
    [SerializeField] private float dashDuration = 0.4f;

    public int max_dashAndDodge = 1;
    private int nmb_dashAndDodge = 1;
    [SerializeField] private TMP_Text dashAndDodgeText;

    private void Start()
    { movementManager = GetComponent<MovementManager>(); dashManager = GetComponent<DashManager>();}


    public void Dodge(Vector2 direction)
    {
        if (nmb_dashAndDodge == 0) return;
        RemoveDashAndDodge();

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
        RemoveDashAndDodge();

        float new_duration = dashManager.Dash(dashDuration, direction);
        StartCoroutine(movementManager.Freeze(new_duration));
        
    }

    private void RemoveDashAndDodge()
    {
        if (nmb_dashAndDodge > 0)
        {
            nmb_dashAndDodge -= 1;
        }
        if (dashAndDodgeText != null)
        {
            dashAndDodgeText.text = "x"+nmb_dashAndDodge.ToString();
        }
    }

    public void ResetDashAndDodge()
    {
        nmb_dashAndDodge = max_dashAndDodge;
        if (dashAndDodgeText != null)
        {
            dashAndDodgeText.text = "x" + nmb_dashAndDodge.ToString();
        }
    }
}
