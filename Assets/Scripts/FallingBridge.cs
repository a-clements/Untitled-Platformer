using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge : MonoBehaviour
{
    [SerializeField] private bool IsFallingBridge = false;
    [SerializeField] private float WaitTimer = 0.0f;
    [SerializeField] private float StartPosition = 0.0f;
    [SerializeField] private float EndPosition = 0.0f;
    [SerializeField] private float FallSpeed = 0.0f;
    [SerializeField] private float RiseSpeed = 0.0f;
    private string Direction = "";

    void Update()
    {
        if (this.transform.localPosition.y > StartPosition)
        {
            Direction = "";
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

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Player" && IsFallingBridge == true)
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(WaitTimer);

        Direction = "Fall";

        yield return new WaitForSeconds(WaitTimer);

        yield return null;
    }

    IEnumerator Rise()
    {
        yield return new WaitForSeconds(WaitTimer);

        Direction = "Rise";

        yield return null;
    }
}
