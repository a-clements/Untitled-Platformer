using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpikeTrap : MonoBehaviour
{
    [SerializeField] private GameObject SpikeTrap;
    [SerializeField] private float WaitTimer = 0.25f;

    private void OnEnable()
    {
        SpikeTrap = this.transform.GetChild(0).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D CollisionInfo)
    {
        if(CollisionInfo.transform.tag == "Player")
        {
            SpikeTrap.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitTimer);
        SpikeTrap.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        yield return null;
    }
}
