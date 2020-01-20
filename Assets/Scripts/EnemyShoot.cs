using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
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
    }

    void Throw()
    {
        float temp = Random.Range(FireRate / 2, FireRate);

        if (Time.time > NextFire)
        {
            NextFire = Time.time + temp;

            for (int i = 0; i < RockList.Count; i++)
            {
                if (!RockList[i].activeInHierarchy)
                {
                    RockList[i].SetActive(true);

                    RockList[i].transform.position = new Vector3(this.transform.position.x, this.transform.position.y);

                    RockList[i].GetComponent<Rigidbody2D>().velocity = Vector2.down * Speed * Time.deltaTime;
                }
            }
        }
    }

    void Update()
    {
        if(this.enabled == true)
        {
            Throw();
        }
    }
}
