using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private int PointValue = 10;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.transform.tag == "Player")
        {

            ScoreManager.UpdateScores(PointValue);
            this.gameObject.SetActive(false);
        }
    }
}
