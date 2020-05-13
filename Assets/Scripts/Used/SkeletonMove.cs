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
    [Tooltip("The Skeleton walk audio clip goes here.")]
    [SerializeField] private AudioClip WalkClip;
    [Header("Bone game object.")]
    [SerializeField] private GameObject Bone;
    [Header("Number of bones in the queue.")]
    [SerializeField] private int PooledObjects = 10;
    [Tooltip("A declaration of the speed of the projectile.")]
    [SerializeField] float BoneSpeed = 64.0f;

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

    [SerializeField] private float BonePosition;

    List<GameObject> BoneList = new List<GameObject>();

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

        int i;
        GameObject obj;

        for (i = 0; i < PooledObjects; i++)
        {
            obj = (GameObject)Instantiate(Bone);
            obj.transform.rotation = Quaternion.identity;
            BoneList.Add(obj);
            BoneList[i].SetActive(false);
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
    }

    public void SkeletonAction()
    {
        AnimatorClipInfo[] ClipInfo = EnemyAnimator.GetCurrentAnimatorClipInfo(0);

        switch (ClipInfo[0].clip.name)
        {
            case "Skeleton Jump":
                break;

            case "Skeleton Throw":
                for (int i = 0; i < BoneList.Count; i++)
                {
                    if (!BoneList[i].activeInHierarchy)
                    {
                        BoneList[i].SetActive(true);

                        if (!Sprite.flipX)
                        {
                            BoneList[i].transform.position = new Vector3((this.transform.position.x - BonePosition), this.transform.position.y);
                            BoneList[i].GetComponent<Rigidbody2D>().velocity = ((Vector2.up * BoneSpeed ) + (Vector2.left * BoneSpeed)) * Time.deltaTime;
                        }

                        else
                        {
                            BoneList[i].transform.position = new Vector3((this.transform.position.x + BonePosition), this.transform.position.y);
                            BoneList[i].GetComponent<Rigidbody2D>().velocity = ((Vector2.up * BoneSpeed) + (Vector2.right * BoneSpeed)) * Time.deltaTime;
                        }
                        break;
                    }
                }

                GetComponent<AudioSource>().PlayOneShot(ThrowBone);

                break;

            case "Skeleton Walk":
                GetComponent<AudioSource>().PlayOneShot(WalkClip);
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
            ThisTransform.localPosition = Vector2.MoveTowards(ThisTransform.localPosition, NextPosition, Speed * Time.deltaTime);
        }
    }
}
