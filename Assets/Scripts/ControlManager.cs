using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class ControlManager : MonoBehaviour
{
  private MovementManager movementManager;
  private DodgeManager dodgeManager;

  

  private void Start()
  {
    movementManager = GetComponent<MovementManager>();
    dodgeManager = GetComponent<DodgeManager>();
  }

  private void OnMove(InputValue value)
  {

    if (value.Get() == null) 
    {
      timeSinceLastMove = 0;
      movementManager.directionPlayer = Vector2.zero;
      movementManager.TryStop();
      return;
    }

    Vector2 direction = ((Vector2) value.Get()).normalized;
    if (timeSinceLastMove < 0.05)
    {
      return;
    }
    
    movementManager.TryChangeDirection(direction);
    timeSinceLastMove = 0;
  }

  private void OnJump()
  {
    movementManager.TryJump();
  }

  private void OnDodge()
  {
    dodgeManager.Dodge();
  }

  private void OnSprint(InputValue value)
  {    
    if (value.Get() == null)
    {
      movementManager.running = false;
      return;
    }

    float buttonPress = (float) value.Get();

    if (buttonPress > 0.8)
    {
      movementManager.running = true;
    }

    else if (buttonPress < 0.4)
    {
      movementManager.running = false;
    }

  }

private float timeSinceLastMove = 0;
 private void Update()
 {
  timeSinceLastMove += Time.deltaTime;
 }
}
