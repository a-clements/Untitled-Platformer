using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private ScoreManager Manager;
    [SerializeField] private int PointValue;

    void Start()
    {
        Manager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.transform.tag == "Player")
        {

            Manager.UpdateScores(PointValue);
            this.gameObject.SetActive(false);
        }
    }
}
