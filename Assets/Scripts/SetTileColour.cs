using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTileColour : MonoBehaviour
{
    private MaterialPropertyBlock PropertyBlock;

    [Range(-100,100)]
    public float saturation = 0.0f;

    [Range(1, 10)]
    public float contrast = 1.0f;

    [Range(-1, 0)]
    public float exposure = 0.0f;

    void Start()
    {
        PropertyBlock = new MaterialPropertyBlock();
        exposure = -0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Renderer>().GetPropertyBlock(PropertyBlock);
        PropertyBlock.SetFloat("_Saturation", saturation);
        PropertyBlock.SetFloat("_Contrast", contrast);
        PropertyBlock.SetFloat("_Exposure", exposure);
        this.GetComponent<Renderer>().SetPropertyBlock(PropertyBlock);
    }
}
