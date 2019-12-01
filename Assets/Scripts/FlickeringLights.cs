using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlickeringLights : MonoBehaviour
{
    public bool LightsActive = false;
    private Light2D Light;
    [SerializeField] private float FlickerTimer;
    [SerializeField] private float MinFlickerTime = 0.01f;
    [SerializeField] private float MaxFlickerTime = 0.05f;
    [SerializeField] private int MinOuterRadius = 1;
    [SerializeField] private int MaxOuterRadius = 5;
    [SerializeField] private Color MinColourRange;
    [SerializeField] private Color MaxColourRange;
    [SerializeField] private float MinIntensity = 1.0f;
    [SerializeField] private float MaxIntensity = 3.0f;
    [SerializeField] private float MinAlpha = 0.5f;
    [SerializeField] private float MaxAlpha = 1f;

    void Start()
    {
        Light = GetComponent<Light2D>();
        StartCoroutine(LightFlicker());
    }

    IEnumerator LightFlicker()
    {
        while (LightsActive == true)
        {
            FlickerTimer = Random.Range(MinFlickerTime, MaxFlickerTime);
            Light.pointLightOuterRadius = Mathf.Lerp(MinOuterRadius, MaxOuterRadius, FlickerTimer);
            MaxColourRange.a = MinColourRange.a = Mathf.Lerp(MinAlpha, MaxAlpha, FlickerTimer);
            Light.color = Color.Lerp(MinColourRange, MaxColourRange, Time.time * FlickerTimer);
            Light.intensity = Mathf.Lerp(MinIntensity, MaxIntensity, FlickerTimer);
            yield return new WaitForSeconds(FlickerTimer);
        }

        yield return null;

        StartCoroutine(LightFlicker());
    }
}
