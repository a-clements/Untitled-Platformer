﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allows the camera to follow the player so that the camera will never be outside the play area. The designer determines a YMax
/// and a YMin value. The camera will never be above or below these values on the Y axis. The designer also determines a XMax and XMin. The
/// camera will never be above or below these values on the X axis.
/// </summary>

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float YMax;
    [SerializeField] private float YMin;

    [SerializeField] private float XMax;
    [SerializeField] private float XMin;

    [SerializeField] private Transform Player;
    private Transform ThisTransform;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        ThisTransform = this.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ThisTransform.position = new Vector3(Mathf.Clamp(Player.position.x, XMin, XMax),
            Mathf.Clamp(Player.position.y, YMin, YMax), ThisTransform.position.z);
    }
}
