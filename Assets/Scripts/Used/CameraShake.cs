using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls camera shake magnitude and at what threshold the camera shake is initiated. This can only be triggered if the 
/// shout metre is full and will continue until it is empty.
/// </summary>

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Image ShoutMetre;
    [SerializeField] private CircleCollider2D ShockWave;
    public Text ThresholdText;

    private Vector3 OriginalPosition;
    private float XShake;
    private float YShake;
    [SerializeField] private bool CanShow = true;

    public Slider ShakeMagnitude;
    public Slider VolumeThreshold;
    public Button AbilityActivation;

    private void Start()
    {
        //ShockWave = GameObject.FindGameObjectWithTag("Player").transform.GetChild(2).GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "MapSelection" && SceneManager.GetActiveScene().name != "CutScene0" 
            && SceneManager.GetActiveScene().name != "CutScene1" && SceneManager.GetActiveScene().name != "CutScene2")
        {
            if (ShoutMetre.fillAmount == 1.0f && CanShow == true)
            {
                GameObject.Find("Entry").GetComponent<Checkpoints>().PanelThree.SetActive(true);
                CanShow = false;
                Time.timeScale = 0;
            }

            ThresholdText.text = Mathf.RoundToInt((VolumeThreshold.value * 100)).ToString();

            if (ShoutControl.Volume > VolumeThreshold.value && ShoutMetre.fillAmount == 1.0f)
            {
                StartCoroutine(Shake());
            }
        }
    }

    IEnumerator Shake()
    {
        OriginalPosition = MainCamera.transform.localPosition;

        MainCamera.GetComponent<CameraFollow>().enabled = false;

        while (ShoutMetre.fillAmount > 0)
        {
            XShake = Random.Range(-1.0f, 1.0f) * ShakeMagnitude.value;
            YShake = Random.Range(-1.0f, 1.0f) * ShakeMagnitude.value;

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
