using System;
using Unity.VisualScripting;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private RaycastManager raycastManager;


    [Header("Parameters")]
    [SerializeField] private float walkSpeed = 8;
    [SerializeField] private float runSpeed = 12;
    [SerializeField] private float currentSpeed = 8;
    [SerializeField] private float lerpFactor = 12;
    [SerializeField] public float jump_force = 1024;
    [SerializeField] private float walljump_force = 512;
    [SerializeField] private float coyoteTime = 0.1f; // Time after leaving the ground during which a jump is still allowed
    [SerializeField] private float inputBufferTime = 0.1f; // Time before landing during which a jump input is still allowed
    [Space(25)]

    public bool running = false;
    private float coyoteTimeCounter = 0;
    private float inputBufferCounter = 0;

    public Vector2 directionEffective = Vector2.zero;
    public Vector2 directionPlayer = Vector2.zero;

    private Vector2 previousDirection = Vector2.zero;
    private float timeSinceDirectionChange = 0f;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.linearDamping = 0.2f;
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

        timeSinceDirectionChange += lerpFactor*Time.deltaTime;

        if (running)
            rigidBody.linearVelocityX = Mathf.Lerp(previousDirection.x, directionEffective.x * runSpeed, timeSinceDirectionChange);
        else
            rigidBody.linearVelocityX = Mathf.Lerp(previousDirection.x, directionEffective.x * walkSpeed, timeSinceDirectionChange);
    }

    public void ApplyForce(Vector2 force)
    {
        rigidBody.AddForce(force);
    }

    private void Jump()
    {
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForceY(jump_force);
    }

    public void TryJump()
    {
        if (!raycastManager.CastDown())
            inputBufferCounter = inputBufferTime;
        
        bool hitLeft = raycastManager.CastLeft();
        bool hitRight = raycastManager.CastRight();
        
        if (hitLeft)
            WallJump(1f);
        else if (hitRight)
            WallJump(-1f);
        else if (coyoteTimeCounter > 0 && rigidBody.linearVelocityY <= 0)
            {Jump();
            print("castDown"); }

    }

    private void WallJump(float direction)
    {
        // Set the velocity to zero before applying the wall jump force
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForce(new Vector2(walljump_force, jump_force) * direction);

        if (raycastManager.CastLeft())
            ChangeDirectionX(1);
        else
            ChangeDirectionX(-1);
    }

    public void StopX()
    {
        ChangeDirectionX(0);
    }

    public void Stop()
    {

        ChangeDirection(Vector2.zero);
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
    private void ChangeDirection(Vector2 direction)
    {
        directionEffective = direction;
        previousDirection = rigidBody.linearVelocity;
        timeSinceDirectionChange = 0f;
    }
    private void ChangeDirectionX(float directionX)
    {
        ChangeDirection(new Vector2(directionX, directionEffective.y));
    }


    [SerializeField] private float sensitivityJoyStick = 0.4f;
    public void TryChangeDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > sensitivityJoyStick)
        { 
            directionPlayer.x = Mathf.Sign(direction.x); 

            if (!raycastManager.CastLeft() && !raycastManager.CastRight())
                ChangeDirectionX(Mathf.Sign(direction.x)); 
        }
        else { directionPlayer.x = 0; }
      
        if (Mathf.Abs(direction.y) > sensitivityJoyStick)
            { directionPlayer.y = Mathf.Sign(direction.y); }
        else
            { directionPlayer.y = 0; }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (raycastManager.CastLeft() || raycastManager.CastRight())
            ChangeDirection(Vector2.zero);
    }

}
