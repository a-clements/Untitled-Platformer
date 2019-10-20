using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class ShoutMetre : MonoBehaviour
{
    private AudioSource Source;
    private string Device;
    private float Sensitivity = 25;
    private float Volume;

    public static float MicLoudness;

    [SerializeField] private int DeviceNumber = 0; //this is the microphone number, 0 is the first microphone found
    [SerializeField] private int SampleLength = 10; //this is the length of a sample, the default is 10 for sampling 10 seconds of audio
    [SerializeField] private int SampleFrequency = 128; //this is how often a sample is taken, the default is 128 samples a second
    [SerializeField] private AudioMixerGroup MicrophoneMixer;
    [SerializeField] private AudioMixerGroup MasterMixer;

    private void OnEnable()
    {
        if (Device == "")
        {
            Device = Microphone.devices[DeviceNumber];
        }

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
        Source.clip = Microphone.Start(Device, true, SampleLength, AudioSettings.outputSampleRate);
        Source.Play();
    }


    void Update()
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
    }
}
