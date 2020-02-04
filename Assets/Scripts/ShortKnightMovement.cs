using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortKnightMovement : MonoBehaviour
{
    [Tooltip("How fast is the Knight.")]
    [SerializeField] private float Speed = 1;
    [Tooltip("An array of NavPoints at which the enemy will stop.")]
    [SerializeField] private Vector3[] NavPoints;
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
    private CapsuleCollider2D CapsuleCollider;

    void Start()
    {
        ThisTransform = transform;
        Sprite = GetComponent<SpriteRenderer>();
        EnemyAnimator = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();

        CapsuleCollider.offset = ColliderOffset;
        CapsuleCollider.size = ColliderSize;

        Action = Random.Range(0, 2);
        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Walk());
                break;
            case 1:
                StartCoroutine(Attack());
                break;
        }

        if (IsMoving == true)
        {
            NextPosition = NavPoints[PointNumber];
        }
    }

    IEnumerator Walk()
    {
        yield return null;

        IsMoving = true;

        EnemyAnimator.SetBool("IsWalking", true);

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

        EnemyAnimator.SetBool("IsWalking", false);

        while (Action == PreviousAction)
        {
            Action = Random.Range(0, 2);
        }

        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Walk());
                break;
            case 1:
                StartCoroutine(Attack());
                break;
        }
    }

    IEnumerator Attack()
    {
        yield return null;

        IsMoving = false;

        EnemyAnimator.SetTrigger("IsAttacking");

        yield return new WaitForSeconds(EnemyAnimator.GetCurrentAnimatorStateInfo(0).length + 1);

        while (Action == PreviousAction)
        {
            Action = Random.Range(0, 2);
        }

        PreviousAction = Action;

        switch (Action)
        {
            case 0:
                StartCoroutine(Walk());
                break;
            case 1:
                StartCoroutine(Attack());
                break;
        }
    }

    void Update()
    {
        if (IsMoving == true)
        {
            if (ThisTransform.position.x <= NavPoints[0].x || ThisTransform.position.x >= NavPoints[NavPoints.Length - 1].x)
            {
                PointNumber++;

                if (PointNumber == NavPoints.Length)
                {
                    PointNumber = 0;
                }

                NextPosition = NavPoints[PointNumber];

                Sprite.flipX = !Sprite.flipX;

                ColliderOffset.x *= -1;

                CapsuleCollider.offset = ColliderOffset;
                CapsuleCollider.size = ColliderSize;
            }

            ThisTransform.position = Vector3.MoveTowards(ThisTransform.position, NextPosition, Speed * Time.deltaTime);
        }
    }
}
