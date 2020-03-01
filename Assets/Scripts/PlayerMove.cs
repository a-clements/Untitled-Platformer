using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float GravityMultiplier;
    [SerializeField] private float JumpModifier = 1.0f;
    [SerializeField] private float FallModifier = 1.5f;
    [SerializeField] private float Knockback = 3.0f;
    [SerializeField] private float SnoozeTimer = 1.0f;
    [SerializeField] private int FallSpeed;

    private Rigidbody2D RigidBody;
    private CapsuleCollider2D CapsuleCollider;
    private Transform ThisTransform;

    public Animator PlayerAnimator;
    public int JumpCount = 1;
    public bool CanJump = true;
    public Transform Checkpoint;
    public AnimatorClipInfo[] ClipInfo;


    private void Awake()
    {
        Manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CapsuleCollider = GetComponent<CapsuleCollider2D>();
        ThisTransform = this.transform;
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.gameObject.tag == "Ground")
        {
            JumpCount = 1;
            CanJump = true;

            if(FallSpeed < -20)
            {
                GetComponent<PlayerHealth>().LoseHeart();
                GetComponent<PlayerHealth>().LoseHeart();
                GetComponent<PlayerHealth>().LoseHeart();
                GetComponent<PlayerHealth>().LoseHeart();
            }

            if (FallSpeed < -15)
            {
                GetComponent<PlayerHealth>().LoseHeart();
                GetComponent<PlayerHealth>().LoseHeart();
            }

            if (FallSpeed < -10)
            {
                GetComponent<PlayerHealth>().LoseHeart();
            }

            if(PlayerAnimator.GetBool("IsWalking") == false)
            {
                PlayerAnimator.SetBool("IsIdle", true);
            }

            RigidBody.velocity = Vector2.zero;
            RigidBody.angularVelocity = 0.0f;

            FallSpeed = 0;

            StartCoroutine(GoBackToSleep());
        }

        if(CollisionInfo.gameObject.tag == "Enemy")
        {
            if(CollisionInfo.transform.position.x > ThisTransform.position.x)
            {
                ThisTransform.GetComponent<Rigidbody2D>().AddForce((Vector2.left + Vector2.up) * Knockback, ForceMode2D.Impulse);
            }

            else
            {
                ThisTransform.GetComponent<Rigidbody2D>().AddForce((Vector2.right + Vector2.up) * Knockback, ForceMode2D.Impulse);
            }
        }
    }

    public void StopEverything()
    {
        StopAllCoroutines();
    }

    public IEnumerator Snooze()
    {
        //AnimatorClipInfo[] ClipInfo = PlayerAnimator.GetCurrentAnimatorClipInfo(0);

        //if (ClipInfo[0].clip.name != "Snooze")
        //{
        //    PlayerAnimator.SetBool("IsSleeping", false);
        //}

        //else
        //{
        //    yield return null;
        //}
        yield return new WaitForSeconds(SnoozeTimer);

        PlayerAnimator.SetBool("IsIdle", false);
        PlayerAnimator.SetBool("IsSleeping", true);

        yield return null;
    }

    public IEnumerator WakeUp()
    {
        ClipInfo = PlayerAnimator.GetCurrentAnimatorClipInfo(0);

        if(ClipInfo[0].clip.name == "Snooze")
        {
            PlayerAnimator.SetBool("IsSleeping", false);
            PlayerAnimator.SetBool("IsIdle", true);
        }

        else
        {
            yield return null;
        }

        yield return null;
    }

    public IEnumerator GoBackToSleep()
    {
        //yield return new WaitForSeconds(ClipInfo.Length);
        yield return new WaitForSeconds(SnoozeTimer);

        StartCoroutine(Snooze());

        yield return null;
    }

    void Start()
    {

    }

    void Update()
    {
        if(Time.timeScale > 0)
        {
            ClipInfo = PlayerAnimator.GetCurrentAnimatorClipInfo(0);

            #region Walk Left
            if (Input.GetKey(Manager.Keys[0]))
            {
                //ThisTransform.GetComponent<SpriteRenderer>().flipX = false;
                if (ClipInfo[0].clip.name != "Snooze" && ClipInfo[0].clip.name != "Wake Up")
                {
                    this.transform.rotation = Quaternion.Euler(0, 180, 0);

                    ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
                }

            }
            #endregion

            #region Walk Right
            if (Input.GetKey(Manager.Keys[1]))
            {
                //ThisTransform.GetComponent<SpriteRenderer>().flipX = true;
                if (ClipInfo[0].clip.name != "Snooze" && ClipInfo[0].clip.name != "Wake Up")
                {
                    this.transform.rotation = Quaternion.Euler(0, 0, 0);

                    ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
                }
            }
            #endregion

            #region Start Walking
            if (Input.GetKeyDown(Manager.Keys[0]) || Input.GetKeyDown(Manager.Keys[1]))
            {
                PlayerAnimator.SetBool("IsIdle", false);
                PlayerAnimator.SetBool("IsWalking", true);

                StopEverything();
                StartCoroutine(WakeUp());
            }
            #endregion

            #region Stop Walking
            if (Input.GetKeyUp(Manager.Keys[0]) || Input.GetKeyUp(Manager.Keys[1]))
            {
                PlayerAnimator.SetBool("IsWalking", false);
                PlayerAnimator.SetBool("IsIdle", true);

                StopEverything();
                StartCoroutine(GoBackToSleep());
            }
            #endregion

            #region Jump
            if (Input.GetKeyDown(Manager.Keys[5]))
            {
                if (ClipInfo[0].clip.name != "Snooze" && ClipInfo[0].clip.name != "Wake Up")
                {
                    if (CanJump == true)
                    {
                        RigidBody.velocity = Vector2.up * JumpHeight * Time.fixedDeltaTime;
                        JumpCount--;

                        if (JumpCount < 0)
                        {
                            CanJump = false;
                        }

                        PlayerAnimator.SetBool("IsJumping", true);
                        PlayerAnimator.SetBool("IsIdle", false);
                    }
                }
            }
            #endregion

            #region Jump Realism
            if (RigidBody.velocity.y < 0)
            {
                RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - JumpModifier) * Time.fixedDeltaTime;
                PlayerAnimator.SetBool("IsJumping", false);
                FallSpeed = (int)RigidBody.velocity.y;
            }

            else if (RigidBody.velocity.y > 0 && !Input.GetKey(Manager.Keys[5]))
            {
                RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - FallModifier) * Time.fixedDeltaTime;
            }
            #endregion

            #region Resize Collider
            switch(ClipInfo[0].clip.name)
            {
                case "Player Walk":
                case "Player Jump":
                case "Player Attack Up":
                case "Player Attack":
                    CapsuleCollider.direction = CapsuleDirection2D.Vertical;
                    CapsuleCollider.offset = new Vector2(0.0f, -.07f);
                    CapsuleCollider.size = new Vector2(0.5f, 1.0f);
                    break;

                case "Player Idle":
                    CapsuleCollider.direction = CapsuleDirection2D.Vertical;
                    CapsuleCollider.offset = new Vector2(0, -.32f);
                    CapsuleCollider.size = new Vector2(0.5f, 0.5f);
                    break;

                case "Snooze":
                    CapsuleCollider.direction = CapsuleDirection2D.Horizontal;
                    CapsuleCollider.offset = new Vector2(0, -.4f);
                    CapsuleCollider.size = new Vector2(0.7f, 0.25f);
                    break;
            }
            #endregion
        }
    }
}
