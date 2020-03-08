using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// This script controls camera shake magnitude and at what threshold the camera shake is initiated. This can only be triggered if the 
/// shout metre is full and will continue until it is empty.
/// </summary>

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Image ShoutMetre;
    [Range(0, 1.0f)]
    [SerializeField] private float ShakeMagnitude = 0.5f;
    [Range(0.1f, 0.9f)]
    [SerializeField] private float VolumeThreshold = 0.4f;
    [SerializeField] private CircleCollider2D ShockWave;
    [SerializeField] private Text ThresholdText;

    private Vector3 OriginalPosition;
    private float XShake;
    private float YShake;
    private bool CanShow = true;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(ShoutMetre.fillAmount == 1.0f && CanShow == true)
        {
            GameObject.Find("Entry").GetComponent<Checkpoints>().PanelThree.SetActive(true);
            CanShow = false;
            Time.timeScale = 0;
        }

        ThresholdText.text = Mathf.RoundToInt((VolumeThreshold * 100)).ToString();

        if (ShoutControl.Volume > VolumeThreshold && ShoutMetre.fillAmount == 1.0f)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        OriginalPosition = MainCamera.transform.localPosition;

        MainCamera.GetComponent<CameraFollow>().enabled = false;

        while (ShoutMetre.fillAmount > 0)
        {
            XShake = Random.Range(-1.0f, 1.0f) * ShakeMagnitude;
            YShake = Random.Range(-1.0f, 1.0f) * ShakeMagnitude;

            MainCamera.transform.localPosition = new Vector3(OriginalPosition.x - XShake, OriginalPosition.y - YShake, OriginalPosition.z);

            ShoutMetre.fillAmount -= Time.deltaTime;

            ShockWave.radius += (Time.deltaTime * 10);

            yield return null;
        }

        ShockWave.radius = 0.1f;

        MainCamera.transform.localPosition = OriginalPosition;

        MainCamera.GetComponent<CameraFollow>().enabled = true;
    }
}
