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

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Enemy")
        {
            this.gameObject.SetActive(false);
        }

        if (CollisionInfo.transform.tag == "Ground")
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
