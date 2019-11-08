using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Transform ThisTransform;
    private Rigidbody2D RigidBody;
    [SerializeField] private string Direction;
    [SerializeField] private float Speed;
    [SerializeField] private bool XAxis;
    [SerializeField] private float MaxXDistance = 0.0f;
    [SerializeField] private float MinXDistance = 0.0f;
    [SerializeField] private bool YAxis;
    [SerializeField] private float MaxYDistance = 0.0f;
    [SerializeField] private float MinYDistance = 0.0f;



    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground")
        {
            Speed *= -1.0f;
        }
    }

    void Update()
    {
        if (XAxis == true)
        {
            if (ThisTransform.localPosition.x > MaxXDistance || ThisTransform.localPosition.x < MinXDistance)
            {
                Speed *= -1.0f;
            }

        }

        if (YAxis == true)
        {
            if (ThisTransform.localPosition.y > MaxYDistance || ThisTransform.localPosition.y < MinYDistance)
            {
                Speed *= -1.0f;
            }
        }

        switch (Direction)
        {
            case "Up":
                ThisTransform.Translate(new Vector3(0, Speed, 0) * Time.deltaTime);
                break;
            case "Left":
                ThisTransform.Translate(new Vector3(Speed, 0, 0) * Time.deltaTime);
                break;
        }
    }
}
