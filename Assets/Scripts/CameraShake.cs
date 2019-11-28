using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Image ShoutMetre;
    [Range(0, 0.5f)]
    [SerializeField] private float ShakeMagnitude;
    [Range(0, 1.0f)]
    [SerializeField] private float Volume;

    private Vector3 OriginalPosition;
    private float XShake;
    private float YShake;

    private void Start()
    {
        OriginalPosition = MainCamera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(ShoutControl.Volume > Volume && ShoutMetre.fillAmount == 1.0f)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        MainCamera.GetComponent<CameraFollow>().enabled = false;

        while(ShoutMetre.fillAmount > 0)
        {
            XShake = Random.Range(-1.0f, 1.0f) * ShakeMagnitude;
            YShake = Random.Range(-1.0f, 1.0f) * ShakeMagnitude;

            MainCamera.transform.localPosition = new Vector3(XShake, YShake, OriginalPosition.z);

            ShoutMetre.fillAmount -= Time.deltaTime;
            yield return null;
        }

        MainCamera.transform.localPosition = OriginalPosition;

        MainCamera.GetComponent<CameraFollow>().enabled = true;
    }
}
