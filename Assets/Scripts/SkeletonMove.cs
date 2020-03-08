using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines how the skeleton enemy moves. The designer can change how far the skelen jumps. How high the skeleton jumps.
/// The speed of the patrol, and how many navigation points the skeleton has. The action that the skeleton takes is random,
/// and the same action cannot be carried out twice in succession.
/// </summary>

public class SkeletonMove : MonoBehaviour
{
    [Tooltip("How far along the X axis can the Skeleton jump.")]
    [SerializeField] private float JumpDistance = 10;
    [Tooltip("How high along the Y axis can the Skeleton jump.")]
    [SerializeField] private float JumpHeight = 150;
    [Tooltip("How fast is the Skeleton.")]
    [SerializeField] private float Speed = 1;
    [Tooltip("An array of NavPoints at which the enemy will stop.")]
    [SerializeField] private Vector3[] NavPoints;
    [Tooltip("The Skeleton throw audio clip goes here.")]
    [SerializeField] private AudioClip ThrowBone;

    private Transform ThisTransform;
    private SpriteRenderer Sprite;
    private Vector3 NextPosition;
    private int PointNumber = 0;
    private bool IsMoving = true;
    private Animator EnemyAnimator;
    private int CurrentAction;
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

        CapsuleCollider.size = new Vector2(0.35f, 0.8f);

        CurrentAction = Random.Range(0, 3);
        PreviousAction = CurrentAction;

        switch(CurrentAction)
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
            NextPosition = NavPoints[PointNumber];
        }
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Player")
        {
            CollisionInfo.transform.GetComponent<PlayerHealth>().LoseHeart();
        }
    }

    public void StopEverything()
    {
        StopAllCoroutines();
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

    public void SkeletonAction()
    {
        AnimatorClipInfo[] ClipInfo = EnemyAnimator.GetCurrentAnimatorClipInfo(0);

        switch (ClipInfo[0].clip.name)
        {
            case "Skeleton Jump":
                break;

            case "Skeleton Throw":
                GetComponent<AudioSource>().PlayOneShot(ThrowBone);
                break;

            case "Skeleton Walk":
                break;
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

        CheckPosition();

        while (CurrentAction == PreviousAction)
        {
            CurrentAction = Random.Range(0, 3);
        }

        PreviousAction = CurrentAction;

        switch (CurrentAction)
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

        CheckPosition();

        while (CurrentAction == PreviousAction)
        {
            CurrentAction = Random.Range(0, 3);
        }

        PreviousAction = CurrentAction;

        switch (CurrentAction)
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

        while (CurrentAction == PreviousAction)
        {
            CurrentAction = Random.Range(0, 3);
        }

        PreviousAction = CurrentAction;

        switch (CurrentAction)
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
            ThisTransform.localPosition = Vector3.MoveTowards(ThisTransform.localPosition, NextPosition, Speed * Time.deltaTime);
        }
    }
}
