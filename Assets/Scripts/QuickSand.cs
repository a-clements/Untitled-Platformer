using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// THis script sucks a player down into the tile and will kill the player after a set period of time. The drag factor and the wait timer are
/// defined by the designer.
/// </summary>

public class QuickSand : MonoBehaviour
{
    [SerializeField] private float WaitTimer = 0.5f;
    [SerializeField] private float Drag = 175.0f;
    [SerializeField] private GameObject Player;

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        Player = TriggerInfo.gameObject;
        Debug.Log("oh crap");
        Player.GetComponent<Rigidbody2D>().drag = Drag;
        StartCoroutine(Sinking(WaitTimer));
    }

    IEnumerator Sinking(float WaitTimer)
    {
        yield return new WaitForSeconds(WaitTimer);
        Player.SetActive(false);
        yield return null;
    }
}
