using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script has two designer defined variables. The attack timer is the time between attacks. The melee distance is the reach of the
/// melee attack.
/// </summary>

public class PlayerMeleeAttack : MonoBehaviour
{
    private GameManager Manager;
    private PlayerMove Player;

    [SerializeField] private float AttackTimer = 0.25f;
    public float MeleeDistance = 0.25f;
    public AudioClip AttackClip;

    void Start()
    {
        Manager = FindObjectOfType<GameManager>();
        Player = this.transform.parent.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Manager.Keys[2]))
        {
            StartCoroutine(Attack());
        }

        if (Input.GetKey(Manager.Keys[3]))
        {
            StartCoroutine(AttackUp());
        }

        if (Input.GetKeyDown(Manager.Keys[2]) || Input.GetKeyDown(Manager.Keys[3]))
        {
            Player.StopEverything();

            if (Player.ClipInfo[0].clip.name == "Player Sleep")
            {
                StartCoroutine(Player.WakeUp());
            }

            Player.PlayerAnimator.SetBool("IsIdle", false);
        }

        if (Input.GetKeyUp(Manager.Keys[2]) || Input.GetKeyUp(Manager.Keys[3]))
        {
            Player.PlayerAnimator.SetBool("IsIdle", true);

            Player.StopEverything();

            StartCoroutine(Player.GoBackToSleep());
        }
    }

    IEnumerator Attack()
    {
        this.transform.parent.GetComponent<PlayerMove>().PlayerAnimator.SetTrigger("IsAttacking");

        yield return null;
    }

    IEnumerator AttackUp()
    {
        this.transform.parent.GetComponent<PlayerMove>().PlayerAnimator.SetTrigger("IsAttackUp");

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
