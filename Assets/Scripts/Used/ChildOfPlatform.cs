using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script attaches the player to the platform when the trigger volume is entered, and detaches the player when the player leaves the
/// trigger volume.
/// </summary>

public class ChildOfPlatform : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        this.transform.position = new Vector3(this.transform.parent.position.x,
            this.transform.parent.position.y - 0.1f, this.transform.parent.position.z);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            TriggerInfo.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            TriggerInfo.transform.parent = null;
        }
    }
}
