using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlickeringLights : MonoBehaviour
{
    public bool LightsActive = false;
    [SerializeField] private float FlickerTimer;
    [SerializeField] private int MinOuterRadius = 1;
    [SerializeField] private int MaxOuterRadius = 5;
    [SerializeField] private int MinColourRange = 60;
    [SerializeField] private int MaxColourRange = 121;
    [SerializeField] private int MinAlpha = 128;
    [SerializeField] private int MaxAlpha = 256;

    void Start()
    {
        StartCoroutine(LightFlicker());
    }

    IEnumerator LightFlicker()
    {
        while (LightsActive == true)
        {
            FlickerTimer = Random.Range(0.01f, 0.05f);
            GetComponent<Light2D>().pointLightOuterRadius = Random.Range(MinOuterRadius, MaxOuterRadius);
            GetComponent<Light2D>().color = new Color32(255,(byte)Random.Range(MinColourRange,MaxColourRange),0,(byte)Random.Range(MinAlpha,MaxAlpha));
            GetComponent<Light2D>().intensity = Random.Range(0.5f,2.0f);
            yield return new WaitForSeconds(FlickerTimer);
        }

        yield return null;

        StartCoroutine(LightFlicker());
    }
}
