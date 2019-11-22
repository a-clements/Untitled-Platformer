using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiWallClimb : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {

	}

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Player")
        {
            foreach (ContactPoint2D point2D in CollisionInfo.contacts)
            {
                if (point2D.normal.y <= 0)
                {
                    CollisionInfo.transform.GetComponent<PlayerMove>().CanJump = true;
                    CollisionInfo.transform.GetComponent<PlayerMove>().JumpCount = 1;
                }

                else
                {
                    CollisionInfo.transform.GetComponent<PlayerMove>().CanJump = false;
                }

                if (point2D.normal.x >= 0)
                {
                    CollisionInfo.transform.GetComponent<PlayerMove>().CanJump = false;
                    CollisionInfo.transform.GetComponent<PlayerMove>().JumpCount = -1;
                    CollisionInfo.transform.position = new Vector2(CollisionInfo.transform.position.x + 0.0125f, CollisionInfo.transform.position.y);
                    CollisionInfo.transform.GetComponent<PlayerMove>().CanJump = true;
                    CollisionInfo.transform.GetComponent<PlayerMove>().JumpCount = 1;
                }

                if (point2D.normal.x <= 0)
                {
                    CollisionInfo.transform.GetComponent<PlayerMove>().CanJump = false;
                    CollisionInfo.transform.GetComponent<PlayerMove>().JumpCount = -1;
                    CollisionInfo.transform.position = new Vector2(CollisionInfo.transform.position.x - 0.0125f, CollisionInfo.transform.position.y);
                    CollisionInfo.transform.GetComponent<PlayerMove>().CanJump = true;
                    CollisionInfo.transform.GetComponent<PlayerMove>().JumpCount = 1;
                }
            }
        }
    }
}
