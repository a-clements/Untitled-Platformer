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
    }

    IEnumerator Attack()
    {
        this.transform.root.GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsAttacking", true);

        this.GetComponent<CircleCollider2D>().enabled = true;

        this.transform.localPosition = new Vector2(this.transform.localPosition.x + MeleeDistance, 0);

        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0,0);
        this.transform.root.GetComponent<PlayerMove>().PlayerAnimator.SetBool("IsAttacking", false);
        this.GetComponent<CircleCollider2D>().enabled = false;
        yield return null;
    }

    IEnumerator AttackUp()
    {
        this.transform.localPosition = new Vector2(0, this.transform.parent.position.y + MeleeDistance);
        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Enemy")
        {
            TriggerInfo.GetComponent<FlyingEnemyMove>().Dead = true;
        }
    }
}
