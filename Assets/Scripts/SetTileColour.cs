using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileColour : MonoBehaviour
{
    private MaterialPropertyBlock PropertyBlock;

    [HideInInspector]
    [Range(-1, 0)]
    public float Exposure = 0.0f;

    [SerializeField] private float LowLight = -1.0f;
    [SerializeField] private float FullLight = 0.0f;

    [SerializeField] private GameObject Tilemap;
    [SerializeField] private GameObject Lights;

    void Start()
    {
        PropertyBlock = new MaterialPropertyBlock();

        Tilemap.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
        Exposure = LowLight;
        PropertyBlock.SetFloat("_Exposure", Exposure);
        Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

        if (Tilemap.transform.childCount > 0)
        {
            for (int i = 0; i < Tilemap.transform.childCount; i++)
            {
                Tilemap.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                Exposure = LowLight;
                PropertyBlock.SetFloat("_Exposure", Exposure);
                Tilemap.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
            }
        }

        StartCoroutine("LightCycle");
    }

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        if(TriggerInfo.tag == "Player")
        {
            Tilemap.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
            Exposure = FullLight;
            PropertyBlock.SetFloat("_Exposure", Exposure);
            Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

            if (Tilemap.transform.childCount > 0)
            {
                for (int i = 0; i < Tilemap.transform.childCount; i++)
                {
                    Tilemap.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                    Exposure = FullLight;
                    PropertyBlock.SetFloat("_Exposure", Exposure);
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
