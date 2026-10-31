using System;
using System.Collections;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashManager : MonoBehaviour
{
    [SerializeField] private RaycastManager raycastManager;

    [SerializeField] private float SPEED = 8;

    private bool dashed = false;
    private Vector2 direction;
    
    public float Dash(float duration, Vector2 direction)
    {
        float new_duration = raycastManager.CastInADirection(direction, SPEED * duration);
        new_duration /= SPEED;

        StartCoroutine(DashCoroutine(new_duration , direction));

        return new_duration;
    }

    public IEnumerator DashCoroutine(float duration, Vector2 direction)
    {
        dashed = true;
        this.direction = direction.normalized;
        
        yield return new WaitForSeconds(duration);

        dashed = false;
        this.direction = Vector2.zero;
        
    }

    private void FixedUpdate()
    {
        if (dashed)
        { transform.Translate(direction * SPEED * Time.deltaTime); }
    }
}
