using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float moveSpeed = 6f;

    float gravityForce = 9.87f;
    float verticalSpeed = 0f;

    public Transform camera;
    public float mouseSens = 2f;
    public float upLimit = -50f;
    public float downLimit = 50f;

    public Animator anim;

    const string WALK_ANIM = "isWalking";

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");

        transform.Rotate(0, horizontalRotation * mouseSens, 0);
        camera.Rotate(-verticalRotation * mouseSens, 0, 0);

        Vector3 baseRotation = camera.localEulerAngles;
        if (baseRotation.x > 180) baseRotation.x -= 360;
        baseRotation.x = Mathf.Clamp(baseRotation.x, upLimit, downLimit);
        camera.localRotation = Quaternion.Euler(baseRotation);
    }

    private void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        if (characterController.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravityForce * Time.deltaTime;

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        Vector3 moveVector = transform.forward * verticalAxis + transform.right * horizontalAxis;
        characterController.Move(moveSpeed * Time.deltaTime * moveVector + gravityMove * Time.deltaTime);

        anim.SetBool(WALK_ANIM, verticalAxis != 0 || horizontalAxis != 0);
    }
}

