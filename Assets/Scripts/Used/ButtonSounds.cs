using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

/// <summary>
/// This script detects a pointer enter, pointer exit, and pointer click. It is the scripting version of using an event trigger in the
/// inspector.
/// </summary>

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip MouseEnter;
    [SerializeField] private AudioClip MouseClick;

    private AudioSource ButtonAudio;

    void Start()
    {
        ButtonAudio = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonAudio.Stop();
        ButtonAudio.PlayOneShot(MouseEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ButtonAudio.Stop();
        ButtonAudio.PlayOneShot(MouseClick);
    }
}
