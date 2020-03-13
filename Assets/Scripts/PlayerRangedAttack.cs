using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows the player to throw rock objects continously. The designer can define how many rock objects are in the object pool.
/// The designer can also define the fire rate, the speed at which the rock object travels, and the time between throws. The throw rate
/// is defined as Time.time > NextFire. If this condition is met, then the fire rate and Time.time is added to the NextFire variable.
/// </summary>

public class PlayerRangedAttack : MonoBehaviour
{
    [Tooltip("The prefab of the projectile goes here.")]
    [SerializeField] private GameObject Rock;
    [Tooltip("Change this to however many projectiles you want to have in the pool.")]
    [SerializeField] private int PooledObjects = 10;
    [Tooltip("A declaration of the rate of fire.")]
    [SerializeField] private float FireRate = 0.5F;
    [Tooltip("A declaration of the speed of the projectile.")]
    [SerializeField] float Speed = 64.0f;
    [Tooltip("A declaration of how long between projectiles.")]
    [SerializeField] private float NextFire = 1.0F;

    private GameManager Manager;
    private PlayerMove Player;

    List<GameObject> RockList = new List<GameObject>();

    void Start()
    {
        int i;
        GameObject obj;

        for (i = 0; i < PooledObjects; i++)
        {
            obj = (GameObject)Instantiate(Rock);
            obj.transform.rotation = Quaternion.identity;
            RockList.Add(obj);
            RockList[i].SetActive(false);
        }

        Manager = FindObjectOfType<GameManager>();
        Player = FindObjectOfType<PlayerMove>();
    }

    void Throw()
    {
        if (Time.time > NextFire)
        {
            NextFire = Time.time + FireRate;

            for (int i = 0; i < RockList.Count; i++)
            {
                if (!RockList[i].activeInHierarchy)
                {
                    RockList[i].SetActive(true);

                    RockList[i].transform.position = new Vector3(this.transform.position.x, this.transform.position.y);

                    if(transform.parent.rotation.y == 0)
                    {
                        RockList[i].GetComponent<Rigidbody2D>().velocity = (Vector2.up + Vector2.right) * Speed * Time.deltaTime;
                    }

                    else
                    {
                        RockList[i].GetComponent<Rigidbody2D>().velocity = (Vector2.up + Vector2.left) * Speed * Time.deltaTime;
                    }
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKey(Manager.Keys[4]))
        {
            Player.StopEverything();

            if (Player.ClipInfo[0].clip.name == "Player Sleep")
            {
                StartCoroutine(Player.WakeUp());
            }

            if (Player.ClipInfo[0].clip.name != "Player Sleep" && Player.ClipInfo[0].clip.name != "Player Wake Up")
            {
                Throw();
            }
        }


        if (Input.GetKeyUp(Manager.Keys[4]))
        {
            Player.PlayerAnimator.SetBool("IsIdle", true);

            Player.StopEverything();

            StartCoroutine(Player.GoBackToSleep());
        }
    }
}
