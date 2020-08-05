using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform transform;
    public float x = 0f;
    public float z = 1f;

    private void Update()
    {
        if ((Input.GetAxis("Mouse ScrollWheel") > 0) && (x < 2))
        {
            x = x + z;
            transform.localPosition = new Vector3(0, 0, x);
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") < 0) && (x > -10))
        {
            x = x - z;
            transform.localPosition = new Vector3(0, 0, x);
        }
    }
}
