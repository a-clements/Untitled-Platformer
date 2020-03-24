using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a simple script that adds one heart to the player when collected. Once the player triggers it, the object is disabled.
/// </summary>

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private AudioClip HealthSound;
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
            GetComponent<AudioSource>().PlayOneShot(HealthSound);

            StartCoroutine(PlaySound());

            TriggerInfo.GetComponent<PlayerHealth>().GainHeart();
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(HealthSound.length);
        this.gameObject.SetActive(false);
        yield return null;
    }
}
