using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    private float colliderWidth;
    private float colliderHeight;
    [SerializeField] private LayerMask walls;

    private Vector2 leftCastOrigin; // Top left point of the collider
    private Vector2 rightCastOrigin; // Top right point of the collider
    private Vector2 bottomCastOrigin; // Bottom left point of the collider
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colliderWidth = _collider.size.x;
        colliderHeight = _collider.size.y;

    }

    private void UpdateOrigins()
    {
        leftCastOrigin = (Vector2)transform.position - Vector2.right * colliderWidth / 2 + Vector2.up * colliderHeight / 2;
        rightCastOrigin = (Vector2)transform.position + Vector2.right * colliderWidth / 2 + Vector2.up * colliderHeight / 2;
        bottomCastOrigin = (Vector2)transform.position - Vector2.up * colliderHeight / 2;
    }

    public bool CastLeft()
    {
        UpdateOrigins();
        RaycastHit2D hit = Physics2D.BoxCast(leftCastOrigin, new Vector2(0.1f, colliderHeight * 0.5f), 0, Vector2.down, 0.1f, walls);
        return hit.collider != null;
    }

    public bool CastRight()
    {
        UpdateOrigins();
        RaycastHit2D hit = Physics2D.BoxCast(rightCastOrigin, new Vector2(0.1f, colliderHeight*0.5f), 0, Vector2.down, 0.1f, walls);
        return hit.collider != null;
    }

    public bool CastDown()
    {
        UpdateOrigins();
        RaycastHit2D hit = Physics2D.BoxCast(bottomCastOrigin, new Vector2(colliderWidth*0.95f, 0.1f), 0, Vector2.down, 0.1f, walls);
        return hit.collider != null;
    }
}
