using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    GameObject player;
    bool canClimb = false;
    float speed = 10f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            print("You touched the Ladder");
            canClimb = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canClimb = false;
    }

    private void Update()
    {
        if (canClimb)
        {
            if (Input.GetKey(KeyCode.O))
            {
                player.transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
            }
            if (Input.GetKey(KeyCode.L))
            {
                player.transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
            }
        }
    }
}
