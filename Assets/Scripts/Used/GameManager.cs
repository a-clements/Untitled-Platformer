using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SpeechLib;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script determines how the game is played. The designer can define how many keys are to be used and what those keys are.
/// The designer can also define how many game controller axis's to use. For the purpose of this game this has been dis-allowed. This script
/// loads and saves the variables in the GameSettings script as a JSON, and will set the keys and sliders of the options menu.
/// </summary>

public class GameManager : MonoBehaviour
{
    public KeyCode[] Keys;
    [HideInInspector] public string[] Movement;

    public GameSettings Gamesettings = new GameSettings();

    SpVoice Voice = new SpVoice();
    SpeechVoiceSpeakFlags Flags;

    private VoiceOver Voiceover;
    private ColourGrading Colourgrading;
    private AudioMixer Audiomixer;
    private CameraShake Camerashake;

    [SerializeField] private GameObject QuitMenu;

    public static bool IsMicrophone = false;

    private void Awake()
    {
        Flags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
        Voiceover = FindObjectOfType<VoiceOver>();
        Colourgrading = FindObjectOfType<ColourGrading>();
        Audiomixer = FindObjectOfType<AudioMixer>();
        Camerashake = FindObjectOfType<CameraShake>();
        //this function is executed first
    }

    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }

    public void QuitToMap()
    {
        SceneManager.LoadSceneAsync("MapSelection", LoadSceneMode.Single);
        ScoreManager.LevelScore = 0;
    }

    public void CloseQuitMenu()
    {
        QuitMenu.SetActive(false);
        GameObject.Find("In Game UI").transform.GetChild(0).GetChild(3).GetComponent<Button>().Select();
    }

    public void ShowQuitMenu()
    {
        QuitMenu.SetActive(true);
        QuitMenu.transform.GetChild(0).GetChild(1).GetComponent<Button>().Select();
    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/GameSettings.json"))
        {
            Gamesettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/GameSettings.json"));

            Keys[0] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.LeftKey, true);
            Keys[1] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.RightKey, true);
            Keys[2] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.FireLeftKey, true);
            Keys[3] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.FireUpKey, true);
            Keys[4] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.ThrowKey, true);
            Keys[5] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.PowerKey, true);
            Keys[6] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.JumpKey, true);
            Colourgrading.ExposureSlider.value = Gamesettings.PostExposure;
            Colourgrading.HueSlider.value = Gamesettings.HueShift;
            Colourgrading.ContrastSlider.value = Gamesettings.ContrastValue;
            Colourgrading.RedChannelSlider.value = Gamesettings.RedChannel;
            Colourgrading.GreenChannelSlider.value = Gamesettings.GreenChannel;
            Colourgrading.BlueChannelSlider.value = Gamesettings.BlueChannel;
            Audiomixer.MasterVolumeSlider.value = Gamesettings.MasterVolume;
            Audiomixer.AmbientVolumeSlider.value = Gamesettings.AmbientVolume;
            Audiomixer.MusicVolumeSlider.value = Gamesettings.MusicVolume;
            Audiomixer.SFXVolumeSlider.value = Gamesettings.SFXVolume;
            Voiceover.VoiceOverVolumeSlider.value = Gamesettings.VoiceoverVolume;
            Voiceover.VoiceOverToggleSlider.value = Gamesettings.VoiceoverToggle;
            Camerashake.ShakeMagnitude.value = Gamesettings.CameraShake;
            Camerashake.VolumeThreshold.value = Gamesettings.MicrophoneThreshold;
        }

        else
        {
            SaveSettings();
        }

        Audiomixer.Setup();
        Colourgrading.Setup();
    }

    public void SaveSettings()
    {
        Gamesettings.LeftKey = Keys[0].ToString();
        Gamesettings.RightKey = Keys[1].ToString();
        Gamesettings.FireLeftKey = Keys[2].ToString();
        Gamesettings.FireUpKey = Keys[3].ToString();
        Gamesettings.ThrowKey = Keys[4].ToString();
        Gamesettings.PowerKey = Keys[5].ToString();
        Gamesettings.JumpKey = Keys[6].ToString();
        Gamesettings.PostExposure = Colourgrading.ExposureSlider.value;
        Gamesettings.HueShift = Colourgrading.HueSlider.value;
        Gamesettings.ContrastValue = Colourgrading.ContrastSlider.value;
        Gamesettings.RedChannel = Colourgrading.RedChannelSlider.value;
        Gamesettings.GreenChannel = Colourgrading.GreenChannelSlider.value;
        Gamesettings.BlueChannel = Colourgrading.BlueChannelSlider.value;
        Gamesettings.MasterVolume = Audiomixer.MasterVolumeSlider.value;
        Gamesettings.AmbientVolume = Audiomixer.AmbientVolumeSlider.value;
        Gamesettings.MusicVolume = Audiomixer.MusicVolumeSlider.value;
        Gamesettings.SFXVolume = Audiomixer.SFXVolumeSlider.value;
        Gamesettings.VoiceoverVolume = Voiceover.VoiceOverVolumeSlider.value;
        Gamesettings.VoiceoverToggle = Voiceover.VoiceOverToggleSlider.value;
        Gamesettings.CameraShake = Camerashake.ShakeMagnitude.value;
        Gamesettings.MicrophoneThreshold = Camerashake.VolumeThreshold.value;

        string jsondata = JsonUtility.ToJson(Gamesettings, true); //this line serializes the Gamemanager variables and creates a string

        File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", jsondata); //This line writes the jsondata string to a file at the application path.
    }

    void Start()
    {
        LoadSettings();
        //this function is executed third
    }

    public void Default()
    {
        if (File.Exists(Application.persistentDataPath + "/GameSettings.json"))
        {
            File.Delete((Application.persistentDataPath + "/GameSettings.json"));
        }

        Keys[0] = KeyCode.LeftArrow;
        Keys[1] = KeyCode.RightArrow;
        Keys[2] = KeyCode.A;
        Keys[3] = KeyCode.S;
        Keys[4] = KeyCode.D;
        Keys[5] = KeyCode.G;
        Keys[6] = KeyCode.Space;
        Colourgrading.ExposureSlider.value = 0.0f;
        Colourgrading.HueSlider.value = 0.0f;
        Colourgrading.ContrastSlider.value = 0.0f;
        Colourgrading.RedChannelSlider.value = 100.0f;
        Colourgrading.GreenChannelSlider.value = 100.0f;
        Colourgrading.BlueChannelSlider.value = 100.0f;
        Audiomixer.MasterVolumeSlider.value = 0.0f;
        Audiomixer.AmbientVolumeSlider.value = -16.0f;
        Audiomixer.MusicVolumeSlider.value = -16.0f;
        Audiomixer.SFXVolumeSlider.value = -16.0f;
        Voiceover.VoiceOverVolumeSlider.value = 0.5f;
        Voiceover.VoiceOverToggleSlider.value = 0.0f;
        Camerashake.ShakeMagnitude.value = 0.5f;
        Camerashake.VolumeThreshold.value = 0.5f;

        SaveSettings();

        ButtonRemapping[] Remapped;

        Remapped = FindObjectsOfType<ButtonRemapping>();

        foreach(ButtonRemapping mapped in Remapped)
        {
            mapped.Start();
        }
    }

    public void Speak(string text)
    {
        if(Voiceover.VoiceOverToggleSlider.value == 1)
        {
            Voice.Volume = (int)(Voiceover.VoiceOverVolumeSlider.value * 100);
            Voice.Speak(text);
        }
    }

}
