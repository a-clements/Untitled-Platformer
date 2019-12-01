using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlickeringLights : MonoBehaviour
{
    public bool LightsActive = false;
    [SerializeField] private float FlickerTimer;
    [SerializeField] private float MinFlickerTime = 0.01f;
    [SerializeField] private float MaxFlickerTime = 0.05f;
    [SerializeField] private int MinOuterRadius = 1;
    [SerializeField] private int MaxOuterRadius = 5;
    [SerializeField] private int MinColourRange = 60;
    [SerializeField] private int MaxColourRange = 121;
    [SerializeField] private float MinIntensity = 0.5f;
    [SerializeField] private float MaxIntensity = 2.0f;
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
            FlickerTimer = Random.Range(MinFlickerTime, MaxFlickerTime);
            GetComponent<Light2D>().pointLightOuterRadius = Random.Range(MinOuterRadius, MaxOuterRadius);
            GetComponent<Light2D>().color = new Color32(255,(byte)Random.Range(MinColourRange,MaxColourRange),0,(byte)Random.Range(MinAlpha,MaxAlpha));
            GetComponent<Light2D>().intensity = Random.Range(MinIntensity, MaxIntensity);
            yield return new WaitForSeconds(FlickerTimer);
        }

        yield return null;

        StartCoroutine(LightFlicker());
    }
}
