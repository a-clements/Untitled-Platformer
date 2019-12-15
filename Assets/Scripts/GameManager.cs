using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SpeechLib;

public class GameManager : MonoBehaviour
{
    public KeyCode[] Keys;
    public string[] Movement;

    public GameSettings Gamesettings = new GameSettings();

    SpVoice Voice = new SpVoice();

    private void Awake()
    {
        //this function is executed first
    }

    private void OnEnable()
    {
        //this function is executed second
        if (File.Exists(Application.persistentDataPath + "/GameSettings.json"))
        {
            Gamesettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/GameSettings.json"));

            Keys[0] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.LeftKey, true);
            Keys[1] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.RightKey, true);
            Keys[2] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.FireLeftKey, true);
            Keys[3] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.FireUpKey, true);
            Keys[4] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.FireDiagonalKey, true);
            Keys[5] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.ThrowKey, true);
            Keys[6] = (KeyCode)System.Enum.Parse(typeof(KeyCode), Gamesettings.JumpKey, true);

            //Movement[0] = Gamesettings.Movement0;
            //Movement[1] = Gamesettings.Movement1;
            //Movement[2] = Gamesettings.Movement2;

            /*The following nested if statement is for loading the main colour*/
            //UIMainColour = Gamesettings.UIMainColour;

            //Color.RGBToHSV(UIMainColour, out Hue, out Saturation, out Brightness);

            //if (UIMainColour == Color.black)
            //{
            //    MainColourDropdown.value = 0;
            //}
            //else if (UIMainColour == Color.blue)
            //{
            //    MainColourDropdown.value = 1;
            //}
            //else if (UIMainColour == Color.cyan)
            //{
            //    MainColourDropdown.value = 2;
            //}
            //else if (UIMainColour == Color.grey)
            //{
            //    MainColourDropdown.value = 3;
            //}
            //else if (UIMainColour == Color.magenta)
            //{
            //    MainColourDropdown.value = 4;
            //}
            //else if (UIMainColour == Color.red)
            //{
            //    MainColourDropdown.value = 5;
            //}
            //else if (UIMainColour == Color.white)
            //{
            //    MainColourDropdown.value = 6;
            //}
            //else
            //{
            //    MainColourDropdown.value = 7;
            //}

            //MainHue.value = Hue;
            //MainSaturation.value = Saturation;
            //MainBrightness.value = Brightness;

            ///*The following nested if statement is for loading the secondary colour*/
            //UISecondColour = Gamesettings.UISecondColour;

            //Color.RGBToHSV(UISecondColour, out Hue, out Saturation, out Brightness);

            //if (UISecondColour == Color.black)
            //{
            //    SecondColourDropdown.value = 0;
            //}
            //else if (UISecondColour == Color.blue)
            //{
            //    SecondColourDropdown.value = 1;
            //}
            //else if (UISecondColour == Color.cyan)
            //{
            //    SecondColourDropdown.value = 2;
            //}
            //else if (UISecondColour == Color.grey)
            //{
            //    SecondColourDropdown.value = 3;
            //}
            //else if (UISecondColour == Color.magenta)
            //{
            //    SecondColourDropdown.value = 4;
            //}
            //else if (UISecondColour == Color.red)
            //{
            //    SecondColourDropdown.value = 5;
            //}
            //else if (UISecondColour == Color.white)
            //{
            //    SecondColourDropdown.value = 6;
            //}
            //else
            //{
            //    SecondColourDropdown.value = 7;
            //}

            //SecondHue.value = Hue;
            //SecondSaturation.value = Saturation;
            //SecondBrightness.value = Brightness;
        }

        else
        {
            Gamesettings.LeftKey = Keys[0].ToString();
            Gamesettings.RightKey = Keys[1].ToString();
            Gamesettings.FireLeftKey = Keys[2].ToString();
            Gamesettings.FireUpKey = Keys[3].ToString();
            Gamesettings.FireDiagonalKey = Keys[4].ToString();
            Gamesettings.ThrowKey = Keys[5].ToString();
            Gamesettings.JumpKey = Keys[6].ToString();


            string jsondata = JsonUtility.ToJson(Gamesettings, true); //this line serializes the Gamemanager variables and creates a string

            File.WriteAllText(Application.persistentDataPath + "/GameSettings.json", jsondata); //This line writes the jsondata string to a file at the application path.
        }
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        //this function is executed third
    }

    public void Speak(string text)
    {
        Voice.Speak(text);
    }

}
