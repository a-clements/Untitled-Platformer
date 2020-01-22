﻿using System.Collections;
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
    public Animator PlayerAnimator;
    private Rigidbody2D RigidBody;
    private BoxCollider2D BoxCollider;
    private Transform ThisTransform;
    public int JumpCount = 1;
    public bool CanJump = true;
    public Transform Checkpoint;
    public Vector2 OriginalCameraPosition;


    private void Awake()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        BoxCollider = GetComponent<BoxCollider2D>();
        ThisTransform = this.transform;
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Ground")
        {
            JumpCount = 1;
            CanJump = true;

            RigidBody.velocity = Vector2.zero;
            RigidBody.angularVelocity = 0.0f;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        #region Walking
            #region Walk Left
            if (Input.GetKey(Manager.Keys[0]))
            {
                ThisTransform.GetComponent<SpriteRenderer>().flipX = false;

                ThisTransform.Translate(Vector2.left * Time.deltaTime * RunSpeed, Space.Self);
            }
            #endregion

            #region Walk Right
            if (Input.GetKey(Manager.Keys[1]))
            {
                ThisTransform.GetComponent<SpriteRenderer>().flipX = true;

                ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
            }
        #endregion

            #region Start Walking
            if(Input.GetKey(Manager.Keys[0]) || Input.GetKey(Manager.Keys[1]))
            {
                PlayerAnimator.SetBool("IsWalking", true);
            }
            #endregion

            #region Stop Walking
            if (Input.GetKeyUp(Manager.Keys[0]) || Input.GetKeyUp(Manager.Keys[1]))
            {
                PlayerAnimator.SetBool("IsWalking", false);
            }
            #endregion

            if(PlayerAnimator.GetBool("IsWalking") == true || PlayerAnimator.GetBool("IsJumping") == true)
            {
                BoxCollider.offset = new Vector2(0.0f, -.07f);
                BoxCollider.size = new Vector2(0.5f, 1.0f);
            }
            
            else if(PlayerAnimator.GetBool("IsIdle") == true)
            {
                BoxCollider.offset = new Vector2(0, -.32f);
                BoxCollider.size = new Vector2(0.5f, 0.5f);
            }

            else
            {

            }
        #endregion

        #region Jump
        if (Input.GetKeyDown(Manager.Keys[5]))
        {
            if (CanJump == true)
            {
                RigidBody.velocity = Vector2.up * JumpHeight * Time.deltaTime;
                JumpCount--;

                if (JumpCount < 0)
                {
                    CanJump = false;
                }
                PlayerAnimator.SetBool("IsJumping", true);
            }
        }
        #endregion

        #region Jump Realism
        if (RigidBody.velocity.y < 0)
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - JumpModifier) * Time.deltaTime;
            PlayerAnimator.SetBool("IsJumping", false);
        }

        else if (RigidBody.velocity.y > 0 && !Input.GetKey(Manager.Keys[6]))
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - FallModifier) * Time.deltaTime;
        }
        #endregion
    }
}
