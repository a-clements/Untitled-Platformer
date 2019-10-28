using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameManager Manager;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float GravityMultiplier;
    private Rigidbody2D RigidBody;
    private Transform ThisTransform;

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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(Manager.Keys[0]))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            ThisTransform.Translate(Vector2.left * Time.deltaTime * RunSpeed, Space.Self);
        }

        if (Input.GetKey(Manager.Keys[1]))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            ThisTransform.Translate(Vector2.right * Time.deltaTime * RunSpeed, Space.Self);
        }

        if (Input.GetKeyDown(Manager.Keys[2]))
        {

        }

        if (Input.GetKeyDown(Manager.Keys[3]))
        {

        }

        if (Input.GetKeyDown(Manager.Keys[4]))
        {

        }

        if (Input.GetKeyDown(Manager.Keys[5]))
        {
            RigidBody.velocity = Vector2.up * JumpHeight * Time.deltaTime;
        }

        if (RigidBody.velocity.y < 0)
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - 1.0f) * Time.deltaTime;
        }

        else if (RigidBody.velocity.y > 0 && !Input.GetKey(Manager.Keys[5]))
        {
            RigidBody.velocity += Vector2.up * Physics2D.gravity.y * (GravityMultiplier - 1.5f) * Time.deltaTime;
        }
    }
}
