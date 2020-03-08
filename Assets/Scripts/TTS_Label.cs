using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This script detects a mouseover event on a label. 
/// </summary>

public class TTS_Label : MonoBehaviour, IPointerEnterHandler
{
    private GameManager GameManager;
    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.enabled == true)
        {
            GameManager.Speak(transform.GetComponent<Text>().text);
        }
    }
}