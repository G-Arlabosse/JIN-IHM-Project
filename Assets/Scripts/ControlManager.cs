using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class ControlManager : MonoBehaviour
{
  private MovementManager movementManager;

  private void Start()
  {
    movementManager = GetComponent<MovementManager>();
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
    movementManager.jump();
  }

  
}
