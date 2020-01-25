using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileColour : MonoBehaviour
{
    private MaterialPropertyBlock PropertyBlock;

    [HideInInspector]
    [Range(-1, 0)]
    public float Exposure = 0.0f;

    [HideInInspector]
    [Range(0, 1)]
    public float Contrast = 1.0f;

    [SerializeField] private float LowExposure = -0.3f;
    [SerializeField] private float FullExposure = 0.0f;

    [SerializeField] private float LowContrast = 0.0f;
    [SerializeField] private float FullContrast = 1.0f;

    [SerializeField] private GameObject Tilemap;
    [SerializeField] private GameObject Lights;

    void Start()
    {
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
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
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

            for(int i = 0; i < Lights.transform.childCount; i++)
            {
                Lights.transform.GetChild(i).GetComponent<FlickeringLights>().LightsActive = true;
            }
        }
    }
}
