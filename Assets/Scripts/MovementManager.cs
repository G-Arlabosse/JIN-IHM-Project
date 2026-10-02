using UnityEngine;

public class MovementManager : MonoBehaviour
{
  private Rigidbody2D rigidBody;

  public Vector2 velocity;
  public Vector2 direction = Vector2.zero;
  private float walkSpeed = 4;
  public bool running = false;
  private float runSpeed = 6;

  public Vector2 jumpForce = new Vector2(0, 1024);

  private void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }


  public void Jump()
  {
    ApplyForce(jumpForce);
  }

  public void ApplyForce(Vector2 force)
  {
    rigidBody.AddForce(force);
  }

  public void Stop()
  {
    velocity = Vector2.zero;
  }

  private void FixedUpdate()
  {
    if (running)
      velocity.x = Mathf.Lerp(velocity.x, direction.x * runSpeed, 0.5f);
    else
      velocity.x = Mathf.Lerp(velocity.x, direction.x * walkSpeed, 0.5f);
    
    transform.Translate(velocity * Time.deltaTime);
  }
}
