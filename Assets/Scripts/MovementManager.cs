using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private RaycastManager raycastManager;

    private float walkSpeed = 4;
    public bool running = false;
    private float runSpeed = 6;
    private float currentSpeed = 4;


    public Vector2 jump_force = new Vector2(0, 1024);
    public Vector2 walljump_force = new Vector2(512, 0);

    public float coyoteTime = 0.1f; // Time after leaving the ground during which a jump is still allowed
    public float inputBufferTime = 0.1f; // Time before landing during which a jump input is still allowed
    private float coyoteTimeCounter;
    private float inputBufferCounter;

    public Vector2 directionEffective = Vector2.zero;
    public Vector2 directionPlayer = Vector2.zero;


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

            if (currentSpeed != runSpeed && running)
            {
                currentSpeed = runSpeed;
            }

            directionEffective.x = directionPlayer.x;

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


        rigidBody.linearVelocityX = Mathf.Lerp(rigidBody.linearVelocityX, directionEffective.x * currentSpeed, 0.5f);
    }

    public void ApplyForce(Vector2 force)
    {
        rigidBody.AddForce(force);
    }

    private void Jump()
    {
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForce(jump_force);
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

    private void WallJump(float direction)
    {
        // Set the velocity to zero before applying the wall jump force
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForce(jump_force + walljump_force * direction);

        if (raycastManager.CastLeft())
            directionEffective.x = 1;
        else
            directionEffective.x = -1;
    }

    public void StopX()
    {
        directionEffective.x = 0;
    }

    public void Stop()
    {
        
        directionEffective = Vector2.zero;
    }
    public void TryStop()
    {
        if (raycastManager.CastDown())
            Stop();
    }

    public void setSprint(bool value)
    {
        running = value;
        if (value && raycastManager.CastDown())
            currentSpeed = runSpeed;
        else
            currentSpeed = walkSpeed;
    }


    [SerializeField] private float sensitivityJoyStick = 0.4f;
    public void TryChangeDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > sensitivityJoyStick)
    { 
        directionPlayer.x = Mathf.Sign(direction.x); 

        if (!raycastManager.CastLeft() && !raycastManager.CastRight())
        {
            directionEffective.x = Mathf.Sign(direction.x); 
        }
            
    }
    else
    { directionPlayer.x = 0; }
      
    if (Mathf.Abs(direction.y) > sensitivityJoyStick)
    { directionPlayer.y = Mathf.Sign(direction.y); }
    else
    { directionPlayer.y = 0; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(raycastManager.CastDown());
    if (raycastManager.CastLeft() || raycastManager.CastRight())
        directionEffective = Vector2.zero;
        
    }

}
