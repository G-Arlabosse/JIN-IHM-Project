using UnityEngine;

public class MovementManager : MonoBehaviour
{
  private Rigidbody2D rigidBody;

  public float s_max = 5;
  public float direction = 0;
  public Vector2 speed;
  public Vector2 jump_force = new Vector2(0, 1024);

  private void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }


  public void jump()
  {
    rigidBody.AddForce(jump_force);
  }

  private void FixedUpdate()
  {
    speed.x = Mathf.Lerp(speed.x, direction * s_max, 0.5f);
    transform.Translate(speed * Time.deltaTime);
  }
}
