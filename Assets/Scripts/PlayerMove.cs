using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float GravityMultiplier;
    public int JumpCount = 1;
    public bool CanJump = true;
    [SerializeField] private GameObject SubObjects;
    private Rigidbody2D RigidBody;
    private Transform ThisTransform;
    public Transform Checkpoint;
    RaycastHit2D Hit2D = new RaycastHit2D();
    Collider2D Collider = new Collider2D();
    ContactFilter2D Filter2D = new ContactFilter2D();


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

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        Hit2D = Physics2D.Raycast(ThisTransform.position - new Vector3(0, GetComponent<SpriteRenderer>().bounds.extents.y + 0.01f, 0), Vector2.down, 0.1f);

        if(Hit2D.transform.tag == "Ground")
        {
            CanJump = true;
            JumpCount = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Move left
        if (Input.GetKey(Manager.Keys[0]))
        {
            GetComponent<SpriteRenderer>().flipX = true;

            SubObjects.transform.rotation = Quaternion.Euler(SubObjects.transform.rotation.x, 180.0f, SubObjects.transform.rotation.z);
            SubObjects.transform.localPosition = new Vector2(-0.25f, SubObjects.transform.localPosition.y);

            ThisTransform.Translate(Vector2.left * Time.deltaTime * RunSpeed, Space.Self);
        }
        #endregion

        #region Move Right
        if (Input.GetKey(Manager.Keys[1]))
        {
            GetComponent<SpriteRenderer>().flipX = false;

            SubObjects.transform.rotation = Quaternion.Euler(SubObjects.transform.rotation.x, 0.0f, SubObjects.transform.rotation.z);
            SubObjects.transform.localPosition = new Vector2(0.25f, SubObjects.transform.localPosition.y);

            ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
        }
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
            }
        }
        #endregion

        #region Realistic Jump
        if (RigidBody.velocity.y < 0)
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - 1.0f) * Time.deltaTime;
        }

        else if (RigidBody.velocity.y > 0 && !Input.GetKey(Manager.Keys[6]))
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - 1.5f) * Time.deltaTime;
        }
        #endregion
    }
}
