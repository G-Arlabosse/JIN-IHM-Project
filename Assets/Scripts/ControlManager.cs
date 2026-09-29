using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class ControlManager : MonoBehaviour
{
    
  private float s_max = 5;
  private float direction = 0;
  private Vector2 speed;
  private Vector2 jump_force = new Vector2(0, 1024);

  private Rigidbody2D rigidBody;

  private void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
  }

  private void OnMove(InputValue value)
  {
    if (value.Get() == null) 
    {
      direction = 0;
      return;
    }

    Vector2 horizontalSpeed = (Vector2) value.Get();

    direction = Mathf.Sign(horizontalSpeed.x);
  }

  private void OnJump()
  {
    rigidBody.AddForce(jump_force);
  }

  private void FixedUpdate()
  {
    speed.x = Mathf.Lerp(speed.x, direction * s_max, 0.5f);
    transform.Translate(speed * Time.deltaTime);
  }
}
