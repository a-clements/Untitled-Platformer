using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float GravityMultiplier;
    [SerializeField] private int JumpCount = 1;
    [SerializeField] private bool CanJump = true;
    [SerializeField] private GameObject RockThrower;
    private Rigidbody2D RigidBody;
    private Transform ThisTransform;
    public Transform Checkpoint;

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
        if(CollisionInfo.transform.tag == "Ground")
        {
            JumpCount = 1;
            CanJump = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Manager.Keys[0]))
        {
            GetComponent<SpriteRenderer>().flipX = true;

            RockThrower.transform.rotation = Quaternion.Euler(RockThrower.transform.rotation.x, 180.0f, RockThrower.transform.rotation.z);
            RockThrower.transform.localPosition = new Vector2(-0.25f, RockThrower.transform.localPosition.y);

            ThisTransform.Translate(Vector2.left * Time.deltaTime * RunSpeed, Space.Self);
        }

        if (Input.GetKey(Manager.Keys[1]))
        {
            GetComponent<SpriteRenderer>().flipX = false;

            RockThrower.transform.rotation = Quaternion.Euler(RockThrower.transform.rotation.x, 0.0f, RockThrower.transform.rotation.z);
            RockThrower.transform.localPosition = new Vector2(0.25f, RockThrower.transform.localPosition.y);

            ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
        }

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

        if (RigidBody.velocity.y < 0)
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - 1.0f) * Time.deltaTime;
        }

        else if (RigidBody.velocity.y > 0 && !Input.GetKey(Manager.Keys[6]))
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - 1.5f) * Time.deltaTime;
        }
    }
}
