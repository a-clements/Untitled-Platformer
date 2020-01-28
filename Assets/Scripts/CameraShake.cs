using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Camera MainCamera;
    [SerializeField] private Image ShoutMetre;
    [Range(0, 1.0f)]
    [SerializeField] private float ShakeMagnitude = 0.5f;
    [Range(0.1f, 0.9f)]
    [SerializeField] private float Volume = 0.4f;
    [SerializeField] private Text Threshhold;

    private Vector3 OriginalPosition;
    private float XShake;
    private float YShake;

    //[System.Serializable]
    //public class MyEventType : UnityEvent { }

    //public MyEventType OnEvent;

    private void Start()
    {
        //if (OnEvent == null)
        //    OnEvent = new MyEventType();

        //OnEvent.AddListener(Ping);

        //Threshhold.text = Volume.ToString();
    }

    //void Ping()
    //{
    //    Threshhold.text = Mathf.RoundToInt((Volume * 100)).ToString();
    //}

    // Update is called once per frame
    void Update()
    {
        Threshhold.text = Mathf.RoundToInt((Volume * 100)).ToString();

        if (ShoutControl.Volume > Volume && ShoutMetre.fillAmount == 1.0f)
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
            yield return null;
        }

        MainCamera.transform.localPosition = OriginalPosition;

        MainCamera.GetComponent<CameraFollow>().enabled = true;
    }
}
