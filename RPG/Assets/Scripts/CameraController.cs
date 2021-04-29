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
    // Start is called before the first frame update
    void Start()
    {
        subject = PlayerController.instance.transform;

        bottomLeft = map.localBounds.min;
        topRight = map.localBounds.max;
    }

    // Update is called after frame
    void LateUpdate()
    {
        transform.position = new Vector3(subject.position.x, subject.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeft.x, topRight.x), Mathf.Clamp(transform.position.y, bottomLeft.y, topRight.y), transform.position.z);
    }
}
