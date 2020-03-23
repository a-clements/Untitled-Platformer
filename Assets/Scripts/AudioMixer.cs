﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This script interfaces with the audio mixer group found in the Audio Mixer folder.
/// The sliders control each channel of the mixer individually.
/// </summary>

public class AudioMixer : MonoBehaviour
{
    public Slider MasterVolumeSlider;
    public Slider AmbientVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;

    private GameObject Player;
    private GameObject[] Enemies;
    private GameObject AudioManager;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        AudioManager = GameObject.Find("Audio Manager");
    }

    void Start()
    {

    }

    public void Setup()
    {
        OnMasterVolumeChange();
        OnAmbientVolumeChange();
        OnMusicVolumeChange();
        OnSFXVolumeChange();
    }

    public void OnMasterVolumeChange()
    {
        AudioManager.transform.GetChild(0).GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("Master", MasterVolumeSlider.value);
    }

    public void OnAmbientVolumeChange()
    {
        AudioManager.transform.GetChild(1).GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("Ambient", AmbientVolumeSlider.value);
    }

    public void OnMusicVolumeChange()
    {
        AudioManager.transform.GetChild(2).GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("Music", MusicVolumeSlider.value);
    }

    public void OnSFXVolumeChange()
    {
        AudioManager.transform.GetChild(3).GetComponent<AudioSource>().outputAudioMixerGroup.audioMixer.SetFloat("SFX", SFXVolumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
