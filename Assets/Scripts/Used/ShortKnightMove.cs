using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines how the short range knight moves. The designer can change the speed of the patrol
/// and how many nav points the short range knight has. The actions taken by the short range knight is random, and the same action
/// cannot be carried out twice in succession.
/// </summary>

public class ShortKnightMove : MonoBehaviour
{
    [Tooltip("How fast is the Knight.")]
    [SerializeField] private float Speed = 1;
    [Tooltip("Place an audio clip here")]
    [SerializeField] private AudioClip AttackClip;
    [SerializeField] private AudioClip WalkClip;
    [Tooltip("An array of NavPoints at which the enemy will stop.")]
    [SerializeField] private Vector3[] NavPoints;

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

        CurrentAction = Random.Range(0, 2);
        PreviousAction = CurrentAction;

        switch (CurrentAction)
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

    public void StopEverything()
    {
        StopAllCoroutines();
    }

    public void ShortKnightAction()
    {
        AnimatorClipInfo[] ClipInfo = EnemyAnimator.GetCurrentAnimatorClipInfo(0);

        switch (ClipInfo[0].clip.name)
        {
            case "Short Range Knight Attack":
                GetComponent<AudioSource>().PlayOneShot(AttackClip);
                break;

            case "Short Range Knight Walk":
                GetComponent<AudioSource>().PlayOneShot(WalkClip);
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

        while (CurrentAction == PreviousAction)
        {
            CurrentAction = Random.Range(0, 2);
        }

        PreviousAction = CurrentAction;

        switch (CurrentAction)
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

        while (CurrentAction == PreviousAction)
        {
            CurrentAction = Random.Range(0, 2);
        }

        PreviousAction = CurrentAction;

        switch (CurrentAction)
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
        AnimatorClipInfo[] ClipInfo = EnemyAnimator.GetCurrentAnimatorClipInfo(0);

        #region Resize Collider
        switch (ClipInfo[0].clip.name)
        {
            case "Short Range Knight Idle":
                CapsuleCollider.offset = new Vector2(0.0f, -.2f);
                CapsuleCollider.size = new Vector2(0.7f, 0.7f);
                break;

            case "Short Range Knight Walk":
                CapsuleCollider.offset = new Vector2(0.0f, -.05f);
                CapsuleCollider.size = new Vector2(0.5f, 1.0f);
                break;

            case "Short Range Knight Attack":
                CapsuleCollider.offset = new Vector2(0.0f, -.05f);
                CapsuleCollider.size = new Vector2(0.5f, 1.0f);
                break;
        }
        #endregion

        if (IsMoving == true)
        {
            //if (ThisTransform.localPosition == NavPoints[PointNumber])
            if (ThisTransform.localPosition.x <= NavPoints[0].x || ThisTransform.localPosition.x >= NavPoints[NavPoints.Length - 1].x)
            {
                PointNumber++;

                if (PointNumber == NavPoints.Length)
                {
                    PointNumber = 0;
                }

                NextPosition = NavPoints[PointNumber];

                Sprite.flipX = !Sprite.flipX;
            }

            ThisTransform.localPosition = Vector3.MoveTowards(ThisTransform.localPosition, NextPosition, Speed * Time.deltaTime);
        }
    }
}
