using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Controller : MonoBehaviour
{
    public float gravityForce = 1f;
    public float lerpTime = 10f;

    Vector3 moveDirection = Vector3.zero;
    Vector3 finalDirection = Vector3.zero;
    float fallForce = 0f;

    [HideInInspector]
    public CharacterController charController;

    public float distanceToGround = .1f;

    bool isGrounded;

    Collider collider;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        distanceToGround = collider.bounds.extents.y;
    }
    private void Update()
    {
        isGrounded = IsOnGround();

        moveDirection = Vector3.Lerp(moveDirection, finalDirection, Time.deltaTime * lerpTime);
        moveDirection.y = fallForce;

        charController.Move(moveDirection * Time.deltaTime);

        if (!isGrounded)
        {
            fallForce -= gravityForce * Time.deltaTime;
        }
    }
    public bool IsOnGround()
    {
        RaycastHit hit;

        if (charController.isGrounded)
        {
            return true;
        }

        if (Physics.Raycast(collider.bounds.center, -Vector3.up, out hit, distanceToGround + .1f))
        {
            return true;
        }

        return false;
    }

    public void StartMove(Vector3 direction)
    {
        finalDirection = direction;
    }
    public void StopMove()
    {
        moveDirection = Vector3.zero;
        finalDirection = Vector3.zero;
    }
    public void Jump(float jumpForce)
    {
        if (isGrounded)
        {
            fallForce = jumpForce;
        }
    }


}
