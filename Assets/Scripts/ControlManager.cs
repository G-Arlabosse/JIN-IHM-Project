using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
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
      movementManager.direction = 0;
      return;
    }
    movementManager.direction = Mathf.Sign(((Vector2) value.Get()).x);
  }

  private void OnJump()
  {
    movementManager.Jump();
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
      movementManager.running = false;
    }

    else if (buttonPress < 0.4)
    {
      movementManager.running = false;
    }

  }
}
