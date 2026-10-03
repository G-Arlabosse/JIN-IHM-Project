using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private RaycastManager raycastManager;

    public float s_max = 5;
    public Vector2 velocity;
    public float direction = 0;
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
            Debug.Log("Grounded");
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
        velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (running)
          velocity.x = Mathf.Lerp(velocity.x, direction * runSpeed, 0.5f);
        else
          velocity.x = Mathf.Lerp(velocity.x, direction * walkSpeed, 0.5f);
        
        transform.Translate(velocity * Time.deltaTime);
    }
}
