using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if(PlayerHealth.LivesRemaining < 3)
            {
                TriggerInfo.GetComponent<PlayerHealth>().GainLife();

                this.gameObject.SetActive(false);
            }
        }
    }
}
