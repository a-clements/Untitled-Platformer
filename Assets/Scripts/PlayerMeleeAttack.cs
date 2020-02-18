using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    private GameManager Manager;
    [SerializeField] private float AttackTimer;
    [SerializeField] private float MeleeDistance = 0.5f;

    void Start()
    {
        Manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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
        this.transform.root.GetComponent<PlayerMove>().PlayerAnimator.SetTrigger("IsAttacking");

        this.GetComponent<CircleCollider2D>().enabled = true;

        this.transform.localPosition = new Vector2(this.transform.localPosition.x + MeleeDistance, 0);

        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0,0);
        this.GetComponent<CircleCollider2D>().enabled = false;
        yield return null;
    }

    IEnumerator AttackUp()
    {
        this.transform.root.GetComponent<PlayerMove>().PlayerAnimator.SetTrigger("IsAttackUp");

        yield return new WaitForSeconds(this.transform.root.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        this.GetComponent<CircleCollider2D>().enabled = true;

        this.transform.localPosition = new Vector2(0, this.transform.localPosition.y + MeleeDistance);

        yield return new WaitForSeconds(AttackTimer);
        this.transform.localPosition = new Vector2(0, 0);
        this.GetComponent<CircleCollider2D>().enabled = false;
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Enemy")
        {
            TriggerInfo.GetComponent<EnemyDeath>().Dead();
        }
    }
}
