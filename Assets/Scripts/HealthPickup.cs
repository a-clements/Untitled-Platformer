using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple script that adds one heart to the player when collected. Once the player triggers it, the object is disabled.
/// </summary>

public class HealthPickup : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            TriggerInfo.GetComponent<PlayerHealth>().GainHeart();

            this.gameObject.SetActive(false);
        }
    }
}
