using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing; //This needs to be included to give the code access to the post process stack.

/// <summary>
/// This script accesses the post process volume within the render pipeline so that individual settings can be adjusted by the designer
/// or during runtime. In this particular instance, the hue, contrast, exposure, saturation, and the RGB channels have been exposed to the
/// designer.
/// </summary>

public class ColourGrading : MonoBehaviour
{
    ColorGrading ColourGrade = null; //ColorGrading is an effect of the post process and the variable needs to be initialised to null.
    PostProcessVolume PostProcess; //This is a post processing object that is filled with the post process component on the game object.

    public Slider HueSlider;
    public Slider ContrastSlider;
    public Slider ExposureSlider;
    public Slider RedChannelSlider;
    public Slider GreenChannelSlider;
    public Slider BlueChannelSlider;

    public float Saturation; //This is the float variable that gives controls the saturation value of the post process stack. It has a range of -100 to 100. 

    void Start ()
    {
        PostProcess = GetComponent<PostProcessVolume>();
        PostProcess.profile.TryGetSettings(out ColourGrade); //This gets the ColorGrading effect and places it in the ColourGrading variable.
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
