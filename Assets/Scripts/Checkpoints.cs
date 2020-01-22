using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private GameObject Panel;

    private void Start()
    {

    }

    private void OnTriggerStay2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.gameObject.tag == "Player")
        {
            TriggerInfo.GetComponent<PlayerMove>().Checkpoint = this.transform;
            ScoreManager.SaveScores();
        }

        if (Panel != null)
        {
            Panel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D TriggerInfo)
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
    }
}
