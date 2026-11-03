using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    [SerializeField]
    private Vector2 cameraPosition;

    [SerializeField]
    private Camera cam;

    private void Start()
    {
        Assert.IsNotNull(cam);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        cam.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, -10);
    }
}
