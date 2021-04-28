using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform subject;
    // Start is called before the first frame update
    void Start()
    {
        subject = PlayerController.instance.transform;
    }

    // Update is called after frame
    void LateUpdate()
    {
        transform.position = new Vector3(subject.position.x, subject.position.y, transform.position.z);
    }
}
