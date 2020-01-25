using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private MaterialPropertyBlock PropertyBlock;

    [HideInInspector]
    [Range(-1, 0)]
    public float Exposure = 0.0f;

    [HideInInspector]
    [Range(0, 1)]
    public float Contrast = 1.0f;


    [SerializeField] public int MethodNumber;

    [Header("Lights Out")]
    [SerializeField] private float LowExposure = -0.3f;
    [SerializeField] private float FullExposure = 0.0f;

    [SerializeField] private float LowContrast = 0.0f;
    [SerializeField] private float FullContrast = 1.0f;

    [SerializeField] private GameObject Tilemap;
    [SerializeField] private GameObject Lights;

    [Header("Greyscale")]
    [SerializeField] private GameObject PostProcess;
    [SerializeField] private float Value;

    void Start()
    {
        PostProcess = GameObject.Find("Post Process");

        switch (MethodNumber)
        {
            case 0:
                PostProcess.GetComponent<ColourGrading>().Saturation = -100;
                break;

            case 1:
                PropertyBlock = new MaterialPropertyBlock();

                Tilemap.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                Exposure = LowExposure;
                Contrast = LowContrast;
                PropertyBlock.SetFloat("_Exposure", Exposure);
                PropertyBlock.SetFloat("_Contrast", Contrast);
                Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

                if (Tilemap.transform.childCount > 0)
                {
                    for (int i = 0; i < Tilemap.transform.childCount; i++)
                    {
                        Tilemap.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                        Exposure = LowExposure;
                        Contrast = LowContrast;
                        PropertyBlock.SetFloat("_Exposure", Exposure);
                        PropertyBlock.SetFloat("_Contrast", Contrast);
                        Tilemap.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if (TriggerInfo.tag == "Player")
        {
            switch(MethodNumber)
            {
                case 0:
                    PostProcess.GetComponent<ColourGrading>().Saturation += Value;
                    Value = 0.0f;
                    break;

                case 1:
                    Tilemap.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                    Exposure = FullExposure;
                    Contrast = FullContrast;
                    PropertyBlock.SetFloat("_Exposure", Exposure);
                    PropertyBlock.SetFloat("_Contrast", Contrast);
                    Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

                    if (Tilemap.transform.childCount > 0)
                    {
                        for (int i = 0; i < Tilemap.transform.childCount; i++)
                        {
                            Tilemap.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                            Exposure = FullExposure;
                            Contrast = FullContrast;
                            PropertyBlock.SetFloat("_Exposure", Exposure);
                            PropertyBlock.SetFloat("_Contrast", Contrast);
                            Tilemap.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
                        }
                    }

                    for (int i = 0; i < Lights.transform.childCount; i++)
                    {
                        Lights.transform.GetChild(i).GetComponent<FlickeringLights>().LightsActive = true;
                    }
                    break;
            }
        }
    }
}
