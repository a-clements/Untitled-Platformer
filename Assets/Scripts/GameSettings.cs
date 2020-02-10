using UnityEngine;

public class GameSettings
{
    /*This script is where game wide variables are stored so they can be serialized for saving to a JSON file or loading from a JSON file.  */
    /* the SET keyword assigns a value to a variable.                                                                                       */
    /*the GET keyword returns the value from the variable to whatever function is trying to use it.                                         */
    /*Notice that there is base class such as MonoBehaviour being used. If it was then it could not be serialized.                          */

    //public bool Fullscreen;
    //public bool Autofire;
    //public bool Voicecontrol;
    //public bool Texttospeech;
    //public int Resolutionindex;
    //public float Musicvolume;
    //public float SFXvolume;
    //public float Gammacorrection;
    public string Leftkey;
    public string Rightkey;
    public string Fireleftkey;
    public string Fireupkey;
    public string Throwkey;
    public string Jumpkey;
    public float Exposure;
    public int Hue;
    public int Contrast;
    public int Red;
    public int Green;
    public int Blue;

    //public string movement0; 
    //public string movement1; 
    //public string movement2; 
    //public Color UIMaincolour; 
    //public Color UISecondcolour; 


    //public bool TextToSpeech
    //{
    //    get
    //    {
    //        return Texttospeech;
    //    }

    //    set
    //    {
    //        Texttospeech = value;
    //    }
    //}

    //public bool VoiceControl
    //{
    //    get
    //    {
    //        return Voicecontrol;
    //    }

    //    set
    //    {
    //        Voicecontrol = value;
    //    }
    //}
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

    public int HueShift
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

    public int ContrastValue
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

    public int RedChannel
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

    public int GreenChannel
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

    public int BlueChannel
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

    //public string Movement0
    //{
    //    get
    //    {
    //        return movement0;
    //    }

    //    set
    //    {
    //        movement0 = value;
    //    }
    //}

    //public string Movement1
    //{
    //    get
    //    {
    //        return movement1;
    //    }

    //    set
    //    {
    //        movement1 = value;
    //    }
    //}

    //public string Movement2
    //{
    //    get
    //    {
    //        return movement2;
    //    }

    //    set
    //    {
    //        movement2 = value;
    //    }
    //}

    //public bool FullScreen
    //{
    //    get
    //    {
    //        return Fullscreen;
    //    }

    //    set
    //    {
    //        Fullscreen = value;
    //    }
    //}

    //public bool AutoFire
    //{
    //    get
    //    {
    //        return Autofire;
    //    }

    //    set
    //    {
    //        Autofire = value;
    //    }
    //}

    //public int ResolutionIndex
    //{
    //    get
    //    {
    //        return Resolutionindex;
    //    }

    //    set
    //    {
    //        Resolutionindex = value;
    //    }
    //}

    //public float MusicVolume
    //{
    //    get
    //    {
    //        return Musicvolume;
    //    }

    //    set
    //    {
    //        Musicvolume = value;
    //    }
    //}

    //public float SFXVolume
    //{
    //    get
    //    {
    //        return SFXvolume;
    //    }

    //    set
    //    {
    //        SFXvolume = value;
    //    }
    //}

    //public float GammaCorrection
    //{
    //    get
    //    {
    //        return Gammacorrection;
    //    }

    //    set
    //    {
    //        Gammacorrection = value;
    //    }
    //}

    //public Color UIMainColour
    //{
    //    get
    //    {
    //        return UIMaincolour;
    //    }

    //    set
    //    {
    //        UIMaincolour = value;
    //    }
    //}

    //public Color UISecondColour
    //{
    //    get
    //    {
    //        return UISecondcolour;
    //    }

    //    set
    //    {
    //        UISecondcolour = value;
    //    }
    //}
}