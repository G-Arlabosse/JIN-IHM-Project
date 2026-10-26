using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementManager : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private RaycastManager raycastManager;


    [Header("Parameters")]
    [SerializeField] private float walkSpeed = 8;
    [SerializeField] private float runSpeed = 12;
    [SerializeField] private float lerpFactor = 8;
    [SerializeField] public float jump_force = 1024;
    [SerializeField] private float walljump_force = 512;
    [SerializeField] private float coyoteTime = 0.1f; // Time after leaving the ground during which a jump is still allowed
    [SerializeField] private float inputBufferTime = 0.1f; // Time before landing during which a jump input is still allowed
    
    [Space(25)]

    [HideInInspector]
    public bool running = false;
    private float coyoteTimeCounter = 0;
    private float inputBufferCounter = 0;

    [HideInInspector]
    public Vector2 directionEffective = Vector2.zero;
    [HideInInspector]
    public Vector2 directionPlayer = Vector2.zero;
    [HideInInspector]
    public float accelerationPourcentage = 1;
    [HideInInspector]
    public float currentSpeed = 8;

    private Vector2 previousDirection = Vector2.zero;
    private float timeSinceDirectionChange = 0f;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //rigidBody.linearDamping = 0.2f;
        coyoteTimeCounter = 0;
        inputBufferCounter = 0;
    }

    private void FixedUpdate()
    {
        inputBufferCounter -= Time.deltaTime;

        bool somethingDown = raycastManager.CastDown();
        bool somethingLeft = raycastManager.CastLeft();
        bool somethingRight = raycastManager.CastRight();
        
        if (somethingDown || (directionPlayer.x > 0 && !somethingRight) || (directionPlayer.x < 0 && !somethingLeft))
            directionEffective.x = directionPlayer.x;


        // Update coyote time counter
        if (somethingDown)
        {
            accelerationPourcentage = 1;

            if (currentSpeed != runSpeed && running)
            {
                currentSpeed = runSpeed;
            }

            coyoteTimeCounter = coyoteTime;
            if (inputBufferCounter > 0)
            {
                TryJump();
                inputBufferCounter = 0;
            }
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        timeSinceDirectionChange += lerpFactor*Time.fixedDeltaTime;


        rigidBody.linearVelocityX = Mathf.Lerp(previousDirection.x, directionEffective.x * currentSpeed, timeSinceDirectionChange * accelerationPourcentage);
            

        if (accelerationPourcentage < 1.0f)
        {
            accelerationPourcentage += Time.fixedDeltaTime;
            accelerationPourcentage = Mathf.Min(accelerationPourcentage, 1.0f);
        }
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
            Jump();


    }

    private void WallJump(float direction)
    {
        // Set the velocity to zero before applying the wall jump force
        rigidBody.linearVelocityY = 0;
        rigidBody.AddForce(new Vector2(0, jump_force));
        rigidBody.linearVelocityX = direction * currentSpeed;
        accelerationPourcentage = 0.01f;

        if (raycastManager.CastLeft())
            ChangeDirectionX(1);
        else
            ChangeDirectionX(-1);
    }

    public void InstantChange(Vector2 direction)
    {
        rigidBody.linearVelocity = direction;
        previousDirection.x = Mathf.Sign(direction.x);
        directionEffective.x = previousDirection.x;

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
        if (direction != Vector2.zero && rigidBody.linearVelocityY < 0)
            return;

        directionEffective = direction;
        previousDirection = rigidBody.linearVelocity;
        timeSinceDirectionChange = 0f;
    }
    private void ChangeDirectionX(float directionX)
    {
        ChangeDirection(new Vector2(directionX, directionEffective.y));
    }


    [SerializeField] private float sensitivityJoyStick = 0.8f;
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
