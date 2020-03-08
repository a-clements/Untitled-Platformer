using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script calls the Shocked function of the EnemyDeath script.
/// </summary>

public class ShockWave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Enemy")
        {
            TriggerInfo.GetComponent<EnemyDeath>().Shocked();
        }
    }
}
