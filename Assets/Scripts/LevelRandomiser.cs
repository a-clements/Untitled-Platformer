﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRandomiser : MonoBehaviour
{
    List<GameObject> ModulesList = new List<GameObject>();

    [SerializeField] private GameObject[] Modules;

    private System.Random GetRandom;

    private int ModulePosition = 0;
    private int j;

    private void OnEnable()
    {
        //GetRandom = new System.Random((int)System.DateTime.Now.Ticks & 0x0000FFFF);
        GetRandom = new System.Random(System.Guid.NewGuid().GetHashCode() & 0x0000FFFF);

        for(int i = 0; i < Modules.Length; i++)
        {
            ModulesList.Add(Modules[i]);
        }
    }

    void Start()
    {
        for (int i = 0; i < Modules.Length; i++)
        {
            ModulePosition += 24;
            j = GetRandom.Next(0, ModulesList.Count);
            ModulesList[j].transform.position = new Vector2(ModulePosition, ModulesList[j].transform.position.y);
            ModulesList.RemoveAt(j);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
