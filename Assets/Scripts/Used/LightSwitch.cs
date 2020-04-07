using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script determines how the light switch object works. This script has a custom inspector attached to it so allow the designer to
/// choose what type of light switch it is. The types are saturation based, or colour changing. The saturation based light switch is the
/// default. The MethodNumber is set by the custom inspector and runs different code depending on the value of MethodNumber. The Lights Out
/// method has four designer defined variables. LowExposure determines how visible the texture on the tile is. LowContrast makes the texture
/// of the tile darker. FullExposure allows the texture of the tile to be fully visible and FullContrast allows the texture of the tile to
/// have normal specular. If MethodNumber is 0 then the lights will always be on and flickering but in greyscale. If the MethodNumber is 1
/// then the lights will activate when the player triggers the light switch.
/// </summary>

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

    [SerializeField] private GameObject Platforms;
    [SerializeField] private GameObject Lights;

    [Header("Greyscale")]
    [SerializeField] private GameObject PostProcess;
    [SerializeField] private float Value;

    [Header("Lever sound")]
    [SerializeField] private AudioClip LeverSound;

    void Start()
    {
        PostProcess = GameObject.Find("Post Process");

        switch (MethodNumber)
        {
            case 0:
                PostProcess.GetComponent<ColourGrading>().Saturation = -100;

                for (int i = 0; i < Lights.transform.childCount; i++)
                {
                    Lights.transform.GetChild(i).GetComponent<FlickeringLights>().LightsActive = true;
                }

                break;

            case 1:
                PropertyBlock = new MaterialPropertyBlock();

                Platforms.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                Exposure = LowExposure;
                Contrast = LowContrast;
                PropertyBlock.SetFloat("_Exposure", Exposure);
                PropertyBlock.SetFloat("_Contrast", Contrast);
                Platforms.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

                if (Platforms.transform.childCount > 0)
                {
                    for (int i = 0; i < Platforms.transform.childCount; i++)
                    {
                        Platforms.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                        Exposure = LowExposure;
                        Contrast = LowContrast;
                        PropertyBlock.SetFloat("_Exposure", Exposure);
                        PropertyBlock.SetFloat("_Contrast", Contrast);
                        Platforms.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
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
                    Platforms.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                    Exposure = FullExposure;
                    Contrast = FullContrast;
                    PropertyBlock.SetFloat("_Exposure", Exposure);
                    PropertyBlock.SetFloat("_Contrast", Contrast);
                    Platforms.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

                    if (Platforms.transform.childCount > 0)
                    {
                        for (int i = 0; i < Platforms.transform.childCount; i++)
                        {
                            Platforms.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                            Exposure = FullExposure;
                            Contrast = FullContrast;
                            PropertyBlock.SetFloat("_Exposure", Exposure);
                            PropertyBlock.SetFloat("_Contrast", Contrast);
                            Platforms.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
                        }
                    }

                    for (int i = 0; i < Lights.transform.childCount; i++)
                    {
                        Lights.transform.GetChild(i).GetComponent<FlickeringLights>().LightsActive = true;
                    }
                    break;
            }

            if(!GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                GetComponent<AudioSource>().PlayOneShot(LeverSound);
                GetComponent<ParticleSystem>().Play();
            }
        }
    }
}