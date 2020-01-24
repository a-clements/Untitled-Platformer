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
    private Vector3 NextPosition;
    private int PointNumber = 0;

    [Tooltip("A boolean to show if t he platform is moving or not.")]
    [SerializeField] private bool IsMoving;
    [Tooltip("A declaration of how fast the platform is moving. Only positive numbers.")]
    [SerializeField] private float Speed;
    [Tooltip("An array of points at which the platform will stop.")]
    [SerializeField] private Vector3[] Points;



    void Start()
    {
        ThisTransform = transform;
        RigidBody = GetComponent<Rigidbody2D>();

        if(IsMoving == true)
        {
            NextPosition = Points[PointNumber];
        }
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground")
        {
            //Reverse = !Reverse;
        }
    }

    void Update()
    {
        if (IsMoving == true)
        {
            if (ThisTransform.position == Points[PointNumber])
            {
                PointNumber++;

                if (PointNumber == Points.Length)
                {
                    PointNumber = 0;
                }

                NextPosition = Points[PointNumber];
            }

            ThisTransform.position = Vector3.MoveTowards(ThisTransform.position, NextPosition, Speed * Time.deltaTime);
        }
    }
}
