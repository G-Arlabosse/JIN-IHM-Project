using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private MovementManager movementManager;
    
    [SerializeField] private float defaultGravity = 2.0f;

    private float gravityDelay = 0.3f;
    private float gravityTimer = 0.5f;

    public void removeGravity()
    {
        print("removal gravity");
        rigidBody2D.gravityScale = 0;
        gravityDelay = 0;
    }

    private void Update()
    {
        if (gravityTimer == gravityDelay)
        {
            if (rigidBody2D.gravityScale < defaultGravity)
            {
                rigidBody2D.gravityScale = defaultGravity;
            }
        }
        else
        {
           gravityTimer += Time.deltaTime;
           gravityTimer = Mathf.Min(gravityTimer, gravityDelay);
        }
    }
}
