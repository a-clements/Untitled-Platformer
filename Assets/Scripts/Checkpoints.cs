using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] ScoreManager Manager;

    private void OnEnable()
    {
        Manager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            TriggerInfo.GetComponent<PlayerMove>().Checkpoint = this.transform;
            Manager.SaveScores();
        }
    }
}
