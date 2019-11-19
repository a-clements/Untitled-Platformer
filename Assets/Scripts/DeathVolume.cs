using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVolume : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            for(int i = PlayerHealth.LivesRemaining; i > -1; i--)
            {
                TriggerInfo.GetComponent<PlayerHealth>().LoseLife();
            }

            //TriggerInfo.gameObject.SetActive(false);
        }
    }
}
