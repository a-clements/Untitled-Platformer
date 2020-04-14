using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// This script defines which microphone is used, what the sample length is, and the sample frequency. This script will also detect if a microphone is present, and give the player a choice to use it.
/// This script will also enable or disable options controls based on the choice of the player of using the microphone or not. This script will also dynamically change level UI based on microphone status.
/// This script will restart a re-connected microphone when a new level loads.
/// </summary>

public class ShoutControl : MonoBehaviour
{
    private AudioSource Source;
    private string Device;
    private bool ShowPanel = true;
    private float[] CurrentSampleData;
    private int CurrentPosition;
    private EventSystem GetEventSystem;

    public static float Volume;

    [SerializeField] private GameObject Panels;
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
        GetEventSystem = FindObjectOfType<EventSystem>();

        GetEventSystem.firstSelectedGameObject = Panels;

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (Microphone.devices.Length != 0 && GameManager.IsMicrophone == false)
            {
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

            Navigation Nav1 = Panels.transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Button>().navigation;
            Navigation Nav2 = Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(12).GetComponent<Slider>().navigation;

            Button Butt1 = Panels.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(5).GetComponent<Button>();
            Button Butt2 = Panels.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Button>();
            
            Slider Slid1 = Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(11).GetComponent<Slider>();

            Nav1.selectOnUp = Butt1;
            Nav1.selectOnRight = Butt2;

            Nav2.selectOnUp = Slid1;
            Nav2.selectOnDown = Butt1;

            Panels.transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Button>().navigation = Nav1;
            Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(12).GetComponent<Slider>().navigation = Nav2;
        }
    }


    public void UseMicrophoneButton()
    {
        GameManager.IsMicrophone = true;
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        Panels.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(false);
        Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(13).gameObject.SetActive(true);
        Panels.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().Select();
    }

    public void DontUseMicropphoneButton()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
        Panels.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(true);

        Navigation Nav1 = Panels.transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Button>().navigation;
        Navigation Nav2 = Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(12).GetComponent<Slider>().navigation;

        Button Butt1  = Panels.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(5).GetComponent<Button>(); 
        Button Butt2 = Panels.transform.GetChild(3).GetChild(0).GetChild(2).GetComponent<Button>();

        Slider Slid1 = Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(11).GetComponent<Slider>();

        Nav1.selectOnUp = Butt1;
        Nav1.selectOnRight = Butt2;

        Nav2.selectOnUp = Slid1;
        Nav2.selectOnDown = Butt1;

        Panels.transform.GetChild(3).GetChild(0).GetChild(3).GetComponent<Button>().navigation = Nav1;
        Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(12).GetComponent<Slider>().navigation = Nav2;

        Panels.transform.GetChild(3).GetChild(0).GetChild(1).GetChild(13).gameObject.SetActive(false);
        Panels.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Button>().Select();

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