using UnityEngine;

public class DodgeManager : MonoBehaviour
{

  private MovementManager movementManager;

  [SerializeField] private GravityManager gravityManager;

  [SerializeField] private float dashForce = 32;

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
    print("dash");
    print(direction);
    gravityManager.removeGravity();
    //movementManager.accelerationPourcentage = 0;
    movementManager.InstantChange(direction.normalized * dashForce);
    
  }
}
