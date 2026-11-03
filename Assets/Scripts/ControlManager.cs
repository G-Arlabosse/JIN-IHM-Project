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

    private float timeSinceLastMove = 0.0f;

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

        Vector2 direction = ((Vector2)value.Get()).normalized;

        movementManager.TryChangeDirection(direction);
        timeSinceLastMove = 0;
    }

    private void OnJump()
    {
        movementManager.TryJump();
    }

    private void OnDodge()
    {
      Vector2 direction = movementManager.directionPlayer;  

      if (movementManager.running && direction != Vector2.zero)
          dodgeManager.Dash(direction);
      else
          dodgeManager.Dodge(direction);
    } 

  private void OnSprint(InputValue value)
  {    
    if (value.Get() == null)
    {
      movementManager.setSprint(false);
      return;
    }
    movementManager.setSprint(true);
  }

 private void Update()
 {
  timeSinceLastMove += Time.deltaTime;
 }
}
