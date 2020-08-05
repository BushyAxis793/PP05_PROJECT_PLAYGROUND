using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LadderController : MonoBehaviour
{
    bool canClimb = false;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            canClimb = true;
            otherCollider.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.tag == "Player")
        {
            canClimb = false;
            otherCollider.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public bool CanClimb()
    {
        return canClimb;
    }
}
