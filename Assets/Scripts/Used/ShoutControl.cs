using System.Collections;
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
    [SerializeField]private string Device;
    public static float Volume;
    [SerializeField] private bool IsMicrophone = false;

    [SerializeField] private Image VolumeMetre;
    [SerializeField] private Text VolumeText;

    [SerializeField] private int DeviceNumber = 0; //this is the microphone number, 0 is the first microphone found
    [SerializeField] private int SampleLength = 10; //this is the length of a sample, the default is 10 for sampling 10 seconds of audio
    [SerializeField] private int SampleFrequency = 128; //this is how often a sample is taken, the default is 128 samples a second
    [SerializeField] private AudioMixerGroup MicrophoneMixer;

    private void OnEnable()
    {
        Source = GetComponent<AudioSource>();
        Source.outputAudioMixerGroup = MicrophoneMixer;
    }

    private void OnDisable()
    {
        Microphone.End(Device);
        Device = "";
        Source = null;
    }

    void Start()
    {

    }

    void StartMicrophone()
    {

        if(Microphone.IsRecording(Device) == false)
        {
            Source.clip = Microphone.Start(Device, true, SampleLength, AudioSettings.outputSampleRate);
            IsMicrophone = true;
            Source.Play();
        }
    }

    void EndMicrophone()
    {
        Source.Stop();
        Microphone.End(Device);
        IsMicrophone = false;
    }

    void MicrophoneStatus()
    {
        if (Microphone.devices.Length != 0)
        {
            Debug.Log("starting");
            Device = Microphone.devices[DeviceNumber];

            //StartMicrophone();

            this.transform.GetChild(0).gameObject.SetActive(false);
        }

        else
        {
            Debug.Log("ended");

            //EndMicrophone();

            Device = "";
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(IsMicrophone == true)
        {
            float Level = 0;
            float[] SampleData = new float[SampleFrequency];
            int Position = Microphone.GetPosition(Device) - (SampleFrequency + 1);

            Source.clip.GetData(SampleData, Position);

            for (int i = 0; i < SampleFrequency; i++)
            {
                float SamplePeak = SampleData[i] * SampleData[i];

                if (Level < SamplePeak)
                {
                    Level = SamplePeak;
                }
            }

            Volume = Mathf.Sqrt(Mathf.Sqrt(Level));

            if (SceneManager.GetActiveScene().name != "Menu" && SceneManager.GetActiveScene().name != "MapSelection" && SceneManager.GetActiveScene().name != "CutScene0"
                && SceneManager.GetActiveScene().name != "CutScene1" && SceneManager.GetActiveScene().name != "CutScene2")
            {
                VolumeMetre.fillAmount = Volume;
                VolumeText.text = (System.Math.Round(Volume, 2) * 100).ToString();
            }
        }

        else
        {
            MicrophoneStatus();
        }
    }
}
