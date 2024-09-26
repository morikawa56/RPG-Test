using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 5;
    private Vector3 offset;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tag.PLAYER).transform;
        offset = transform.position - playerTransform.position;
    }

    void Update()
    {
        transform.position = playerTransform.position + offset;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.fieldOfView += scroll * zoomSpeed;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 37, 78);
    }
}
