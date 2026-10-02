using UnityEngine;

public class DodgeManager : MonoBehaviour
{

  private MovementManager movementManager;

  private void Start()
  {
    movementManager = GetComponent<MovementManager>();
  }

  public void Dodge()
  {
    movementManager.Stop();
  }
}
