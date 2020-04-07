using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
