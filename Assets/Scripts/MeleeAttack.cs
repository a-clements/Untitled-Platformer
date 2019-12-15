using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float AttackTimer;
    [SerializeField] private float MeleeDistance = 0.5f;

    void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Manager.Keys[2]))
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKeyDown(Manager.Keys[3]))
        {
            StartCoroutine(AttackUp());
        }

        if(Input.GetKeyDown(Manager.Keys[4]))
        {
            StartCoroutine(AttackDiagonal());
        }
    }

    IEnumerator Attack()
    {
        this.transform.root.GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsAttacking", true);

        if(this.transform.root.GetComponent<SpriteRenderer>().flipX == false)
        {
            this.transform.localPosition = new Vector2(this.transform.parent.position.x + MeleeDistance, 0);
        }

        else
        {
            this.transform.localPosition = new Vector2(this.transform.parent.position.x - MeleeDistance, 0);
        }

        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0,0);
        this.transform.root.GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsAttacking", false);
        yield return null;
    }

    IEnumerator AttackUp()
    {
        this.transform.localPosition = new Vector2(0, this.transform.parent.position.y + MeleeDistance);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }

    IEnumerator AttackDiagonal()
    {
        if (this.transform.root.GetComponent<SpriteRenderer>().flipX == false)
        {
            this.transform.localPosition = new Vector2(this.transform.parent.position.x + MeleeDistance, this.transform.parent.position.y + MeleeDistance);
        }

        else
        {
            this.transform.localPosition = new Vector2(this.transform.parent.position.x - MeleeDistance, this.transform.parent.position.y + MeleeDistance);
        }

        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }
}
