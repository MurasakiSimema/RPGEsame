using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public Transform subject;

    public Tilemap map;
    private Vector3 bottomLeft;
    private Vector3 topRight;

    private float halfHeight;
    private float halfWidth;

    // Start is called before the first frame update
    void Start()
    {
        subject = PlayerController.instance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        if (map.size.x > halfWidth * 2 && map.size.y > halfHeight * 2) 
        {
            bottomLeft = map.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
            topRight = map.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
        }
        else
        {
            bottomLeft = map.localBounds.min;
            topRight = map.localBounds.max;
        }
    }

    // LateUpdate is called after frame
    void LateUpdate()
    {
        //transform.position = new Vector3(subject.position.x, subject.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(subject.position.x, bottomLeft.x, topRight.x), Mathf.Clamp(subject.position.y, bottomLeft.y, topRight.y), transform.position.z);
    }
}
