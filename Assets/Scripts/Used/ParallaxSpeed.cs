using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines the speed of the parallax on the X axis.
/// </summary>

public class ParallaxSpeed : MonoBehaviour
{
    [SerializeField] private float Parallaxspeed;

    void Start()
    {

    }

    private void Update()
    {
        this.GetComponent<RawImage>().uvRect = new Rect(Time.time * Parallaxspeed, 0, 1, 1);
    }
}
