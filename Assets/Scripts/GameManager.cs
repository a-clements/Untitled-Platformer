using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpeechLib;

public class GameManager : MonoBehaviour
{
    public KeyCode[] Keys;
    public string[] Movement;

    SpVoice Voice = new SpVoice();

    private void Awake()
    {
        //this function is executed first
    }

    private void OnEnable()
    {
        //this function is executed second
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
