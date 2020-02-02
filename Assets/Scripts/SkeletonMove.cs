using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMove : MonoBehaviour
{
    [Tooltip("How far along the X axis can the Skeleton jump.")]
    [SerializeField] private float JumpDistance = 10;
    [Tooltip("How high along the Y axis can the Skeleton jump.")]
    [SerializeField] private float JumpHeight = 150;
    [Tooltip("How fast is the Skeleton.")]
    [SerializeField] private float Speed = 1;
    [Tooltip("An array of points at which the enemy will stop.")]
    [SerializeField] private Vector3[] Points;

    private Transform ThisTransform;
    private SpriteRenderer Sprite;
    private Vector3 NextPosition;
    private int PointNumber = 0;
    private bool IsMoving = true;
    private Animator EnemyAnimator;
    [SerializeField] private int Action;
    private int PreviousAction;
    private Rigidbody2D RigidBody;

    void Start()
    {
        ThisTransform = transform;
        Sprite = GetComponent<SpriteRenderer>();
        EnemyAnimator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();

        Action = Random.Range(0, 3);
        PreviousAction = Action;

        switch(Action)
        {
            case 0:
                StartCoroutine(Jump());
                break;
            case 1:
                StartCoroutine(Walk());
                break;
            case 2:
                StartCoroutine(Throw());
                break;
        }

        if (IsMoving == true)
        {
            NextPosition = Points[PointNumber];
        }
    }

    IEnumerator Jump()
    {
        yield return null;

        IsMoving = false;

        EnemyAnimator.SetTrigger("IsJumping");
        
        if(!Sprite.flipX)
        {
            RigidBody.velocity = ((Vector2.up * JumpHeight) + (Vector2.left * JumpDistance)) * Time.fixedDeltaTime;
        }
        else
        {
            RigidBody.velocity = ((Vector2.up * JumpHeight) + (Vector2.right * JumpDistance)) * Time.fixedDeltaTime;
        }

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length);

        while(Action == PreviousAction)
        {
            Action = Random.Range(0, 3);
        }

        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Jump());
                break;
            case 1:
                StartCoroutine(Walk());
                break;
            case 2:
                StartCoroutine(Throw());
                break;
        }

        IsMoving = true;
    }

    IEnumerator Walk()
    {
        yield return null;


        EnemyAnimator.SetBool("IsWalking",true);

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length);

        EnemyAnimator.SetBool("IsWalking", false);

        while (Action == PreviousAction)
        {
            Action = Random.Range(0, 3);
        }

        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Jump());
                break;
            case 1:
                StartCoroutine(Walk());
                break;
            case 2:
                StartCoroutine(Throw());
                break;
        }
    }

    IEnumerator Throw()
    {
        yield return null;

        IsMoving = false;

        EnemyAnimator.SetTrigger("IsThrowing");

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length);

        while (Action == PreviousAction)
        {
            Action = Random.Range(0, 3);
        }

        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Jump());
                break;
            case 1:
                StartCoroutine(Walk());
                break;
            case 2:
                StartCoroutine(Throw());
                break;
        }

        IsMoving = true;
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

                Sprite.flipX = !Sprite.flipX;
            }

            ThisTransform.position = Vector3.MoveTowards(ThisTransform.position, NextPosition, Speed * Time.deltaTime);
        }
    }
}
