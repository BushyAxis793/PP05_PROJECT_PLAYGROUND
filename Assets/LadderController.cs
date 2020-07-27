using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LadderController : MonoBehaviour
{
    PlayerController playerController;
    bool canClimb = false;
    float speed = 10f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            print("You touched the Ladder");
            canClimb = true;
        }
    }
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {

            canClimb = false;
        }
    }

    private void Update()
    {
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.O))
            {
                print("You clicked O");
                //playerController.transform.Translate(Vector3.up * Time.deltaTime * speed);
                playerController.transform.position += Vector3.up * Time.deltaTime * speed;
            }

            if (Input.GetKey(KeyCode.L))
            {
                playerController.transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }
    }
}
