using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge : MonoBehaviour
{
    [Tooltip("A declaration on if the bridge is falling. If it is false then all variables are should be empty.")]
    [SerializeField] private bool IsFallingBridge = false;
    [Tooltip("A declaration of how quickly after the player steps onto the bridge that it will fall.")]
    [SerializeField] private float WaitTimer = 0.0f;
    [Tooltip("A declaration of the where the bridge starts.")]
    [SerializeField] private float StartPosition = 0.0f;
    [Tooltip("A declaration of the where the bridge starts.")]
    [SerializeField] private float EndPosition = 0.0f;
    [Tooltip("A declaration of how fast the bridge will fall.")]
    [SerializeField] private float FallSpeed = 0.0f;
    [Tooltip("A declaration of how fast the bridge will reset.")]
    [SerializeField] private float RiseSpeed = 0.0f;

    private string Direction = "";

    void Update()
    {
        if (this.transform.localPosition.y > StartPosition)
        {
            Direction = "";
            this.transform.localPosition = new Vector2(this.transform.localPosition.x, StartPosition);
        }

        if(this.transform.localPosition.y < EndPosition)
        {
            StartCoroutine(Rise());
        }

        switch (Direction)
        {
            case "Rise":
                this.transform.Translate(new Vector3(0, RiseSpeed, 0) * Time.deltaTime);
                break;
            case "Fall":
                this.transform.Translate(new Vector3(0, -FallSpeed, 0) * Time.deltaTime);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D CollisionInfo)
    {
        if (CollisionInfo.transform.tag == "Player" && IsFallingBridge == true)
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(WaitTimer);

        Direction = "Fall";

        yield return null;
    }

    IEnumerator Rise()
    {
        yield return new WaitForSeconds(WaitTimer);

        Direction = "Rise";

        yield return null;
    }
}
