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
    [SerializeField] private Animator PlayerAnimator;
    private Rigidbody2D RigidBody;
    private Transform ThisTransform;
    public int JumpCount = 1;
    public bool CanJump = true;
    public Transform Checkpoint;
    public Vector2 OriginalCameraPosition;
    RaycastHit2D Hit2D = new RaycastHit2D();


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
                GetComponent<SpriteRenderer>().flipX = true;

                PlayerAnimator.SetBool("IsWalking", true);

                SubObjects.transform.rotation = Quaternion.Euler(SubObjects.transform.rotation.x, 180.0f, SubObjects.transform.rotation.z);
                SubObjects.transform.localPosition = new Vector2(-0.25f, SubObjects.transform.localPosition.y);

                ThisTransform.Translate(Vector2.left * Time.deltaTime * RunSpeed, Space.Self);
            }
            #endregion

            #region Walk Right
            if (Input.GetKey(Manager.Keys[1]))
            {
                GetComponent<SpriteRenderer>().flipX = false;

                PlayerAnimator.SetBool("IsWalking", true);

                SubObjects.transform.rotation = Quaternion.Euler(SubObjects.transform.rotation.x, 0.0f, SubObjects.transform.rotation.z);
                SubObjects.transform.localPosition = new Vector2(0.25f, SubObjects.transform.localPosition.y);

                ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
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
            }
        }
        #endregion

        #region Jump Realism
        if (RigidBody.velocity.y < 0)
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - JumpModifier) * Time.deltaTime;
        }

        else if (RigidBody.velocity.y > 0 && !Input.GetKey(Manager.Keys[5]))
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - FallModifier) * Time.deltaTime;
        }
        #endregion
    }
}
