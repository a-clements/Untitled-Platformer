using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing; //This needs to be included to give the code access to the post process stack.

public class ColourGrading : MonoBehaviour
{
    ColorGrading ColourGrade = null; //ColorGrading is an effect of the post processand the variable needs to be initialised to null.
    [SerializeField] PostProcessVolume PostProcess; //This is a post processing object that is filled with the post process component on the game object.

    public Slider HueSlider;
    public Slider ContrastSlider;
    public Slider ExposureSlider;
    public Slider RedChannelSlider;
    public Slider GreenChannelSlider;
    public Slider BlueChannelSlider;

    //public int HueShift; //this is the float variable that controls the hue 
    public float Saturation; //This is the float variable that gives controls the saturation value of the post process stack. It has a range of -100 to 100. 
    //public int Contrast; //This is the float variable that gives controls the contrast value of the post process stack. It has a range of -10 to 10. 
    //public float Exposure; //This is the float variable that gives controls the contrast value of the post process stack. It has a range of -5 to 5. 
    //public int RedChannel = 100; //this is the float variable for the red channel 
    //public int GreenChannel = 100; //this is the float variable for the green channel 
    //public int BlueChannel = 100; //this is the float variable for the blue channel 

    public static ColourGrading Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start ()
    {
        PostProcess = GetComponent<PostProcessVolume>(); //This gets the post process volume componentand places it in the PostProcess variable.
        PostProcess.profile.TryGetSettings(out ColourGrade); //This  gets the ColorGrading effect and places it in the ColourGrading variable.
    }

    public void OnHueChange()
    {
        ColourGrade.hueShift.value = HueSlider.value;
    }

    public void OnContrastChange()
    {
        ColourGrade.contrast.value = ContrastSlider.value;
    }

    public void OnExposureChange()
    {
        ColourGrade.postExposure.value = ExposureSlider.value;
    }

    public void OnRedChange()
    {
        ColourGrade.mixerRedOutRedIn.value = RedChannelSlider.value;
    }

    public void OnGreenChange()
    {
        ColourGrade.mixerGreenOutGreenIn.value = GreenChannelSlider.value;
    }

    public void OnBlueChange()
    {
        ColourGrade.mixerBlueOutBlueIn.value = BlueChannelSlider.value;
    }


    void Update ()
    {
        ColourGrade.saturation.value = Saturation;
	}
}
