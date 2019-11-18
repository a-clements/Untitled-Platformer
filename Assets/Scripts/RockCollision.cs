using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    public float Distance;
    private void OnEnable()
    {
        //called second
    }

    private void Awake()
    {
        //called first
    }

    private void Start()
    {
        //called third
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
            TriggerInfo.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Ground")
        {
            this.gameObject.SetActive(false);
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
