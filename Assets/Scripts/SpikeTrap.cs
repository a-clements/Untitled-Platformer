using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float ForceMultiplier;

    private void OnTriggerEnter2D(Collider2D CollisionInfo)
    {
        if(CollisionInfo.GetComponent<SpriteRenderer>().flipX == true)
        {
            CollisionInfo.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.left) * ForceMultiplier, ForceMode2D.Impulse);
        }

        else
        {
            CollisionInfo.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right) * ForceMultiplier, ForceMode2D.Impulse);
        }
    }
}
