using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiWallClimb : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            //TriggerInfo.GetComponent<Player>().IsGrounded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            //TriggerInfo.GetComponent<Player>().IsGrounded = true;
        }
    }
}
