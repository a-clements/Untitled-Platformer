using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingVolume : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Ground")
        {
            this.transform.parent.GetComponent<PlayerMove>().JumpCount = 1;
            this.transform.parent.GetComponent<PlayerMove>().CanJump = true;
        }
    }
}
