using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    public GameObject Rock;

    public int PooledObjects = 10;

    public float FireRate = 0.5F;
    public float Speed = 64.0f;

    private float NextFire = 1.0F;

    private GameManager Manager;

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

        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

                    if(transform.root.rotation.y == 0)
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
        if (Input.GetKey(Manager.Keys[5]))
        {
            Throw();
        }
    }
}
