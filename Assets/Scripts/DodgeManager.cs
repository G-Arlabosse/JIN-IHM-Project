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
    Vector2 direction = movementManager.directionPlayer;

    if (movementManager.running && direction != Vector2.zero)
      Dash(direction);
    else 
      movementManager.Stop();
  }

  public void Dash(Vector2 direction)
  {
    movementManager.ApplyForce(direction.normalized * dashForce);
  }
}
