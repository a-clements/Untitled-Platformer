using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float AttackTimer;

    void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Manager.Keys[2]))
        {
            StartCoroutine(AttackLeft());
        }

        if (Input.GetKeyDown(Manager.Keys[3]))
        {
            StartCoroutine(AttackUp());
        }

        if (Input.GetKeyDown(Manager.Keys[4]))
        {
            StartCoroutine(AttackRight());
        }

        if(Input.GetKeyDown(Manager.Keys[2]) && Input.GetKeyDown(Manager.Keys[3]))
        {
            StartCoroutine(AttackLeftDiagonal());
        }

        if (Input.GetKeyDown(Manager.Keys[4]) && Input.GetKeyDown(Manager.Keys[3]))
        {
            StartCoroutine(AttackRightDiagonal());
        }
    }

    IEnumerator AttackLeft()
    {
        this.transform.localPosition = new Vector2(this.transform.parent.position.x - 1.0f, 0);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0,0);
        yield return null;
    }

    IEnumerator AttackUp()
    {
        this.transform.localPosition = new Vector2(0, this.transform.parent.position.y + 1.0f);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }

    IEnumerator AttackRight()
    {
        this.transform.localPosition = new Vector2(this.transform.parent.position.x + 1.0f, 0);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }

    IEnumerator AttackLeftDiagonal()
    {
        this.transform.localPosition = new Vector2(this.transform.parent.position.x - 1.0f, this.transform.parent.position.y + 1.0f);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }

    IEnumerator AttackRightDiagonal()
    {
        this.transform.localPosition = new Vector2(this.transform.parent.position.x + 1.0f, this.transform.parent.position.y + 1.0f);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }
}
