using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THis script detects a collision with the player and causes damage, causing the player to be pushed back in
/// the opposite direction to what they are facing.
/// </summary>

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float ForceMultiplier;

    private void OnCollisionStay2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Player")
        {
            if(CollisionInfo.transform.GetComponent<PlayerMove>().PlayerAnimator.GetBool("IsDead") ==  false)
            {
                CollisionInfo.transform.GetComponent<PlayerHealth>().LoseHeart();

                if (CollisionInfo.transform.GetComponent<SpriteRenderer>().flipX == true)
                {
                    CollisionInfo.transform.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right) * ForceMultiplier, ForceMode2D.Force);
                }

                else
                {
                    CollisionInfo.transform.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * ForceMultiplier, ForceMode2D.Force);
                }
            }
        }
    }
}
