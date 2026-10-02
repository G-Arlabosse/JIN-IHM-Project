using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private RaycastManager raycastManager;

    public float s_max = 5;
    public float direction = 0;
    public Vector2 speed;
    public Vector2 jump_force = new Vector2(0, 1024);
    public Vector2 walljump_force = new Vector2(512, 0);



    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void TryJump()
    {
        bool hitLeft = raycastManager.CastLeft();
        bool hitRight = raycastManager.CastRight();
        bool grounded = raycastManager.CastDown();
        if (grounded)
            Jump();
        if (hitLeft)
            WallJump(1f);
        if (hitRight)
            WallJump(-1f);

    }

    private void Jump()
    {
        rigidBody.AddForce(jump_force);
    }

    private void WallJump(float direction)
    {
        rigidBody.AddForce(jump_force + walljump_force * direction);
    }

    private void FixedUpdate()
    {
        speed.x = Mathf.Lerp(speed.x, direction * s_max, 0.5f);
        transform.Translate(speed * Time.deltaTime);
    }
}
