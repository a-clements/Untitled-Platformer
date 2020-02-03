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
    [SerializeField] private Vector2 ColliderSize;
    [SerializeField] private Vector2 ColliderOffset;

    private Transform ThisTransform;
    private SpriteRenderer Sprite;
    private Vector3 NextPosition;
    private int PointNumber = 0;
    private bool IsMoving = true;
    private Animator EnemyAnimator;
    private int Action;
    private int PreviousAction;
    private Rigidbody2D RigidBody;
    private BoxCollider2D BoxCollider;

    void Start()
    {
        ThisTransform = transform;
        Sprite = GetComponent<SpriteRenderer>();
        EnemyAnimator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();

        BoxCollider.offset = ColliderOffset;
        BoxCollider.size = ColliderSize;

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

        if (!Sprite.flipX)
        {
            RigidBody.velocity = ((Vector2.up * JumpHeight) + (Vector2.left * JumpDistance)) * Time.fixedDeltaTime;
        }
        else
        {
            RigidBody.velocity = ((Vector2.up * JumpHeight) + (Vector2.right * JumpDistance)) * Time.fixedDeltaTime;
        }

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

        if (ThisTransform.position.x <= Points[0].x || ThisTransform.position.x >= Points[Points.Length - 1].x)
        {
            PointNumber++;

            if (PointNumber == Points.Length)
            {
                PointNumber = 0;
            }

            NextPosition = Points[PointNumber];

            Sprite.flipX = !Sprite.flipX;

            ColliderOffset.x *= -1;

            BoxCollider.offset = ColliderOffset;
            BoxCollider.size = ColliderSize;
        }

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

    IEnumerator Walk()
    {
        yield return null;

        IsMoving = true;

        EnemyAnimator.SetBool("IsWalking",true);

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

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

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

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

                ColliderOffset.x *= -1;

                BoxCollider.offset = ColliderOffset;
                BoxCollider.size = ColliderSize;
            }

            ThisTransform.position = Vector3.MoveTowards(ThisTransform.position, NextPosition, Speed * Time.deltaTime);
        }
    }
}
