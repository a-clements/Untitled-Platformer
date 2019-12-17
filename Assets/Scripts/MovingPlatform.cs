using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /// <summary>
    /// This script details how a platform will move between point A and point B. If the platform does not move then leave all variables blank.
    /// </summary>

    private Transform ThisTransform;
    private Rigidbody2D RigidBody;

    [Tooltip("A decleration of which axis the platform will move on.")]
    [SerializeField] private string Direction;
    [Tooltip("A declaration on if the direction of the platform has been reversed.")]
    [SerializeField] private bool Reverse;
    [Tooltip("A declaration of how fast the platform is moving. Only positive numbers.")]
    [SerializeField] private float Speed;
    [Tooltip("A declaration of whether the platform is moving between point to point on the X axis, rather than by collision.")]
    [SerializeField] private bool XAxis;
    [Tooltip("A declaration of the maximum position of the platform while moving from point to point on the X axis")]
    [SerializeField] private float MaxXDistance = 0.0f;
    [Tooltip("A declaration of the minimum position of the platform while moving from point to point on the X axis")]
    [SerializeField] private float MinXDistance = 0.0f;
    [Tooltip("A declaration of whether the platform is moving between point to point on the Y axis, rather than by collision.")]
    [SerializeField] private bool YAxis;
    [Tooltip("A declaration of maximum position of the platform while moving from point to point on the Y axis")]
    [SerializeField] private float MaxYDistance = 0.0f;
    [Tooltip("A declaration of the minimum position of the platform while moving from point to point on the X axis")]
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
            Reverse = !Reverse;
        }
    }

    void Update()
    {
        if (XAxis == true)
        {
            if (ThisTransform.localPosition.x >= MaxXDistance || ThisTransform.localPosition.x <= MinXDistance)
            {
                Reverse = !Reverse;
            }

        }

        if (YAxis == true)
        {
            if (ThisTransform.localPosition.y >= MaxYDistance || ThisTransform.localPosition.y <= MinYDistance)
            {
                Reverse = !Reverse;
            }
        }

        switch (Direction)
        {
            case "Up":
                if(Reverse == true)
                {
                    ThisTransform.Translate(Vector2.up * Speed * Time.deltaTime);
                }

                else
                {
                    ThisTransform.Translate(Vector2.down * Speed * Time.deltaTime);
                }
                break;
            case "Left":
                if (Reverse == true)
                {
                    ThisTransform.Translate(Vector2.left * Speed * Time.deltaTime);
                }

                else
                {
                    ThisTransform.Translate(Vector2.right * Speed * Time.deltaTime);
                }
                break;
        }
    }
}
