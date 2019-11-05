using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSand : MonoBehaviour
{
    [SerializeField] private float WaitTimer = 0.5f;
    [SerializeField] private GameObject Player;

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        Player = TriggerInfo.gameObject;
        Debug.Log("oh crap");
        Player.GetComponent<Rigidbody2D>().drag = 175.0f;
        //StartCoroutine(Sinking(WaitTimer * WaitTimer));
        //Player.GetComponent<PlayerMove>().enabled = false;
        StartCoroutine(Sinking(WaitTimer));
    }

    IEnumerator Sinking(float WaitTimer)
    {
        yield return new WaitForSeconds(WaitTimer);
        Player.SetActive(false);
        yield return null;
    }
}
