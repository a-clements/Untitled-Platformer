using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float GravityMultiplier;
    [SerializeField] private float AttachCameraPoint;
    [SerializeField] private float DetachCameraPoint;
    [SerializeField] private GameObject SubObjects;
    [SerializeField] private GameObject Camera;
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
        Camera = GameObject.Find("Post Process Camera");
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

        #region Camera
        if (ThisTransform.position.x >= AttachCameraPoint)
        {
            Camera.transform.SetParent(ThisTransform);
        }
        else
        {
            Camera.transform.parent = null;
            Camera.transform.position = new Vector3(OriginalCameraPosition.x, OriginalCameraPosition.y, -10);
        }

        if (ThisTransform.position.x >= DetachCameraPoint)
        {
            Camera.transform.parent = null;
        }

        if (Camera.transform.IsChildOf(ThisTransform))
        {
            //if (ThisTransform.position.y <= 0)
            //{
            //    Camera.transform.position = new Vector3(ThisTransform.position.x, -0.625f, -10);
            //}

            //else if (ThisTransform.position.y >= 1.5f)
            //{
            //    Camera.transform.position = new Vector3(ThisTransform.position.x, 3.5f, -10);
            //}

            //else
            //{
            //    Camera.transform.position = new Vector3(ThisTransform.position.x, ThisTransform.position.y, -10);
            //}
        }
        #endregion
    }
}
