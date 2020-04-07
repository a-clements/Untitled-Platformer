using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script detects a collision with the player and increments the score.
/// </summary>

public class Treasure : MonoBehaviour
{
    [SerializeField] private int PointValue = 10;
    [SerializeField] private AudioClip TreasureSound;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.transform.tag == "Player")
        {

            ScoreManager.UpdateScores(PointValue);
            GetComponent<AudioSource>().PlayOneShot(TreasureSound);
            GetComponent<ParticleSystem>().Play();
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(TreasureSound.length);
        this.gameObject.SetActive(false);
        yield return null;
    }
}
