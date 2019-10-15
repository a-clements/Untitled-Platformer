using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpikeTrap : MonoBehaviour
{
    [SerializeField] private GameObject SpikeTrap;

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Player")
        {
            SpikeTrap.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.6f, this.transform.position.z);
        }
    }
}
