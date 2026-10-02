using UnityEngine;

public class DodgeManager : MonoBehaviour
{

  private MovementManager movementManager;

  private float dashForce = 512;

  private void Start()
  {
    movementManager = GetComponent<MovementManager>();
  }

  public void Dodge()
  {
    Vector2 direction = movementManager.direction;

    if (movementManager.running && direction != Vector2.zero)
      Dash(direction);
    else 
      movementManager.velocity = Vector2.zero;
  }

  public void Dash(Vector2 direction)
  {
    movementManager.ApplyForce(direction.normalized * dashForce);
  }
}
