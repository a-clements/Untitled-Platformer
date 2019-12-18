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
    [SerializeField] private GameObject SubObjects;
    public Animator PlayerAnimator;
    private Rigidbody2D RigidBody;
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
        ThisTransform = this.transform;
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
                ThisTransform.eulerAngles = new Vector3(0, 180);

                ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
            }
            #endregion

            #region Walk Right
            if (Input.GetKey(Manager.Keys[1]))
            {
                ThisTransform.eulerAngles =  new Vector3(0, 0);

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
        #endregion

        #region Jump
        if (Input.GetKeyDown(Manager.Keys[6]))
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
