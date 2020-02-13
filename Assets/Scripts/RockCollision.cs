using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    [SerializeField] private float Distance = 5.0f;
    [SerializeField] private float Knockback = 3.0f;

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
            this.gameObject.SetActive(false);
        }

        if (TriggerInfo.transform.tag == "Player")
        {
            this.gameObject.SetActive(false);

            TriggerInfo.transform.GetComponent<PlayerHealth>().LoseHeart();

            if (TriggerInfo.transform.position.x < ThisTransform.position.x)
            {
                TriggerInfo.GetComponent<Rigidbody2D>().AddForce((Vector2.left + Vector2.up) * Knockback ,ForceMode2D.Impulse);
            }

            else if(TriggerInfo.transform.position.x > ThisTransform.position.x)
            {
                TriggerInfo.GetComponent<Rigidbody2D>().AddForce((Vector2.right + Vector2.up) * Knockback, ForceMode2D.Impulse);
            }
        }
    }

    private void Update()
    {
        if(this.transform.position.z > Distance)
        {
            this.gameObject.SetActive(false);
        }
    }
}
