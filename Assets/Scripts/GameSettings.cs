using UnityEngine;

/// <summary>
/// This script holds all the variables of the user settings to be saved and retrieved.
/// </summary>

public class GameSettings
{
    public string Leftkey;
    public string Rightkey;
    public string Fireleftkey;
    public string Fireupkey;
    public string Throwkey;
    public string Jumpkey;
    public float Exposure;
    public float Hue;
    public float Contrast;
    public float Red;
    public float Green;
    public float Blue;
    public float Master;
    public float Ambient;
    public float Music;
    public float SFX;
    public float Voiceover;
    public float Toggle;

    public float PostExposure
    {
        get
        {
            return Exposure;
        }

        set
        {
            Exposure = value;
        }
    }

    public float HueShift
    {
        get
        {
            return Hue;
        }

        set
        {
            Hue = value;
        }
    }

    public float ContrastValue
    {
        get
        {
            return Contrast;
        }

        set
        {
            Contrast = value;
        }
    }

    public float RedChannel
    {
        get
        {
            return Red;
        }

        set
        {
            Red = value;
        }
    }

    public float GreenChannel
    {
        get
        {
            return Green;
        }

        set
        {
            Green = value;
        }
    }

    public float BlueChannel
    {
        get
        {
            return Blue;
        }

        set
        {
            Blue = value;
        }
    }

    public float MasterVolume
    {
        get
        {
            return Master;
        }

        set
        {
            Master = value;
        }
    }

    public float AmbientVolume
    {
        get
        {
            return Ambient;
        }

        set
        {
            Ambient = value;
        }
    }

    public float MusicVolume
    {
        get
        {
            return Music;
        }

        set
        {
            Music = value;
        }
    }

    public float SFXVolume
    {
        get
        {
            return SFX;
        }

        set
        {
            SFX = value;
        }
    }

    public float VoiceoverVolume
    {
        get
        {
            return Voiceover;
        }

        set
        {
            Voiceover = value;
        }
    }

    public float VoiceoverToggle
    {
        get
        {
            return Toggle;
        }

        set
        {
            Toggle = value;
        }
    }

    public string RightKey
    {
        get
        {
            return Rightkey;
        }

        set
        {
            Rightkey = value;
        }
    }

    public string LeftKey
    {
        get
        {
            return Leftkey;
        }

        set
        {
            Leftkey = value;
        }
    }

    public string FireLeftKey
    {
        get
        {
            return Fireleftkey;
        }

        set
        {
            Fireleftkey = value;
        }
    }

    public string FireUpKey
    {
        get
        {
            return Fireupkey;
        }

        set
        {
            Fireupkey = value;
        }
    }

    public string ThrowKey
    {
        get
        {
            return Throwkey;
        }

        set
        {
            Throwkey = value;
        }
    }

    public string JumpKey
    {
        get
        {
            return Jumpkey;
        }

        set
        {
            Jumpkey = value;
        }
    }
}