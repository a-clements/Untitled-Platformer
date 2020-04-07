using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a very simple script that kills the player if entered.
/// </summary>

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
            for(int i = PlayerHealth.HeartsRemaining; i > -1; i--)
            {
                TriggerInfo.GetComponent<PlayerHealth>().LoseHeart();
            }
        }
    }
}
