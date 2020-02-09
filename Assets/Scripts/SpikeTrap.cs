using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float ForceMultiplier;

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Player")
        {
            CollisionInfo.transform.GetComponent<PlayerHealth>().LoseHeart();

            if (CollisionInfo.transform.GetComponent<SpriteRenderer>().flipX == true)
            {
                CollisionInfo.transform.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right) * ForceMultiplier, ForceMode2D.Impulse);
            }

            else
            {
                CollisionInfo.transform.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * ForceMultiplier, ForceMode2D.Impulse);
            }
        }
    }
}
