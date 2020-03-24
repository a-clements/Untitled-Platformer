using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script detects collisions between the rock object and an enemy or the player. The designer can define the distance the rock travels
/// when thrown by the player. This script also allows the designer to define how far the player is knocked back when the player is hit by a 
/// rock. This script will also despawn the rock if it travels too far away from the spawn position, or if it hits an object with the Ground
/// tag. This script will call the Dead function on the EnemyDeath script.
/// </summary>

public class EnemyRockCollision : MonoBehaviour
{
    [SerializeField] private float Distance = 5.0f;
    [SerializeField] private AudioClip RockSound;

    private Transform ThisTransform;

    private void OnEnable()
    {
        //called second
    }

    private void Awake()
    {
        //called first
        ThisTransform = this.transform;
    }

    private void Start()
    {
        //called third
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.transform.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
            TriggerInfo.transform.GetComponent<EnemyDeath>().Dead();
        }

        if (TriggerInfo.transform.tag == "Ground")
        {
            this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<AudioSource>().PlayOneShot(RockSound);
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(RockSound.length);
        this.gameObject.SetActive(false);
        yield return null;
    }

    private void Update()
    {
        if (this.transform.position.z > Distance)
        {
            this.gameObject.SetActive(false);
        }
    }
}