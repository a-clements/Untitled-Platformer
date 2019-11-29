using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlickeringLights : MonoBehaviour
{
    public bool LightsActive = false;
    [SerializeField] private float FlickerTimer;

    void Start()
    {
        StartCoroutine(LightFlicker());
    }

    IEnumerator LightFlicker()
    {
        while (LightsActive == true)
        {
            FlickerTimer = Random.Range(0.01f, 0.05f);
            GetComponent<Light2D>().pointLightOuterRadius = Random.Range(1, 5);
            GetComponent<Light2D>().color = new Color32(255,(byte)Random.Range(60,121),0,(byte)Random.Range(128,256));
            GetComponent<Light2D>().intensity = Random.Range(0.5f,2.0f);
            yield return new WaitForSeconds(FlickerTimer);
        }

        yield return null;

        StartCoroutine(LightFlicker());
    }
}
