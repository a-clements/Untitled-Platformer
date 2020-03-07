using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceOver : MonoBehaviour
{
    public Slider VoiceOverToggleSlider;
    public Slider VoiceOverVolumeSlider;

    public static VoiceOver Instance = null;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
