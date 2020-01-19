using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSaturationAmount : MonoBehaviour
{
    [SerializeField] private GameObject PostProcess;
    [SerializeField] private float Value;

    void Start()
    {
        PostProcess = GameObject.Find("Post Process");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
            PostProcess.GetComponent<ColourGrading>().Saturation += Value;
            this.enabled = false;
        }
    }
}
