using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Player = GameObject.Find("Player").transform;
        ThisTransform = this.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ThisTransform.position = new Vector3(Mathf.Clamp(Player.position.x, XMin, XMax), Mathf.Clamp(Player.position.y, YMin, YMax), ThisTransform.position.z);
    }
}
