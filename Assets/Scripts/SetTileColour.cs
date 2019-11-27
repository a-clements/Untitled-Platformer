using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileColour : MonoBehaviour
{
    private MaterialPropertyBlock PropertyBlock;

    //[Range(-100,100)]
    //public float saturation = 0.0f;

    //[Range(1, 10)]
    //public float contrast = 1.0f;

    [HideInInspector]
    [Range(-1, 0)]
    public float exposure = 0.0f;

    [SerializeField] private float LowLight = -1.0f;
    [SerializeField] private float FullLight = 0.0f;

    [SerializeField] private GameObject Tilemap;



    void Start()
    {
        PropertyBlock = new MaterialPropertyBlock();

        Tilemap.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
        exposure = LowLight;
        PropertyBlock.SetFloat("_Exposure", exposure);
        Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

        if (Tilemap.transform.childCount > 0)
        {
            for (int i = 0; i < Tilemap.transform.childCount; i++)
            {
                Tilemap.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                //PropertyBlock.SetFloat("_Saturation", saturation);
                //PropertyBlock.SetFloat("_Contrast", contrast);
                exposure = LowLight;
                PropertyBlock.SetFloat("_Exposure", exposure);
                Tilemap.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
            }
        }

        else
        {
            this.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
            //PropertyBlock.SetFloat("_Saturation", saturation);
            //PropertyBlock.SetFloat("_Contrast", contrast);
            exposure = LowLight;
            PropertyBlock.SetFloat("_Exposure", exposure);
            Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
        }

    }

    //private void OnCollisionEnter2D(Collision2D CollisionInfo)
    //{
    //    this.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
    //    //PropertyBlock.SetFloat("_Saturation", saturation);
    //    //PropertyBlock.SetFloat("_Contrast", contrast);
    //    exposure = 0.0f;
    //    PropertyBlock.SetFloat("_Exposure", exposure);
    //    this.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
    //}

    private void OnTriggerEnter2D(Collider2D TriggerInfo)
    {
        Debug.Log(TriggerInfo.tag);
        if(TriggerInfo.tag == "Player")
        {
            Tilemap.transform.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
            exposure = FullLight;
            PropertyBlock.SetFloat("_Exposure", exposure);
            Tilemap.transform.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);

            if (Tilemap.transform.childCount > 0)
            {
                for (int i = 0; i < Tilemap.transform.childCount; i++)
                {
                    Tilemap.transform.GetChild(i).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
                    //PropertyBlock.SetFloat("_Saturation", saturation);
                    //PropertyBlock.SetFloat("_Contrast", contrast);
                    exposure = FullLight;
                    PropertyBlock.SetFloat("_Exposure", exposure);
                    Tilemap.transform.GetChild(i).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
                }
            }
        }
    }

    //private void OnTriggerExit2D(Collider2D TriggerInfo)
    //{
    //    if (TriggerInfo.tag == "Player")
    //    {
    //        for (int i = 0; i < transform.childCount; i++)
    //        {
    //            for (int b = 0; b < transform.GetChild(i).childCount; b++)
    //            {
    //                this.transform.GetChild(i).GetChild(b).GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
    //                //PropertyBlock.SetFloat("_Saturation", saturation);
    //                //PropertyBlock.SetFloat("_Contrast", contrast);
    //                exposure = -1.0f;
    //                PropertyBlock.SetFloat("_Exposure", exposure);
    //                this.transform.GetChild(i).GetChild(b).GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
    //            }
    //        }
    //    }
    //}
}
