﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script defines which microphone is used, what the sample length is, and the sample frequency.
/// </summary>

public class ShoutControl : MonoBehaviour
{
    private AudioSource Source;
    private string Device;
    private bool ShowPanel = true;
    private float[] CurrentSampleData;
    private int CurrentPosition;

    public static float Volume;

    [SerializeField] private Image VolumeMetre;
    [SerializeField] private Text VolumeText;
    [SerializeField] private int DeviceNumber = 0; //this is the microphone number, 0 is the first microphone found
    [SerializeField] private int SampleLength = 10; //this is the length of a sample, the default is 10 for sampling 10 seconds of audio
    [SerializeField] private int SampleFrequency = 128; //this is how often a sample is taken, the default is 128 samples a second
    [SerializeField] private AudioMixerGroup MicrophoneMixer;

    private void OnEnable()
    {
        CurrentSampleData = new float[SampleFrequency];

        Source = GetComponent<AudioSource>();
        Source.outputAudioMixerGroup = MicrophoneMixer;

        if(SceneManager.GetActiveScene().name == "Menu")
        {
            if (Microphone.devices.Length != 0 && GameManager.IsMicrophone == false)
            {
                this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GetComponent<CameraShake>().VolumeThreshold.gameObject.SetActive(true);
                GetComponent<CameraShake>().AbilityActivation.gameObject.SetActive(false);
            }

            else
            {
                this.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
                GetComponent<CameraShake>().VolumeThreshold.gameObject.SetActive(false);
                GetComponent<CameraShake>().AbilityActivation.gameObject.SetActive(true);
            }
        }

        LevelSetup();

        this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }

    void LevelSetup()
    {
        if (Microphone.devices.Length != 0)
        {
            GameManager.IsMicrophone = true;
        }

        else
        {
            GameManager.IsMicrophone = false;
        }

        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "MapSelection" && SceneManager.GetActiveScene().name != "CutScene0"
        && SceneManager.GetActiveScene().name != "CutScene1" && SceneManager.GetActiveScene().name != "CutScene2" && GameManager.IsMicrophone == true)
        {
            GetComponent<CameraShake>().VolumeThreshold.gameObject.SetActive(true);
            GetComponent<CameraShake>().AbilityActivation.gameObject.SetActive(false);
            StartMicrophone();
        }

        else if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "MapSelection" && SceneManager.GetActiveScene().name != "CutScene0"
        && SceneManager.GetActiveScene().name != "CutScene1" && SceneManager.GetActiveScene().name != "CutScene2" && GameManager.IsMicrophone == false)
        {
            VolumeMetre.enabled = false;
            VolumeText.enabled = false;
            GetComponent<CameraShake>().ThresholdText.gameObject.SetActive(false);
            GetComponent<CameraShake>().VolumeThreshold.gameObject.SetActive(false);
            GetComponent<CameraShake>().AbilityActivation.gameObject.SetActive(true);
        }
    }


    public void UseMicrophoneButton()
    {
        GameManager.IsMicrophone = true;
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }

    public void DontUseMicropphoneButton()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

        if(Microphone.devices.Length != 0)
        {
            GetComponent<CameraShake>().VolumeThreshold.gameObject.SetActive(true);
            GetComponent<CameraShake>().ThresholdText.gameObject.SetActive(true);
            GetComponent<CameraShake>().ShoutMetre.gameObject.SetActive(true);
            VolumeMetre.gameObject.SetActive(true);
            VolumeText.gameObject.SetActive(true);
            GetComponent<CameraShake>().AbilityActivation.gameObject.SetActive(false);
        }

        else
        {
            GetComponent<CameraShake>().VolumeThreshold.gameObject.SetActive(false);
            GetComponent<CameraShake>().ThresholdText.gameObject.SetActive(false);
            GetComponent<CameraShake>().ShoutMetre.gameObject.SetActive(false);
            VolumeMetre.gameObject.SetActive(false);
            VolumeText.gameObject.SetActive(false);
            GetComponent<CameraShake>().AbilityActivation.gameObject.SetActive(true);
        }

        ShowPanel = false;
    }

    private void OnDisable()
    {
        Source.Stop();
        Microphone.End(Device);
        Device = "";
        Source = null;
    }

    void StartMicrophone()
    {
        Device = Microphone.devices[DeviceNumber];
        Source.clip = Microphone.Start(Device, true, SampleLength, AudioSettings.outputSampleRate);
        Source.Play();
    }

    void MicrophoneStatus()
    {
        if (Microphone.devices.Length != 0)
        {
            this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

            GameManager.IsMicrophone = true;
        }

        else
        {
            if (ShowPanel == true)
            {
                this.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            }

            GameManager.IsMicrophone = false;
        }
    }

    void GetMicrophoneInput()
    {
        if(GameManager.IsMicrophone == true)
        {
            float Level = 0;

            CurrentPosition = Microphone.GetPosition(Device) - (SampleFrequency + 1);

            Source.clip.GetData(CurrentSampleData, CurrentPosition);

            for (int i = 0; i < SampleFrequency; i++)
            {
                float SamplePeak = CurrentSampleData[i] * CurrentSampleData[i];

                if (Level < SamplePeak)
                {
                    Level = SamplePeak;
                }
            }

            Volume = Mathf.Sqrt(Mathf.Sqrt(Level));

            VolumeMetre.fillAmount = Volume;
            VolumeText.text = (System.Math.Round(Volume, 2) * 100).ToString();
        }

        else
        {
            ShowPanel = true;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "MapSelection" && SceneManager.GetActiveScene().name != "CutScene0"
        && SceneManager.GetActiveScene().name != "CutScene1" && SceneManager.GetActiveScene().name != "CutScene2" && GameManager.IsMicrophone == true)
        {
            MicrophoneStatus();
        }
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "MapSelection" && SceneManager.GetActiveScene().name != "CutScene0"
        && SceneManager.GetActiveScene().name != "CutScene1" && SceneManager.GetActiveScene().name != "CutScene2" && GameManager.IsMicrophone == true)
        {
            GetMicrophoneInput();
        }
    }
}