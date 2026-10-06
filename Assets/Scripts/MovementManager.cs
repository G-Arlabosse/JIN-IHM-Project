using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private RaycastManager raycastManager;

    private float walkSpeed = 4;
    public bool running = false;
    private float runSpeed = 6;
    public Vector2 jump_force = new Vector2(0, 1024);
    public Vector2 walljump_force = new Vector2(512, 0);

    public float coyoteTime = 0.1f; // Time after leaving the ground during which a jump is still allowed
    public float inputBufferTime = 0.1f; // Time before landing during which a jump input is still allowed
    private float coyoteTimeCounter;
    private float inputBufferCounter;



    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.linearDamping = 0.2f;
        coyoteTimeCounter = 0;
        inputBufferCounter = 0;
    }

    private void Update()
    {
        inputBufferCounter -= Time.deltaTime;
        // Update coyote time counter
        if (raycastManager.CastDown())
        {
            coyoteTimeCounter = coyoteTime;
            if (inputBufferCounter > 0 && rigidBody.linearVelocityY <= 0)
            {
                TryJump();
                inputBufferCounter = 0;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void TryJump()
    {
        if (!raycastManager.CastDown())
            inputBufferCounter = inputBufferTime;

        bool hitLeft = raycastManager.CastLeft();
        bool hitRight = raycastManager.CastRight();
        if (coyoteTimeCounter > 0 && rigidBody.linearVelocityY <= 0)
            Jump();
        if (hitLeft)
            WallJump(1f);
        if (hitRight)
            WallJump(-1f);

    }

    private void Jump()
    {
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForce(jump_force);
    }

    private void WallJump(float direction)
    {
        // Set the velocity to zero before applying the wall jump force
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForce(jump_force + walljump_force * direction);
    }

    public void Stop()
    {
        rigidBody.linearVelocity = Vector2.zero;
    }

    public void ApplyForce(Vector2 force)
    {
        rigidBody.AddForce(force);
    }

    public void ApplyVelocity(Vector2 Velocity)
    {
        rigidBody.linearVelocity = Velocity;
    }

    public void ApplyVelocity(float x, float y) { ApplyVelocity(new Vector2(x, y)); }

    public void ApplyVelocityX(float x)
    {
        ApplyVelocity(x, rigidBody.linearVelocity.y);
    }

    public void ApplyVelocityY(float y)
    {
        ApplyVelocity(rigidBody.linearVelocity.x, y);
    }

    public Vector2 getDirection()
    {
        Vector2 direction = new Vector2(rigidBody.linearVelocity.x, rigidBody.linearVelocity.y);

        if (direction.x != 0)
            direction.x = Mathf.Sign(direction.x);

        if (direction.y != 0)
            direction.y = Mathf.Sign(direction.y);

        return direction;
    }
}
