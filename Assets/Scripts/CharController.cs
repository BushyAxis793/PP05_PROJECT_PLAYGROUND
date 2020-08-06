using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CharController : MonoBehaviour
{

    public Vector3 move = new Vector3(0, 0, 0);
    public Vector3 rotation = new Vector3(0, 0, 0);
    public Vector3 mouseMove = new Vector3(0, 0, 0);
    public Vector3 cameraHolder1Move = new Vector3(0, 0, 0);
    public Vector3 cameraHolder2Move = new Vector3(0, 0, 0);

    public float camXSpeed;
    public float camYSpeed;
    public float speedRotation = 1.0f;
    public float speed = 1.0f;
    public float jumpForce = 1f;
    public float speedRun = 4f;
    public float speedWalk = 2f;

    public Rigidbody rigidbody;
    public LayerMask groundLayer;
    public CapsuleCollider col;
    public Transform cameraH1;
    public Transform cameraH2;
    public Transform cameraH1Zero;
    public Transform camerah2Zero;

    Animator anim;
    LadderController ladder;

    const string WALK_ANIM = "isWalking";
    const string RUN_ANIM = "isRunning";
    const string CLIMB_ANIM = "isClimbing";

    void Start()
    {
        anim = GetComponent<Animator>();
        ladder = FindObjectOfType<LadderController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedRun;
            anim.SetBool(RUN_ANIM, true);
        }
        else
        {
            speed = speedWalk;
            anim.SetBool(RUN_ANIM, false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) && !ladder.CanClimb())
        {
            MoveMouse();
            MovePPM();
        }
        else if (Input.GetMouseButton(0))
        {
            MoveCamera();
        }
        else
        {
            Cursor.visible = true;
            HeroMoveNormal();
            if (Input.GetAxis("Horizontal") != 0 || (Input.GetAxis("Vertical") != 0)) CameraBack();

        }

        Climb();
    }

    private void MovePPM()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        WalkHandler(h, v);

        if ((v > 0) && (h > 0)) move = transform.forward + transform.right;
        else if ((v > 0) && (h < 0)) move = transform.forward + transform.right * -1f;
        else if ((v < 0) && (h > 0)) move = transform.forward * -1f + transform.right;
        else if ((v < 0) && (h < 0)) move = transform.forward * -1f + transform.right * -1f;
        else if (v > 0) move = transform.forward;
        else if (v < 0) move = transform.forward * -.8f;
        else if (h > 0) move = transform.right;
        else if (h < 0) move = transform.right * -1f;
        else move = Vector3.zero;

        rigidbody.MovePosition(transform.localPosition + move * Time.deltaTime * speed);

    }

    private void MoveMouse()
    {
        Cursor.visible = false;
        mouseMove.y = Input.GetAxis("Mouse X");
        Quaternion mouseRot = Quaternion.Euler(mouseMove * speed);
        rigidbody.MoveRotation(rigidbody.rotation * mouseRot);

        cameraHolder2Move.x = Input.GetAxis("Mouse Y") * -1f;
        cameraH2.Rotate(cameraHolder2Move, camYSpeed);
    }

    void HeroMoveNormal()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        WalkHandler(h, v);

        if (v > 0) move = transform.forward;
        else if (v < 0) move = transform.forward * -1.0f;
        else move = Vector3.zero;

        if (h != 0)
        {
            rotation.y = h;
            Quaternion moveRot = Quaternion.Euler(rotation * speedRotation);
            rigidbody.MoveRotation(rigidbody.rotation * moveRot);
        }

        rigidbody.MovePosition(transform.localPosition + move * speed * Time.deltaTime);
    }

    private void WalkHandler(float h, float v)
    {
        if (h > 0 || v > 0 || h < 0 || v < 0) anim.SetBool(WALK_ANIM, true);
        else anim.SetBool(WALK_ANIM, false);
    }

    private void MoveCamera()
    {
        Cursor.visible = false;
        cameraHolder1Move.y = Input.GetAxis("Mouse X");
        cameraH1.Rotate(cameraHolder1Move, camXSpeed);

        cameraHolder2Move.x = Input.GetAxis("Mouse Y") * -1f;
        cameraH2.Rotate(cameraHolder2Move, camYSpeed);

    }
    void CameraBack()
    {
        cameraH1.rotation = Quaternion.Lerp(cameraH1.rotation, cameraH1Zero.rotation, Time.deltaTime * speed);
        cameraH2.rotation = Quaternion.Lerp(cameraH2.rotation, cameraH1Zero.rotation, Time.deltaTime * speed);
    }
    bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayer);
    }

    void Climb()
    {
        if (ladder.CanClimb())
        {
            anim.SetBool(WALK_ANIM, false);
            anim.SetBool(CLIMB_ANIM, true);

            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
            }
        }
        // anim.SetBool(CLIMB_ANIM, false);

        //anim.SetBool(WALK_ANIM, true);
        ///wylaczyc animacje climb
    }
}
