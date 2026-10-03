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

  private float sensitivityJoyStick = 0.4f;

  private void Start()
  {
    movementManager = GetComponent<MovementManager>();
    dodgeManager = GetComponent<DodgeManager>();
  }

  private void OnMove(InputValue value)
  {
    if (value.Get() == null) 
    {
      movementManager.direction = Vector2.zero;
      return;
    }

    Vector2 directionNT = ((Vector2) value.Get()).normalized;

    if (Mathf.Abs(directionNT.x) > sensitivityJoyStick)
    { movementManager.direction.x = Mathf.Sign(directionNT.x); }
    else
    { movementManager.direction.x = 0; }
      
    if (Mathf.Abs(directionNT.y) > sensitivityJoyStick)
    { movementManager.direction.y = Mathf.Sign(directionNT.y); }
    else
    { movementManager.direction.y = 0; }
      
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
}
