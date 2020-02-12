using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongKnightMove : MonoBehaviour
{
    [Tooltip("How far along the X axis can the Knight jump.")]
    [SerializeField] private float JumpDistance = 25;
    [Tooltip("How high along the Y axis can the Knight jump.")]
    [SerializeField] private float JumpHeight = 250;
    [Tooltip("How fast is the Knight.")]
    [SerializeField] private float Speed = 1;
    [Tooltip("An array of NavPoints at which the enemy will stop.")]
    [SerializeField] private Vector3[] NavPoints;
    [SerializeField] private Vector2 ColliderSize;

    private Transform ThisTransform;
    private SpriteRenderer Sprite;
    private Vector3 NextPosition;
    private int PointNumber = 0;
    private bool IsMoving = true;
    private Animator EnemyAnimator;
    private int Action;
    private int PreviousAction;
    private Rigidbody2D RigidBody;
    private CapsuleCollider2D CapsuleCollider;

    void Start()
    {
        ThisTransform = transform;
        Sprite = GetComponent<SpriteRenderer>();
        EnemyAnimator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();

        CapsuleCollider.size = ColliderSize;

        Action = Random.Range(0, 2);
        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Jump());
                break;
            case 1:
                StartCoroutine(Walk());
                break;
        }

        if (IsMoving == true)
        {
            NextPosition = NavPoints[PointNumber];
        }
    }

    void CheckPosition()
    {
        if (ThisTransform.localPosition == NavPoints[PointNumber])
        {
            PointNumber++;

            if (PointNumber == NavPoints.Length)
            {
                PointNumber = 0;
            }

            NextPosition = NavPoints[PointNumber];

            Sprite.flipX = !Sprite.flipX;
        }
    }

    IEnumerator Jump()
    {
        yield return null;

        IsMoving = false;

        EnemyAnimator.SetTrigger("IsAttacking");

        if (!Sprite.flipX)
        {
            RigidBody.velocity = ((Vector2.up * JumpHeight) + (Vector2.left * JumpDistance)) * Time.fixedDeltaTime;
        }
        else
        {
            RigidBody.velocity = ((Vector2.up * JumpHeight) + (Vector2.right * JumpDistance)) * Time.fixedDeltaTime;
        }

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

        //if (ThisTransform.position.x <= NavPoints[0].x || ThisTransform.position.x >= NavPoints[NavPoints.Length - 1].x)
        //{
        //    PointNumber++;

        //    if (PointNumber == NavPoints.Length)
        //    {
        //        PointNumber = 0;
        //    }

        //    NextPosition = NavPoints[PointNumber];

        //    Sprite.flipX = !Sprite.flipX;
        //}

        CheckPosition();

        while (Action == PreviousAction)
        {
            Action = Random.Range(0, 2);
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
        }
    }

    IEnumerator Walk()
    {
        yield return null;

        IsMoving = true;

        EnemyAnimator.SetBool("IsWalking", true);

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

        EnemyAnimator.SetBool("IsWalking", false);

        CheckPosition();

        while (Action == PreviousAction)
        {
            Action = Random.Range(0, 2);
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
        }
    }

    void Update()
    {
        if (IsMoving == true)
        {
            ThisTransform.localPosition = Vector3.MoveTowards(ThisTransform.localPosition, NextPosition, Speed * Time.deltaTime);
        }
    }
}
