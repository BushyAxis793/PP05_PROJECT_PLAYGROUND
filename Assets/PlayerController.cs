using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerController : MonoBehaviour
{


    [SerializeField] float speed = 5f;
    [SerializeField] float smoothTime = .1f;
    [SerializeField] Transform cam;

    float smoothVelocity;

    CharacterController charController;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {

        float vAxis = Input.GetAxisRaw("Vertical");
        float hAxis = Input.GetAxisRaw("Horizontal");

        Vector3 direction = new Vector3(hAxis, 0f, vAxis).normalized;

        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, smoothTime);
            // transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.LookAt(moveDirection);
            charController.Move(moveDirection.normalized * speed * Time.deltaTime);

        }
    }


}
